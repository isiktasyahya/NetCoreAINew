using NAudio.Wave;
using System.Net.Http.Headers;
using System.Speech.Synthesis;
using System.Text;
using System.Text.Json;
class Program
{
    static async Task Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.WriteLine("🎤 Sesli Chat Bot Başladı. Konuşmak İçinde Enter Tuşuna Basınız..");
        while (true)
        {
            Console.ReadLine();
            string audioFilePath = "recorded.wav"; // DÜZELTME: .vav → .wav
            // 1.Adım:  Mikrof kaydı al
            Console.WriteLine("🎤 Konuşmaya Başlayın...");
            RecordAudio(audioFilePath);
            Console.WriteLine("🛑 Kayıt Tamamlandı...");
            // 2.Adım:  OpenAI Whisper Api ile yazıya çevir.
            string transciption = await TranscribeAudioAsync(audioFilePath);
            Console.WriteLine($"🙍‍ Sen: {transciption}");
            //3.Adım:  ChatGbtye Soruyu Gönder
            string reply = await AskChatGqtAsync(transciption);
            Console.WriteLine($"🤖 ChatBot: {reply}");
            // 4. Adım: Yanıtı Seslendir.
            var synth = new SpeechSynthesizer();
            synth.Speak(reply);
        }
    }
    static void RecordAudio(string outputFilePath)
    {
        using var waveIn = new WaveInEvent();
        waveIn.WaveFormat = new WaveFormat(16000, 1);
        using var writer = new WaveFileWriter(outputFilePath, waveIn.WaveFormat);
        waveIn.DataAvailable += (s, a) => writer.Write(a.Buffer, 0, a.BytesRecorded);
        waveIn.StartRecording();
        Thread.Sleep(5000);
        waveIn.StopRecording();
    }
    static async Task<string> TranscribeAudioAsync(string audioFilePath)
    {
        string apiKey = "OpenAI_ApiKey";
        using var httpClient = new HttpClient();
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);
        using var form = new MultipartFormDataContent();
        using var fs = File.OpenRead(audioFilePath);
        form.Add(new StreamContent(fs), "file", Path.GetFileName(audioFilePath));
        form.Add(new StringContent("whisper-1"), "model");
        var response = await httpClient.PostAsync("https://api.openai.com/v1/audio/transcriptions", form);
        var result = await response.Content.ReadAsStringAsync();

        // DÜZELTME: HTTP hata kontrolü + TryGetProperty
        if (!response.IsSuccessStatusCode)
            throw new Exception($"Whisper API Hatası ({response.StatusCode}): {result}");

        using var doc = JsonDocument.Parse(result);
        if (doc.RootElement.TryGetProperty("text", out var textElement))
            return textElement.GetString();

        throw new Exception($"Beklenmeyen API yanıtı: {result}");
    }
    static async Task<string> AskChatGqtAsync(string userMessage)
    {
        string apiKey = "OpenAI_ApiKey";
        using var httpClient = new HttpClient();
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);
        var payload = new
        {
            model = "gpt-4",
            messages = new[]
            {
            new
            {
                role = "user",
                content = userMessage
            }
        }
        };
        var content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");
        var response = await httpClient.PostAsync("https://api.openai.com/v1/chat/completions", content);
        var result = await response.Content.ReadAsStringAsync();

        // DÜZELTME: HTTP hata kontrolü
        if (!response.IsSuccessStatusCode)
            throw new Exception($"ChatGPT API Hatası ({response.StatusCode}): {result}");

        using var doc = JsonDocument.Parse(result);
        return doc.RootElement.GetProperty("choices")[0].GetProperty("message").GetProperty("content").GetString();
    }
}