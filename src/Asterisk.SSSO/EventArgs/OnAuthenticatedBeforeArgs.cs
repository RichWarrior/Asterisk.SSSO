using Asterisk.SSSO.Entities;

namespace Asterisk.SSSO.EventArgs
{
    public class OnAuthenticatedBeforeArgs : EventArgs
    {
        public OnAuthenticatedBeforeArgs(DtmfItem _dtmfItem) 
            : base(_dtmfItem)
        {
        }
    }
}
