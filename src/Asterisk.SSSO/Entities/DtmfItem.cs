using AsterNet.Standard.ARI_1_0.Models;

namespace Asterisk.SSSO.Entities
{
    public  class DtmfItem
    {
        public Channel channel { get; set; }
        public string digits { get; set; }
        public int time { get; set; }
        public int retryCount { get; set; }

        public DtmfItem(Channel _channel,int _time)
        {
            channel = _channel;
            time = _time;
            retryCount = 1;
        }
    }
}
