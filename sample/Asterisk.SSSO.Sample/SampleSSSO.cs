using Asterisk.SSSO.Entities;
using Asterisk.SSSO.EventArgs;
using Asterisk.SSSO.Utilities;
using AsterNet.Standard;
using AsterNet.Standard.ARI_1_0.Models;

namespace Asterisk.SSSO.Sample
{
    public class SampleSSSO : BaseSSSO
    {
        ARIMain ariMain; 

        public SampleSSSO()
        {
            StasisEndpoint endpoint = new StasisEndpoint("127.0.0.1",7004,"root","123",false);
            ariMain = new ARIMain(endpoint, "AsteriskSSSO", this);

            this.MaxWaitingTime = 30;
            this.RetryCount = 3;
            this.PinLength = 4;

            DoWork();
        }

        public override bool ValidateNumber(IAriClient ariClient, Channel channel)
        {
            if (channel.Caller.Number == "90530000000")
                return true;
            return false;
        }

        public override bool ValidatePin(DtmfItem dtmfItem)
        {
            if (dtmfItem.digits == "1234" && dtmfItem.channel.Caller.Number == "905300000000")
                return true;
            return false;
        }

        private void DoWork()
        {
            this.OnValidateNumberBefore = (OnValidateNumberBeforeArgs e) =>
            {
                if(e.channel.Caller.Number == "905300000000")
                {

                }
            };

            this.OnValidateNumberAfter = (OnValidateNumberAfterArgs e) =>
            {
                if(e.cause == Constants.CallerIdValidated)
                {

                }
                else
                {

                }
            };

            this.OnAuthenticationFailedBefore = (OnAuthenticationFailedBeforeArgs e) =>
            {
                if(e.cause == Constants.TimeHasExpired)
                {

                }
                if(e.cause == Constants.PinValidationError)
                {

                }
                if(e.cause == Constants.PleaseAgainTry)
                {

                }
            };

            this.OnAuthenticationFailedEventHandler += (sender, e) =>
            {
                if (e.cause == Constants.TimeHasExpired)
                {

                }
                if (e.cause == Constants.PinValidationError)
                {

                }
                if (e.cause == Constants.PleaseAgainTry)
                {

                }
            };

            this.OnAuthenticationFailedAfter = (OnAuthenticationFailedAfterArgs e) =>
            {
                if (e.cause == Constants.TimeHasExpired)
                {

                }
                if (e.cause == Constants.PinValidationError)
                {

                }
                if (e.cause == Constants.PleaseAgainTry)
                {

                }
            };

            this.OnValidatePinBefore = (OnValidatePinBeforeArgs e) =>
            {
                if(e.dtmfItem.channel.Caller.Number == "905300000000" && e.dtmfItem.digits == "####")
                {                    
                }
            };

            this.OnValidatePinAfter = (OnValidatePinAfterArgs e) =>
            {
                if(e.cause == Constants.PinValidated)
                {

                }
                else
                {

                }
            };

            this.OnAuthenticatedBefore = (OnAuthenticatedBeforeArgs e) =>
            {
                
            };

            this.OnAuthenticatedEventHandler += (sender, e) =>
            {
                
            };

            this.OnAuthenticatedAfter = (OnAuthenticatedAfterArgs e) =>
            {

            };
        }
    }
}
