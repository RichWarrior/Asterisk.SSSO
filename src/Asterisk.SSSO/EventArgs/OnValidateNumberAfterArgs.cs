using AsterNet.Standard.Models;

namespace Asterisk.SSSO.EventArgs
{
    public class OnValidateNumberAfterArgs 
    {
        public Channel channel{ get; set; }
        public string cause { get; set; }
        public OnValidateNumberAfterArgs(Channel _channel,string _cause)
        {
            channel = _channel;
            cause = _cause;
        }
    }
}
