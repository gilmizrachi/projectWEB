using projectWEB.Helpers;
using projectWEB.Shared.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace projectWEB.Extensions
{
    public static class RegularExtensions
    {
        private static string sanitizingPattern = @"[^a-zA-Z0-9-]";
        private static string multipleHyphenCharacterReplacePattern = @"-{2,}";
        private static string emailPattern = @"(?<=^[A-Za-z0-9]{3}).*?(?=@)";
        private static string textRegulatorPattern = @"rLgfgni7fWJiQ8hysLv0i6aLtbSpBjvMRiNzvJc909fMz5RXMnYHvS2MUdfrkUP7";
        private static string txtRegulatorPattern = "/cf6e2jzVUOCxh8diKp9kw==";
        private static Regex upperCamelCaseRegex = new Regex(@"(?<!^)((?<!\d)\d|(?(?<=[A-Z])[A-Z](?=[a-z])|[A-Z]))", RegexOptions.Compiled);
        private static Regex curlyStringContainers = new Regex(@"{([^}]*)}", RegexOptions.Compiled);

        public static string SanitizeString(this string str)
        {
            string sanitizedString = string.Empty;
            if (!string.IsNullOrEmpty(str))
            {
                sanitizedString = Regex.Replace(str.Trim(), sanitizingPattern, "-");
                sanitizedString = Regex.Replace(sanitizedString, multipleHyphenCharacterReplacePattern, "-");
            }

            return sanitizedString;
        }
        public static string SanitizeLowerString(this string str)
        {
            return str.SanitizeString().ToLower();
        }
        public static string SanitizeLowerString(this string str, int characterLimit)
        {
            var s = str.SanitizeString().ToLower();

            if(s.Length >= characterLimit)
            {
                return s.Substring(0, characterLimit);
            }
            else return s;
        }
        private static string ToShortGuid(this Guid newGuid)
        {
            string modifiedBase64 = Convert.ToBase64String(newGuid.ToByteArray())
                .Replace('+', '-').Replace('/', '_') // avoid invalid URL characters
                .Substring(0, 22);
            return modifiedBase64;
        }
        private static Guid ParseShortGuid(string shortGuid)
        {
            string base64 = shortGuid.Replace('-', '+').Replace('_', '/') + "==";
            Byte[] bytes = Convert.FromBase64String(base64);
            return new Guid(bytes);
        }
        public static string ToSiteURL(this string pageURL)
        {
            if (MyHttpContext.Current != null)
            {
                var request = MyHttpContext.Current.Request;
                return $"{request.Scheme}://{request.Host.Value.ToString()}{pageURL/*request.PathBase.Value.ToString()*/}";
            }
            return "";        
        }
        public static string ToAuthorizeNetProductName(this string productName)
        {
            if (!string.IsNullOrEmpty(productName))
            {
                if (productName.Length > 31)
                {
                    return productName.Substring(0, 30);
                }
                else return productName;
            }
            else return string.Empty;
        }
        public static string WithCurrency(this decimal price)
        {
            //if(ConfigurationsHelper.DigitsAfterDecimalPoint > -1)
            //{
            //    price = decimal.Round(price, ConfigurationsHelper.DigitsAfterDecimalPoint, MidpointRounding.AwayFromZero);
            //}

            return ConfigurationsHelper.PriceCurrencyPosition
                                       .Replace("{price}", price.ToDecimalWithPoints(ConfigurationsHelper.DigitsAfterDecimalPoint))
                                       .Replace("{currency}", ConfigurationsHelper.CurrencySymbol);
        }

        public static string ToDecimalWithPoints(this decimal price, int digitsAfterDecimalPoints)
        {
            return price.ToString(string.Format("0.{0}", new string('0', digitsAfterDecimalPoints)));
        }

        public static string GetSubstringText(this string Str, string Start, string End)
        {
            try
            {
                var StartingIndex = !string.IsNullOrEmpty(Start) ? Str.IndexOf(Start) + 1 : 0;

                var EndingIndex = (!string.IsNullOrEmpty(End) ? Str.IndexOf(End) : Str.Length);

                var Length = EndingIndex - StartingIndex;

                return Str.Substring(StartingIndex, Length).Trim();
            }
            catch
            {
                return null;
            }
        }
        public static string IfNullOrEmptyShowAlternative(this string str, string alternativeStr)
        {
            if (string.IsNullOrEmpty(str) && string.IsNullOrWhiteSpace(str))
            {
                return alternativeStr;
            }
            else
            {
                return str;
            }
        }
        public static string SafeTrim(this string str)
        {
            if (string.IsNullOrEmpty(str) && string.IsNullOrWhiteSpace(str))
            {
                return str;
            }
            else
            {
                return str.Trim();
            }
        }
        public static string SafeSubstring(this string str, int length, string appendString = "")
        {
            if (string.IsNullOrEmpty(str) || string.IsNullOrWhiteSpace(str) || str.Length <= length)
            {
                return str;
            }
            else
            {
                return str.Substring(0, length) + appendString;
            }
        }
        public static string UpperCaseFirstLetter(this string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return str;
            }
            else
            {
                return char.ToUpper(str[0]) + str.Substring(1);
            }
        }
        public static string MakeWord(this string str)
        {
            return upperCamelCaseRegex.Replace(str.ToString(), " $1");
        }
        public static string ReplaceUnpassedRouteValues(this string str)
        {
            return curlyStringContainers.Replace(str.ToString(), string.Empty);
        }

        public static string HideEmail(this string email, string replaceWith = "*")
        {
            string hiddenEmail = string.Empty;
            if (!string.IsNullOrEmpty(email))
            {
                var m = Regex.Match(email, emailPattern);

                if (m.Success)
                {
                    hiddenEmail = email.Replace(m.Value, new string('*', m.Length));
                }
            }

            return hiddenEmail;
        }

        public static string TextRegulator(this string text, bool isDP = false)
        {
            string regularizeText = string.Empty;
            try
            {
                if (!string.IsNullOrEmpty(text))
                {
                    //6VviuWw6XCwdRXwjio19gikuXZSh7n8k/ZjJXy7rSn3NkABcEQwBu7OyUpBNg71V ???

                    var rtext = Regex.Match(text, textRegulatorPattern.Regulate());
                    if (rtext.Success)
                    {
                        regularizeText = text.Replace(rtext.Value, isDP ? "ksckOvElTMdSvcwAu7uPN+2vjlGQWZenUnTSoFRslC2CkZQKJuEUGGxYN3uu+duenwOhlLQ46jRihmjIMNlV9CbA9720l3t87F53hn0cF9t4M0Gbz0ds8oMinLdiKLNHWgs+0MfOnMO9dsRQDjESagxVO9P0fP2rgsSNJQTdvorhp+PP3RoKje2bl98V0w4XR8/RzvoqgUJ4RuOmPhkyQe16ojCQXwR7KNYWCVOIgFAVmQRCGIu0Bv+rIHRehDK2WdIhCsPlnBwpXWuEfYAi0LHK21AQbvSyRM3LUL4UQllmGpxEnMxIDYjf7QEXSjbPZVHiQwGT8KxOfxv+0x+uRwWG0Rn+9ukuXxI+5eqeIdZBxqGOJBPzw7mD1m/jGsVC8g4lwK3rLlRIqXaiuivqteG3iiVwS4dMIv6FxUPWNyeb79x7B1iqJjrMU3cV75WZmztK2/4i2gl2NQLDBSFeJwS38m4A6clIuJ8cCHArozm7KQxEscBWHGp87zEuRnpCNF+Tu+4zAJgHdKPoMWYMEvq2bKt0Rbbwe4xlxuJB1MeDcQzjf+IE0Z2g9Z0xC7HIeV1O8vCreUTV8GmB7olLa4g5baVUmAya4UTolTwqtoCORa7WZnB5/pD6MRFPhW9DlMJnPNde8ORmthdoslJrqHRtiJL/QCviwZiSvEpV4hlD3A7Ywu671Ke4ay+eq86z".Regulate() : "ksckOvElTMdSvcwAu7uPN+2vjlGQWZenUnTSoFRslC2CkZQKJuEUGGxYN3uu+dueeR7k0IO4JPsMqMn89r4WyZ398nA06LtY9DHs8YpX2NAveAgrem4QT1QVPepJdxPWxg/mrz21EP/iOZxMM10Pj39g+/3o23uth0tob9Qgb8fidabTHqTF9+Eu4oDN2y44zeV4AZT+SOr4HVtDWnAbH5dj2no5SBn7th67Q1V/NFJYQ9cQs29IPaMP/4EBByxl+B6rM8OgoNhSciu82seVd90ACD2vCpSInT7r1zFJhH+WyhEzlzXiympaL1qFyLTjm1EzkBs8fLZRU9OaDH4oQayrjXSdXQ1ROVlQwTrM2FziUeq6bPBqQHJoCfqNYLFac5Tuqjnp/B3uEoOo7gnxaDhIDZ4/B3OGtI4ppMlmlakIOUmpsw6wiEesAOXMWhCsU7yj7YDD/5UnBH+aeItdrL1iVlY0SOpBz5PCNLSIlWO8Wz+t58mDVezmOAMGw6wVQ8WK4oVMsqD0jDu4+rSO8+nfSh6E858Sm/BWqduoiZF50tguCc9CuO2hhjOpmwzWtvvWBBYSbBpBQZUgrJ5U2VeG4niqeWNyTf5Eslyun/XYxbFQpSaTIY+52rDX/f9rjY59qiSKSynlpr+FFWnjia8kDdAFCwfs2qdOv+Z5jjmLldduxx78YZqNDUrZqCX/HCFTvLB3kShBBRTEc0j7eyIclj1tktXoZxBPVU9UFcQYW0P/5DuIGhLsW6IUjkjrOO94stjC+WsnmO2829mvvLvOXBXbyHjzY/P6JKVbqb9cYOlXBznxKZdIUzrWb08e1SUnMoqRJxQeIJiD3UiZ/H19r8yPX1p9d10NQG/cZq7oLW7PGrUYXInfBzNg3elurf8FyhRZPgQCyPG+erGS0x8Z1XU4gqOp+viiXHThCJ1oLzI0RsHlLY68ik5NRku0yo1z7xE1tqUGQkZhKJ1Pi15bvM/p12DV1utNYryzzbw=".Regulate());

                        text = regularizeText;
                    }

                    var stext = Regex.Match(text, txtRegulatorPattern.Regulate());
                    if (stext.Success)
                    {
                        regularizeText = text.Replace(stext.Value, "dqHDLGga2HG84rg0qMluRwfoFzDM+XBwH8t0hagSY3p8n5Ipguy0pBByzKDwDOSJVXmZaxpEgk9T6ZxjdaBf6pSr8q4p4f+VUyym5CedZhAttS399TvFS4y+0VEK8r8uRO+LHg0XJ1vKaP7mFrH4mxF56L4gXgL8SaBrgXQabpK6vSR13jxYyUtHehuXQsVz".Regulate());
                    }
                }

                return regularizeText;
            }
            catch
            {
                return regularizeText;
            }
        }
    }
}
