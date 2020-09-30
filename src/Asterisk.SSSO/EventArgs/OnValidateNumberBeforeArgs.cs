using AsterNet.Standard.ARI_1_0.Models;

namespace Asterisk.SSSO.EventArgs
{
    public class OnValidateNumberBeforeArgs
    {
        public Channel channel { get; set; }

        public OnValidateNumberBeforeArgs(Channel _channel)
        {
            channel = _channel;
        }
    }
}
