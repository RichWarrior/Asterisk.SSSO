using Asterisk.SSSO.Entities;
using Asterisk.SSSO.EventArgs;
using Asterisk.SSSO.Utilities;
using AsterNet.Standard;
using AsterNet.Standard.Models;

namespace Asterisk.SSSO.Sample
{
    public class SampleSSSO : BaseSSSO
    {
        ARIMain ariMain; 

        public SampleSSSO()
        {
            StasisEndpoint endpoint = new StasisEndpoint("127.0.0.1",7004,"root","123",false);
            ariMain = new ARIMain(endpoint, "AsteriskSSSO", this);

            this.MaxWaitingTime = 30;
            this.RetryCount = 3;
            this.PinLength = 4;

            DoWork();
        }

        public override bool ValidateNumber(IAriClient ariClient, Channel channel)
        {
            //Arayan Numarayı Kontrol Edilecek Kısım
            return false;
        }

        public override bool ValidatePin(DtmfItem dtmfItem)
        {
            //Girilen Pin'i Kontrol Edilecek Kısım
            return false;
        }

        private void DoWork()
        {
            this.OnValidateNumberBefore = (OnValidateNumberBeforeArgs e) =>
            {
                //Numarayı Kontrol Etmeden Önce Yapılacaklar
            };

            this.OnValidateNumberAfter = (OnValidateNumberAfterArgs e) =>
            {
                //Numarayı Kontrol Ettikten Sonra Yapılacaklar
            };

            this.OnAuthenticationFailedBefore = (OnAuthenticationFailedBeforeArgs e) =>
            {
                //Oturum Açılamadan Önce Yapılacaklar
            };

            this.OnAuthenticationFailedEventHandler += (sender, e) =>
            {
              //Oturum Açılmama Esnasında Yapılacaklar
            };

            this.OnAuthenticationFailedAfter = (OnAuthenticationFailedAfterArgs e) =>
            {
               //Oturum Açılmama İşleminden Sonra Yapılacaklar
            };

            this.OnValidatePinBefore = (OnValidatePinBeforeArgs e) =>
            {
                //Pin Kontrol Etmeden Önce Yapılacaklar
            };

            this.OnValidatePinAfter = (OnValidatePinAfterArgs e) =>
            {
               //Pin Kontrol Ettikten SOnra Yapılacaklar
            };

            this.OnAuthenticatedBefore = (OnAuthenticatedBeforeArgs e) =>
            {
                //Oturum Açma Başarılı Olmadan Önce Yapılacaklar
            };

            this.OnAuthenticatedEventHandler += (sender, e) =>
            {
                //Oturum Açma Başarılı Olduğu Esnada Yapılacaklar
            };

            this.OnAuthenticatedAfter = (OnAuthenticatedAfterArgs e) =>
            {
                //Oturum Açma Başarılı Olduktan Sonra Yapılacaklar
            };
        }
    }
}
