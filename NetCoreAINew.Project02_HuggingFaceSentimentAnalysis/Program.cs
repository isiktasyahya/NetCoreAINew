using System.Globalization;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

var apiKey = "HuggingFace_ApiKey";

Console.Write("Enter your text here: ");
var text = Console.ReadLine();

var modelUrl = "https://router.huggingface.co/hf-inference/models/cardiffnlp/twitter-roberta-base-sentiment-latest";

using var client = new HttpClient();
client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

var json = JsonSerializer.Serialize(new { inputs = text });
var content = new StringContent(json, Encoding.UTF8, "application/json");

var response = await client.PostAsync(modelUrl, content);
var result = await response.Content.ReadAsStringAsync();

if (!response.IsSuccessStatusCode)
{
    Console.WriteLine("❌ API Hatası:");
    Console.WriteLine(result);
    return;
}

using var doc = JsonDocument.Parse(result);

if (doc.RootElement.ValueKind != JsonValueKind.Array || doc.RootElement.GetArrayLength() == 0)
{
    Console.WriteLine("❌ Beklenmeyen veri formatı:");
    Console.WriteLine(result);
    return;
}

var items = doc.RootElement[0];

var topLabel = items
    .EnumerateArray()
    .OrderByDescending(e => e.GetProperty("score").GetDouble())
    .First();

var label = topLabel.GetProperty("label").GetString()?.ToLower();
var score = topLabel.GetProperty("score").GetDouble();

string labelText = label switch
{
    "label_0" or "negative" => "NEGATİF 😡",
    "label_1" or "neutral" => "NÖTR 😐",
    "label_2" or "positive" => "POZİTİF 😍",
    _ => $"BİLİNMİYOR (Label: {label})"
};

Console.OutputEncoding = Encoding.UTF8;

Console.WriteLine("\n🗒️ Girdi Metni:");
Console.WriteLine(text);

Console.WriteLine("\n🎭 Duygu Analizi:");
Console.WriteLine($"✅ Duygu Durumu: {labelText}");
Console.WriteLine($"🎯 Güven Skoru: %{(score * 100).ToString("F2", CultureInfo.InvariantCulture)}");