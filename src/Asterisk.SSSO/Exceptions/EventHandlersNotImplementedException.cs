using Asterisk.SSSO.Utilities;
using System;

namespace Asterisk.SSSO.Exceptions
{
    public class EventHandlersNotImplementedException : Exception
    {
        public EventHandlersNotImplementedException(string name) 
            : base(String.Format(Constants.EventHandlersNotImplementedException,name))
        {

        }
    }
}
