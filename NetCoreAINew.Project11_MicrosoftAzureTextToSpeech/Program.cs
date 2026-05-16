using System.Net.Http.Headers;
using System.Text;

class Program
{
    static async Task Main(string[] args)
    {
        string subscriptionKey = "MS_Azure-ApiKey";  // Azure Apikey buraya gelir.

        string region = "westeurope"; // Burada  kullanılan bölge yazılır.

        string tokenEndPoint = $"https://{region}.api.cognitive.microsoft.com/sts/v1.0/issueToken";

        // string tokenEndpoint = $"https://{region}.api.cognitive.microsoft.com/sts/v1.0/issuetoken"; // İstek yapılıcak yer için hazırlanılan ifade

        var token = await GetTokenAsync(subscriptionKey, tokenEndPoint);

        string userText = "Selamlar bu bir denemedir. Umarım başarılı olur."; // Burası kullanıcıdan da alınabilir.

        await SynthesizeSpeechAsync(token, region, userText);
    }

    static async Task<string> GetTokenAsync(string key, string endPoint)
    {
        using var client = new HttpClient();

        client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", key);

        var response = await client.PostAsync(endPoint, null);

        return await response.Content.ReadAsStringAsync();
    }

    // Burada metin sese dönüşür.
    static async Task SynthesizeSpeechAsync(string token, string region, string text)
    {
        using var client = new HttpClient();

        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        client.DefaultRequestHeaders.Add("User-Agent", "AzureTTSClient");

        client.DefaultRequestHeaders.Add("X-Microsoft-OutputFormat", "riff-16khz-16bit-mono-pcm");

        // Burada seslendirme için farklı sesler vs kullanılabilir. AI'dan destek alarak, farklı dillerde ki ve tonlara ulaşabiliriz.
        string ssml = $@"
<speak version='1.0' xml:lang='en-US'>
  <voice xml:lang='tr-TR' name='tr-TR-AhmetNeural'>{text}</voice>
</speak>";

        var content = new StringContent(ssml, Encoding.UTF8, "application/ssml+xml");
        var result = await client.PostAsync($"https://{region}.tts.speech.microsoft.com/cognitiveservices/v1", content);

        if (result.IsSuccessStatusCode)
        {
            var audioBytes = await result.Content.ReadAsByteArrayAsync();
            File.WriteAllBytes("output2.wav", audioBytes); // Burada ses dosyası oluştururuz.
            Console.WriteLine("Ses dosyası oluşturuldu: output.wav");
        }
        else
        {
            Console.WriteLine("Hata: " + result.StatusCode);
            Console.WriteLine(await result.Content.ReadAsStringAsync());
        }
    }
}

