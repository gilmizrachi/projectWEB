using Microsoft.AspNetCore.Http;
using projectWEB.Shared.Helpers;

using System.Net;
using Newtonsoft.Json;

namespace projectWEB.Helpers
{
    public static class CookiesHelper
    {
        private const string COOKIE_NAME = "ObjValue";

        public static T GetObjectFromCookie<T>(string key)
        {
            T retVal = default(T);
            string strValue = GetStringFromCookie(key);
            if (strValue != "")
            {
                retVal = DeSerializeObject<T>(strValue);
            }
            return retVal;
        }

        private static string GetStringFromCookie(string key)
        {
            string retVal;
            string myCookie = MyHttpContext.Current.Request.Cookies[COOKIE_NAME];
            if (!string.IsNullOrEmpty(myCookie))
            {
                if(MyHttpContext.Current.Request.Cookies.TryGetValue(key, out retVal))
                {
                    return WebUtility.UrlDecode(retVal);
                }
            }
            return "";
        }

        internal static string SerializeObject<T>(this T toSerialize)
        {
            return JsonConvert.SerializeObject(toSerialize);
        }

        internal static T DeSerializeObject<T>(string objValue)
        {
            return JsonConvert.DeserializeObject<T>(objValue);
        }

    }
}
