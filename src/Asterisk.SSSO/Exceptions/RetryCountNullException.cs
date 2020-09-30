using Asterisk.SSSO.Utilities;
using System;

namespace Asterisk.SSSO.Exceptions
{
    public class RetryCountNullException : Exception
    {
        public RetryCountNullException():base(Constants.RetryCountNullExceptionText)
        {

        }
    }
}
