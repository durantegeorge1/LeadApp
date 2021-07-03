using System;
namespace LeadApp.Core.Extensions
{
    public static class StringExtensions
    {
        public static string ToPhoneNumber(this string phoneNumber)
        {
            if (string.IsNullOrEmpty(phoneNumber))
            {
                return string.Empty;
            }

            phoneNumber = phoneNumber.Replace(" ", "");
            if(phoneNumber.Length == 13)
            {
                string countryCode = phoneNumber[0..3];
                long phoneNumberDigits = long.Parse(phoneNumber[3..]);
                return string.Format("{0} {1:### ### ####}", countryCode, phoneNumberDigits);
            }
            else if(phoneNumber.Length == 12)
            {
                string countryCode = phoneNumber[0..2];
                long phoneNumberDigits = long.Parse(phoneNumber[2..]);
                return string.Format("{0} {1:### ### ####}", countryCode, phoneNumberDigits);
                //return string.Format("{0: +# ### ### ####}", phoneNumber);
            }
            else
            {
                long phoneNumberDigits = long.Parse(phoneNumber);
                return string.Format("+1 {0:### ### ####}", phoneNumberDigits);
            }
        }
    }
}
