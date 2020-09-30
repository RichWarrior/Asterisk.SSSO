using Asterisk.SSSO.Utilities;
using System;

namespace Asterisk.SSSO.Exceptions
{
    public class MaxWaitingTimeNullException  : Exception
    {
        public MaxWaitingTimeNullException() : base(Constants.MaxWaitingTimeNullExceptionText)
        {

        }        
    }
}
