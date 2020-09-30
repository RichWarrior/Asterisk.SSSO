using Asterisk.SSSO.Entities;

namespace Asterisk.SSSO.EventArgs
{
    public class OnAuthenticatedAfterArgs : EventArgs
    {
        public OnAuthenticatedAfterArgs(DtmfItem _dtmfItem) 
            : base(_dtmfItem)
        {
        }
    }
}
