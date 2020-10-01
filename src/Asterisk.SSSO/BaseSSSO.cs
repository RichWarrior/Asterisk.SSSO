using Asterisk.SSSO.Entities;
using Asterisk.SSSO.EventArgs;
using Asterisk.SSSO.Exceptions;
using AsterNet.Standard;
using AsterNet.Standard.Models;
using System;

namespace Asterisk.SSSO
{
    public class BaseSSSO : ISSSO
    {
        #region Variables
        int? _maxWaitingTime;

        int? _pinLength;

        int? _retryCount;

        public int? MaxWaitingTime
        {
            get
            {                
                if (!_maxWaitingTime.HasValue)
                    throw new MaxWaitingTimeNullException();
                return _maxWaitingTime;
            }
            set
            {
                _maxWaitingTime = value;
            }
        }

        public int? PinLength
        {
            get
            {
                if (!_pinLength.HasValue)
                    throw new PinLengthNullException();
                return _pinLength;
            }
            set
            {
                _pinLength = value;
            }
        }

        public int? RetryCount
        {
            get
            {
                if (!_retryCount.HasValue)
                    throw new RetryCountNullException();
                return _retryCount;
            }
            set
            {
                _retryCount = value;
            }
        }
        #endregion

        #region Actions
        Action<OnValidateNumberBeforeArgs> _onValidateNumberBefore;
        Action<OnValidateNumberAfterArgs> _onValidateNumberAfter;
        Action<OnAuthenticationFailedBeforeArgs> _onAuthenticationFailedBefore;
        Action<OnAuthenticationFailedAfterArgs> _onAuthenticationFailedAfter;
        Action<OnAuthenticatedBeforeArgs> _onAuthenticatedBefore;
        Action<OnAuthenticatedAfterArgs> _onAuthenticatedAfter;
        Action<OnValidatePinBeforeArgs> _onValidatePinBefore;
        Action<OnValidatePinAfterArgs> _onValidatePinAfter;

        public Action<OnValidateNumberBeforeArgs> OnValidateNumberBefore
        {
            get
            {
                return _onValidateNumberBefore;
            }
            set
            {
                _onValidateNumberBefore = value;
            }
        }
        public Action<OnValidateNumberAfterArgs> OnValidateNumberAfter
        {
            get
            {
                return _onValidateNumberAfter;
            }
            set
            {
                _onValidateNumberAfter = value;
            }
        }
        public Action<OnAuthenticationFailedBeforeArgs> OnAuthenticationFailedBefore
        {
            get
            {
                return _onAuthenticationFailedBefore;
            }
            set
            {
                _onAuthenticationFailedBefore = value;
            }
        }
        public Action<OnAuthenticationFailedAfterArgs> OnAuthenticationFailedAfter
        {
            get
            {
                return _onAuthenticationFailedAfter;
            }
            set
            {
                _onAuthenticationFailedAfter = value;
            }
        }
        public Action<OnAuthenticatedBeforeArgs> OnAuthenticatedBefore
        {
            get
            {
                return _onAuthenticatedBefore;
            }
            set
            {
                _onAuthenticatedBefore = value;
            }
        }
        public Action<OnAuthenticatedAfterArgs> OnAuthenticatedAfter
        {
            get
            {
                return _onAuthenticatedAfter;
            }
            set
            {
                _onAuthenticatedAfter = value;
            }
        }
        public Action<OnValidatePinBeforeArgs> OnValidatePinBefore
        {
            get
            {
                return _onValidatePinBefore;
            }
            set
            {
                _onValidatePinBefore = value;
            }
        }
        public Action<OnValidatePinAfterArgs> OnValidatePinAfter
        {
            get
            {
                return _onValidatePinAfter;
            }
            set
            {
                _onValidatePinAfter = value;
            }
        }
        #endregion

        #region EventHandlers
        public event EventHandler<OnAuthenticatedEventArgs> OnAuthenticatedEventHandler;
        public event EventHandler<OnAuthenticationFailedArgs> OnAuthenticationFailedEventHandler;
        #endregion

        #region Events

        public void OnAuthenticated(OnAuthenticatedEventArgs e)
        {
            OnAuthenticatedBefore?.Invoke(new OnAuthenticatedBeforeArgs(e.dtmfItem));
            if (OnAuthenticatedEventHandler == null)
                throw new EventHandlersNotImplementedException(nameof(OnAuthenticatedEventHandler));
            OnAuthenticatedEventHandler(this, e);
            OnAuthenticatedAfter?.Invoke(new OnAuthenticatedAfterArgs(e.dtmfItem));
        }

        public void OnAuthenticationFailed(OnAuthenticationFailedArgs e, Action hangupAction = null)
        {
            OnAuthenticationFailedBefore?.Invoke(new OnAuthenticationFailedBeforeArgs(e.channel, e.cause));
            if (OnAuthenticationFailedEventHandler == null)
                throw new EventHandlersNotImplementedException(nameof(OnAuthenticationFailedEventHandler));
            OnAuthenticationFailedEventHandler(this, e);
            hangupAction?.Invoke();
            OnAuthenticationFailedAfter?.Invoke(new OnAuthenticationFailedAfterArgs(e.channel,e.cause));
        }

        #endregion

        #region Override
        public virtual bool ValidateNumber(IAriClient ariClient, Channel channel)
        {
            throw new EventHandlersNotImplementedException(nameof(ValidateNumber));
        }
        public virtual bool ValidatePin(DtmfItem dtmfItem)
        {
            throw new EventHandlersNotImplementedException(nameof(ValidatePin));
        }
        #endregion
    }
}
