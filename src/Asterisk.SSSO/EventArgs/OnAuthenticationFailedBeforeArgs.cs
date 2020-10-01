using AsterNet.Standard.Models;

namespace Asterisk.SSSO.EventArgs
{
    public class OnAuthenticationFailedBeforeArgs 
    {
        public Channel channel{ get; set; }
        public string cause { get; set; }
        public OnAuthenticationFailedBeforeArgs(Channel _channel,string _cause)             
        {
            channel = _channel;
            cause = _cause;
        }
    }
}
