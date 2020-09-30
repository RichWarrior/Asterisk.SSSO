using Asterisk.SSSO.Utilities;
using System;

namespace Asterisk.SSSO.Exceptions
{
    public class PinLengthNullException : Exception
    {
        public PinLengthNullException() : base(Constants.PinLengthNullExceptionText)
        {

        }
    }
}
