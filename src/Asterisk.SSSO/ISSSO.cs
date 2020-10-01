using Asterisk.SSSO.Entities;
using Asterisk.SSSO.EventArgs;
using AsterNet.Standard;
using AsterNet.Standard.Models;
using System;

namespace Asterisk.SSSO
{
    public interface ISSSO
    {
        #region Variables
        int? MaxWaitingTime { get; set; }
        int? PinLength { get; set; }
        int? RetryCount { get; set; }
        #endregion

        #region Actions
        Action<OnValidateNumberBeforeArgs> OnValidateNumberBefore { get; set; }
        Action<OnValidateNumberAfterArgs> OnValidateNumberAfter { get; set; }
        Action<OnAuthenticationFailedBeforeArgs> OnAuthenticationFailedBefore { get; set; }
        Action<OnAuthenticationFailedAfterArgs> OnAuthenticationFailedAfter { get; set; }
        Action<OnAuthenticatedBeforeArgs> OnAuthenticatedBefore { get; set; }
        Action<OnAuthenticatedAfterArgs> OnAuthenticatedAfter { get; set; }
        Action<OnValidatePinBeforeArgs> OnValidatePinBefore { get; set; }
        Action<OnValidatePinAfterArgs> OnValidatePinAfter { get; set; }
        #endregion

        #region EventHandlers
        event EventHandler<OnAuthenticatedEventArgs> OnAuthenticatedEventHandler;
        event EventHandler<OnAuthenticationFailedArgs> OnAuthenticationFailedEventHandler;
        #endregion

        #region Events
        void OnAuthenticated(OnAuthenticatedEventArgs e);

        void OnAuthenticationFailed(OnAuthenticationFailedArgs e, Action hangupAction = null);
        #endregion

        #region Override
        bool ValidateNumber(IAriClient ariClient, Channel channel);
        bool ValidatePin(DtmfItem dtmfItem);
        #endregion
    }
}
