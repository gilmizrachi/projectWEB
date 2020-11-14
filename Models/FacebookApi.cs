using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace projectWEB.Models
{
    public class FacebookApi
    {
        private readonly string FB_PAGE_ID;
        private readonly string FB_ACCESS_TOKEN;
        private const string FB_BASE_ADDRESS = "https://graph.facebook.com/";

        public FacebookApi(string pageId, string accessToken)
        {
            FB_PAGE_ID = pageId;
            FB_ACCESS_TOKEN = accessToken;
        }

        public FacebookApi()
        {
            FB_PAGE_ID = "109520194305954";
            FB_ACCESS_TOKEN = "EAAaw3rXu3I8BAFd4UyNX3FSt2yOJtFPwfmZBlvkNxyOtz9QtACGG3BN08D8xLkbXo8acZCEvPyHrOHZBgxMub5XdKqPAqoJ1kT9l6dtAeFD9g9wx9Hee7ZBufwKZB4cMJOY7PgKP7i4yorGyPXcNXgRiMSFRII1c7qF6BZC2zZANUpUDqFUKspHZBl09eSqZAmY8ZD";
        }

        public async Task<string> PublishMessage(string message)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(FB_BASE_ADDRESS);

                var parametters = new Dictionary<string, string>
                {
                    { "access_token", FB_ACCESS_TOKEN },
                    { "message", message }
                };
                var encodedContent = new FormUrlEncodedContent(parametters);

                var result = await httpClient.PostAsync($"{FB_PAGE_ID}/feed", encodedContent);
                var msg = result.EnsureSuccessStatusCode();
                return await msg.Content.ReadAsStringAsync();
            }

        }
    }
}
