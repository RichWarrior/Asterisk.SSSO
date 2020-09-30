using Asterisk.SSSO.Entities;

namespace Asterisk.SSSO.EventArgs
{
    public class OnAuthenticatedEventArgs : EventArgs
    {
        public string cause { get; set; }
        public OnAuthenticatedEventArgs(DtmfItem _dtmfItem,string _cause) 
            : base(_dtmfItem)
        {
            cause = _cause;
        }
    }
}
