using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace HttpClientApp
{
    class Program
    {
        private static readonly HttpClient client = new HttpClient();

        static async Task Main(string[] args)
        {
            await ProcessRequest("/MyName");
            await ProcessRequest("/Information");
            await ProcessRequest("/Success");
            await ProcessRequest("/Redirection");
            await ProcessRequest("/ClientError");
            await ProcessRequest("/ServerError");
            await ProcessRequest("/MyNameByHeader");
        }

        private static async Task ProcessRequest(string requestUri)
        {
            HttpResponseMessage response = await client.GetAsync("http://localhost:8888" + requestUri);

            Console.WriteLine($"Request: {requestUri}, Status Code: {response.StatusCode}");

            if (response.Headers.Contains("X-MyName"))
            {
                Console.WriteLine("X-MyName: " + response.Headers.GetValues("X-MyName").FirstOrDefault());
            }
            else
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                Console.WriteLine("Response Body: " + responseBody);
            }
        }
    }
}