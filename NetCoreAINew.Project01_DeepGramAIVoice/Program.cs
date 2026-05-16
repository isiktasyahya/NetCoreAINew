
// Amaç ses dosyası gönderip bu ses dosyasını metne çevvirtmeliyiz.

using System.Net.Http.Headers;
using System.Text.Json;

var apikey = "ApiKey";

var filePath = "testtr.mp3";

// Projede Dosya Yoksa Mesaj Göster demektir.
if (!File.Exists(filePath))
{
    Console.WriteLine("Dosya Bulunamadı! ");
    return;
}

//HTTP isteğine kimlik doğrulama (authentication) bilgisi ekliyor,Ben bu API’ye boş gitmiyorum, işte benim anahtarım (apikey) burada.

using var client = new HttpClient();
client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Token", apikey);

// Dosyayı aç, işim bitince otomatik kapat (dispose et).
using var fileStream = File.OpenRead(filePath);

//Elimdeki fileStream’i al, bunu HTTP isteğinde gönderilecek body (içerik) haline çevir
var content = new StreamContent(fileStream);

// Benim gönderdiğim veri MP3 ses dosyasıdır” demek.
content.Headers.ContentType = new MediaTypeHeaderValue("audio/mp3");

// Deepgrama İstek atarız.
var response = await client.PostAsync("https://api.deepgram.com/v1/listen?language=tr", content);
var json = await response.Content.ReadAsStringAsync();

// Gelen Yanıtı alırız.
try
{
    // Elimdeki JSON string’i al, üzerinde gezebileceğim bir yapı(DOM) haline getir.
    var doc = JsonDocument.Parse(json);

    var transcript = doc.RootElement
        .GetProperty("results")
        .GetProperty("channels")[0]
        .GetProperty("alternatives")[0]
        .GetProperty("transcript")
        .GetString();

    Console.WriteLine();
    Console.WriteLine("Transcribe Metni: \n");
    Console.WriteLine(transcript);
}
catch (Exception ex)
{
    Console.WriteLine("JSON Çözümleme Sürecinde Bir Hata Oluştu.");
    Console.WriteLine(ex.Message);
    Console.WriteLine("\n Gelen Yanıt \n" + json);
}

