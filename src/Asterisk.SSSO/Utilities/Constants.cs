namespace Asterisk.SSSO.Utilities
{
    public static class Constants
    {
        #region Exception
        public static readonly string MaxWaitingTimeNullExceptionText = "Max Waiting Time Is Not Be Null";
        public static readonly string PinLengthNullExceptionText = "Pin Length Is Not Be Null";
        public static readonly string RetryCountNullExceptionText = "Retry Count Is Not Be Null";
        public static readonly string EventHandlersNotImplementedException = "{0} Event Is Not Implemented";
        #endregion

        #region Messages
        public static readonly string Authenticated = "Authenticated Successfully";
        public static readonly string AuthenticationFailed = "Authenticate Failed";
        public static readonly string PleaseAgainTry = "Please Again Try";
        public static readonly string TimeHasExpired = "Time Has Expired";

        public static readonly string CallerIdValidated = "Caller Id Validated";
        public static readonly string CallerIdValidationError = "Caller Id Is Not Validated";
        public static readonly string PinValidated = "Pin Validated";
        public static readonly string PinValidationError = "Pin Is Not Validated";
        #endregion
    }
}
