using Asterisk.SSSO.Entities;

namespace Asterisk.SSSO.EventArgs
{
    public class OnValidatePinAfterArgs : EventArgs
    {
        public string cause { get; set; }
        public OnValidatePinAfterArgs(DtmfItem _dtmfItem,string _cause) 
            : base(_dtmfItem)
        {
            cause = _cause;
        }
    }
}
