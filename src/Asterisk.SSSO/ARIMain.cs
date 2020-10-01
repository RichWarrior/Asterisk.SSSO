using Asterisk.SSSO.Entities;
using Asterisk.SSSO.EventArgs;
using Asterisk.SSSO.Utilities;
using AsterNet.Standard;
using AsterNet.Standard.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Asterisk.SSSO
{
    public class ARIMain
    {
        #region Variables
        public AriClient ariClient;
        public IDictionary<string, DtmfItem> dtmfs = new Dictionary<string, DtmfItem>();
        public IDictionary<string, string> numbers = new Dictionary<string, string>();

        Timer dtmfTimer;
        object lockObject = new object();

        ISSSO ssso;
        #endregion

        public ARIMain(StasisEndpoint endpoint, string app, ISSSO _ssso)
        {
            ariClient = new AriClient(endpoint, app);
            ariClient.Connect();

            ssso = _ssso;

            ariClient.OnStasisStartEvent += AriClient_OnStasisStartEvent;
            ariClient.OnChannelHangupRequestEvent += AriClient_OnChannelHangupRequestEvent;
            ariClient.OnChannelDtmfReceivedEvent += AriClient_OnChannelDtmfReceivedEvent;

            dtmfTimer = new Timer(dtmfTimerDoWork, lockObject, TimeSpan.Zero, TimeSpan.FromSeconds(1));
        }
        private void AriClient_OnStasisStartEvent(IAriClient sender, StasisStartEvent e)
        {
            sender.Channels.Answer(e.Channel.Id);
            numbers.Add(e.Channel.Caller.Number, e.Channel.Id);
            ssso.OnValidateNumberBefore?.Invoke(new OnValidateNumberBeforeArgs(e.Channel));
            bool callerIdValidated = ssso.ValidateNumber(sender, e.Channel);
            ssso.OnValidateNumberAfter?.Invoke(new OnValidateNumberAfterArgs(e.Channel, callerIdValidated ? Constants.CallerIdValidated : Constants.CallerIdValidationError));
            if (!callerIdValidated)
            {
                ssso.OnAuthenticationFailed(new OnAuthenticationFailedArgs(e.Channel, Constants.CallerIdValidationError));
                Hangup(e.Channel);
                return;
            }

            DtmfItem dtmfItem = new DtmfItem(e.Channel, ssso.MaxWaitingTime.Value);
            dtmfs.Add(e.Channel.Id, dtmfItem);
        }

        private void AriClient_OnChannelDtmfReceivedEvent(IAriClient sender,ChannelDtmfReceivedEvent e)
        {
            DtmfItem dtmfItem;
            dtmfs.TryGetValue(e.Channel.Id, out dtmfItem);
            if (dtmfItem != null)
            {
                dtmfItem.digits += e.Digit;

                if (dtmfItem.digits.Length == ssso.PinLength)
                {
                    ssso.OnValidatePinBefore?.Invoke(new OnValidatePinBeforeArgs(dtmfItem));
                    var pinValidated = ssso.ValidatePin(dtmfItem);
                    ssso.OnValidatePinAfter?.Invoke(new OnValidatePinAfterArgs(dtmfItem, pinValidated ? Constants.PinValidated : Constants.PinValidationError));
                    if (pinValidated)
                    {
                        ssso.OnAuthenticated(new OnAuthenticatedEventArgs(dtmfItem, Constants.Authenticated));
                        Hangup(dtmfItem.channel);
                        return;
                    }
                    if (dtmfItem.retryCount >= ssso.RetryCount)
                    {
                        ssso.OnAuthenticationFailed(new OnAuthenticationFailedArgs(e.Channel, Constants.AuthenticationFailed));
                        Hangup(e.Channel);
                    }
                    else
                    {
                        ssso.OnAuthenticationFailed(new OnAuthenticationFailedArgs(e.Channel, Constants.PleaseAgainTry));
                        dtmfItem.retryCount++;
                        dtmfItem.digits = "";
                    }
                    return;
                }

                dtmfs.Remove(e.Channel.Id);
                dtmfs.Add(e.Channel.Id, dtmfItem);
            }
        }

        private void AriClient_OnChannelHangupRequestEvent(IAriClient sender, ChannelHangupRequestEvent e)
        {
            string callerId = e.Channel.Caller.Number;
            numbers.Remove(callerId);

            if (!String.IsNullOrEmpty(e.Channel.Id))
                dtmfs.Remove(e.Channel.Id);
            else
            {
                var keys = dtmfs.Keys;
                foreach (var item in keys)
                {
                    DtmfItem dtmfItem;
                    dtmfs.TryGetValue(item, out dtmfItem);
                    if (dtmfItem != null)
                    {
                        if (dtmfItem.channel.Caller.Number == callerId)
                            dtmfs.Remove(item);
                    }
                }
            }
        }

        private void dtmfTimerDoWork(object state)
        {
            if (!Monitor.TryEnter(state))
                return;
            var keys = dtmfs.Keys.ToList();
            foreach (var item in keys)
            {
                DtmfItem dtmfItem;
                dtmfs.TryGetValue(item, out dtmfItem);
                if (dtmfItem != null)
                {
                    dtmfItem.time--;
                    if(dtmfItem.time<0 &&dtmfItem.retryCount >= ssso.RetryCount)
                    {
                        dtmfs.Remove(item);
                        Task.Run(() =>
                        {
                            ssso.OnAuthenticationFailed(new OnAuthenticationFailedArgs(dtmfItem.channel, Constants.TimeHasExpired),()=> {
                                Hangup(dtmfItem.channel);
                            });
                        });
                    }else if(dtmfItem.time < 0)
                    {
                        dtmfs.Remove(item);
                        Task.Run(() =>
                        {
                            ssso.OnAuthenticationFailed(new OnAuthenticationFailedArgs(dtmfItem.channel, Constants.PleaseAgainTry));
                            dtmfItem.time = ssso.MaxWaitingTime.Value;
                            dtmfItem.retryCount++;
                            dtmfItem.digits = "";
                            dtmfs.Add(dtmfItem.channel.Id, dtmfItem);
                        });
                    }
                }
            }
            Monitor.Exit(state);
        }

        private void Hangup(Channel channel)
        {
            string callerId = channel.Caller.Number;

            try
            {
                ariClient.Channels.Hangup(channel.Id);
            }
            catch (Exception)
            {
                AriClient_OnChannelHangupRequestEvent(ariClient, new ChannelHangupRequestEvent() { Channel = channel });
            }
            finally
            {
                numbers.Remove(callerId);
                dtmfs.Remove(channel.Id);
            }
        }
    }
}
