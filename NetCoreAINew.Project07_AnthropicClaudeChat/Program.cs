
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

class Program
{
    static async Task Main(string[] args)
    {
        var apikey = "Claude_ApiKey";

        Console.Write("Lütfen Sormak istediğiniz soruyu yazınız: ");
        var prompt = Console.ReadLine();

        using var client = new HttpClient();

        client.BaseAddress = new Uri("https://api.anthropic.com");

        client.DefaultRequestHeaders.Add("x-api-key", apikey);

        client.DefaultRequestHeaders.Add("anthropic-version", "2023-06-01");

        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        var requestBody = new
        {
            model = "claude-opus-4-5",
            max_tokens = 1000,
            temperature = 0.7,
            messages = new[]
            {
                new
                {
                    role="user",
                    content=prompt
                }
            }
        };

        var jsonContent = new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json");

        var response = await client.PostAsync("v1/messages", jsonContent);

        var responseString = await response.Content.ReadAsStringAsync();
        var jsonDoc = JsonDocument.Parse(responseString);
        var cevap = jsonDoc.RootElement
            .GetProperty("content")[0]
            .GetProperty("text")
            .GetString();

        Console.WriteLine("🎯 Claude Cevabı: ");
        Console.WriteLine(cevap);
    }
}
/*
 * Uri, bir internet/dosya adresini temsil eden C# sınıfıdır.
  
 * BaseAddress = HttpClient sınıfının bir özelliğidir. Tüm istekler için ortak adres tanımlamanızı sağlar.
   BaseAddress sayesinde her seferinde tam URL yazmak zorunda kalmazsınız, sadece endpoint'i (v1/messages gibi) yazmanız yeterli olur.

 * Accept, HTTP isteğinde sunucuya "bana hangi formatta veri gönder" dediğiniz bir request header'ıdır.

 */