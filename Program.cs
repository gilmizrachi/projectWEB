using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using projectWEB.Models;

namespace projectWEB
{
    public class Program
    {
        public static void Main(string[] args)
        {
            PublishToFacebook();
            CreateHostBuilder(args).Build().Run();
        }
        private static void PublishToFacebook()
        {
            Facebook facebook = new Facebook("EAA9ZCZBIcsnZBEBAH4oDmIDCnJt0IDMI8frCpuXaWsQ5ZCXo6KGLAWaTdTHGcFIB9Kr0VgCpQJA9vsUkJthidZBgEA1M1DrohyaZAYoZC0ZBEYUrzHZB0L9O4X6EK32v5IW1iLnOWN3O1ZA5NCi2ns5lLj7AHEK8wWdc5EaizjchqpPNzDueZCvIqpzg20XbKI4HkLFlQtlgbgmamFGA6jQaKmY", "102526684953651");
            var rezText = Task.Run(async () =>
            {
                using (var http = new HttpClient())
                {
                    return await facebook.PublishSimplePost("First Post");
                }
            });
            var rezTextJson = JObject.Parse(rezText.Result.Item2);
            if (rezText.Result.Item1 != 200)
            {
                try // return error from JSON
                {
                    Console.WriteLine($"Error posting to Facebook. {rezTextJson["error"]["message"].Value<string>()}");
                    return;
                }
                catch (Exception ex) // return unknown error
                {
                    // log exception somewhere
                    Console.WriteLine($"Unknown error posting to Facebook. {ex.Message}");
                    return;
                }
            }
            Console.WriteLine(rezTextJson);
        }
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
