using System.Text.RegularExpressions;

namespace Asterisk.SSSO.Utilities
{
    public static class Segment
    {
        public static string Control(string phoneNumber)
        {
            phoneNumber = phoneNumber.Replace(" ", "");
            var _Reg = Regex.Match(phoneNumber, @"[0-9]*");
            phoneNumber = _Reg.Groups[0].Value;

            string _PhoneNr = "";

            if (phoneNumber.Length > 2)
            {
                string _ContralChar = phoneNumber.Substring(0, 2);
                if ((_ContralChar == "00") && phoneNumber.Length > 7)
                {
                    _PhoneNr = phoneNumber.Substring(2);
                }
                else if ((_ContralChar == "90") && phoneNumber.Length == 12)
                {
                    _PhoneNr = phoneNumber;
                }
                else if ((_ContralChar == "02" || _ContralChar == "03" || _ContralChar == "04" || _ContralChar == "05" || _ContralChar == "08") && phoneNumber.Length == 11)
                {
                    _PhoneNr = "9" + phoneNumber;
                }
                else if ((_ContralChar[0].ToString() == "2" || _ContralChar[0].ToString() == "3" || _ContralChar[0].ToString() == "4" || _ContralChar[0].ToString() == "5" || _ContralChar[0].ToString() == "8") && phoneNumber.Length == 10)
                {
                    _PhoneNr = "90" + phoneNumber;
                }
                else if ((_ContralChar == "44") && phoneNumber.Length == 7)
                {
                    _PhoneNr = "90" + phoneNumber;
                }
                else if (phoneNumber.Length == 4)
                {
                    _PhoneNr = phoneNumber;
                }
            }
            return _PhoneNr;
        }

    }
}
