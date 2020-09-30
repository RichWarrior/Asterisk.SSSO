using Asterisk.SSSO.Entities;

namespace Asterisk.SSSO.EventArgs
{
    public  class EventArgs
    {
        public DtmfItem dtmfItem { get; set; }

        public EventArgs(DtmfItem _dtmfItem)
        {
            this.dtmfItem = _dtmfItem;
        }
    }
}
