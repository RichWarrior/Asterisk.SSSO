using Asterisk.SSSO.Entities;

namespace Asterisk.SSSO.EventArgs
{
    public class OnValidatePinBeforeArgs : EventArgs
    {
        public OnValidatePinBeforeArgs(DtmfItem _dtmfItem) 
            : base(_dtmfItem)
        {
        }
    }
}
