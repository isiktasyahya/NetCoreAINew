
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

class Program
{
    static async Task Main(string[] args)
    {
        // Kodtaki türkçe karakterleri okumasına yardımcı olur.
        Console.OutputEncoding = Encoding.UTF8;

        Console.WriteLine("🤖 OpenAI Kod Asistanına Hoş Geldin \n ");
        Console.WriteLine("Kodunu yaz ve aşşağıdaki işlemlerden birini seç \n");
        Console.WriteLine("1 - Açıklama Üret");
        Console.WriteLine("2 - Refactor Et");
        Console.WriteLine("3 - Test Case Oluştur");

        Console.Write("\nSeçimin(1/2/3): ");
        var choice = Console.ReadLine();

        Console.WriteLine("\nKodunu Gir(Bitirmek için 'END' yaz): ");

        // StringBuilder, büyük veya sürekli değişen metinleri daha performanslı şekilde oluşturmak için kullanılan bir sınıftır. new, C#’ta bir nesneyi (object) bellekte oluşturmak için kullanılan anahtar kelimedir.
        StringBuilder userCode = new();

        string? line;
        // Burası kullanıcıdan satır satır kod almaya yarar ve END yazında durmayı anlatır.
        while ((line = Console.ReadLine()) != null && line.Trim() != "END")
        {
            userCode.AppendLine(line);
            // Trim() bir string’in başındaki ve sonundaki gereksiz boşlukları temizler.
        }

        string prompt = choice switch
        {
            "1" => $"Lütfen aşşağıdaki C# kodunu açıklayıcı şekilde açıkla: \n\n {userCode}",
            "2" => $"Lütfen aşşağıdaki C# kodunu daha temiz, okunabilir, ve iyi şekilde refector et: \n\n {userCode}",
            "3" => $"Lütfen aşşağıdaki C# kodu için Unit test case üret: \n\n {userCode}",
        };

        var result = await AskOpenAI(prompt);

        Console.WriteLine("\n 💬 OpenAI Yanıtı:\n ");
        Console.WriteLine(result);
    }

    static async Task<string> AskOpenAI(string prompt)
    {
        const string apiKey = "OpenAI_ApiKey";
        const string endpoint = "https://api.openai.com/v1/chat/completions";

        using var client = new HttpClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

        var requestBody = new
        {
            model = "gpt-4",
            messages = new[]
            {
                new
                {
                    role="system",
                    content="Sen uzman bir C# yazılım gelişticisisin. Kodları açıkla, düzelt veya task case üret."
                },
                new
                {
                    role="user",
                    content=prompt
                }
            },
            temperature = 0.5
        };
        var json = JsonSerializer.Serialize(requestBody);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await client.PostAsync(endpoint, content);
        var responseJson = await response.Content.ReadAsStringAsync();

        var doc = JsonDocument.Parse(responseJson);

        return doc.RootElement
            .GetProperty("choices")[0]
            .GetProperty("message")
            .GetProperty("content")
            .ToString();

    }
}










