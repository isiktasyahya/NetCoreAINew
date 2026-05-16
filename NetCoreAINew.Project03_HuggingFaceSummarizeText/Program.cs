
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

Console.Write("Enter Your Text Here: ");

var apikey = "HuggingFace_ApiKey";
var inputText = Console.ReadLine();

var requestData = new
{
    inputs = inputText
};

var json = JsonSerializer.Serialize(requestData);
var content = new StringContent(json, Encoding.UTF8, "application/json");


using var client = new HttpClient();
client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",apikey);

var response = await client.PostAsync("https://router.huggingface.co/hf-inference/models/facebook/bart-large-cnn", content);
var responseContent = await response.Content.ReadAsStringAsync();


Console.WriteLine("📖 Text Summarize: ");
Console.WriteLine($"{responseContent}");


