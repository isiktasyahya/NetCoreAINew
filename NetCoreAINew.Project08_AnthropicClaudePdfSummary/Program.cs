using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using UglyToad.PdfPig;

class Program
{
    static async Task Main(string[] args)
    {
        string pdfPath = "C:\\Users\\isikt\\OneDrive\\Masaüstü\\insanNeyleYasar.pdf";

        var apikey = "Claude_ApiKey";

        if (!File.Exists(pdfPath))
        {
            Console.WriteLine("Pdf Dosyası Bulunamadı! ");
            return;
        }

        string pdfText = "";

        using (var document = PdfDocument.Open(pdfPath))
        {
            foreach (var page in document.GetPages())
            {
                pdfText += page.Text + "\n";
            }
        }
        string prompt = $"Aşşağıdaki metni detaylıca özetlermisin? \n\n{pdfText}";

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
        var responseString =await response.Content.ReadAsStringAsync();

        var jsonDoc = JsonDocument.Parse(responseString);
        var cevap = jsonDoc.RootElement
            .GetProperty("content")[0]
            .GetProperty("text")
            .GetString();

        Console.WriteLine("🎯 Claude Pdf Özeti: ");
        Console.WriteLine(cevap);

    }
}
