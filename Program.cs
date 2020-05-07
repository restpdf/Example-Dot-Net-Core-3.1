using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace restpdf.io
{
    class Program
    {
        static void Main(string[] args)
        {
            var ApiKey = "";

            var RequestBodyParameters = new
            {
                output = "data",
                url    = "https://www.google.co.uk"
            };

            var RequestBodyBody = new StringContent(JsonSerializer.Serialize(RequestBodyParameters), Encoding.UTF8, "application/json");

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("X-API-KEY", ApiKey);

                using (var result = client.PostAsync("https://api.restpdf.io/v1/pdf", RequestBodyBody).Result)
                {
                    if (result.IsSuccessStatusCode)
                    {
                        Console.WriteLine("Your PDF was converted.");

                        File.WriteAllBytes("google.pdf", result.Content.ReadAsByteArrayAsync().Result);
                    } else
                    {
                        Console.WriteLine("There was an error converting your PDF.");
                    }
                }
            }
        }
    }
}
