# 🤖 NetCoreAINew — .NET Core Yapay Zeka Entegrasyonları

> 20 farklı yapay zeka API'sini .NET Core ile entegre eden kapsamlı bir proje koleksiyonu.  
> Ses, görüntü, metin, NLP, video üretimi ve daha fazlasını içerir.

---

## 📁 Projeler

### 🎙️ Project01 — DeepGram AI Voice (Ses Tanıma)
DeepGram API kullanılarak ses dosyasından metin elde edilir.

**Örnek Çıktı:**
> *"bayramda hava nasıl olacak işte merak edilen soru bu hem tatil bölgesine gidenler hem de kurban bayramına bulundukları şehirde geçirenler için..."*

![DeepGram Output](https://github.com/isiktasyahya/NetCoreAINew/blob/main/images/1.png?raw=true)

---

### 😊 Project02 — HuggingFace Sentiment Analysis (Duygu Analizi)
Girilen metnin duygusunu (Pozitif/Negatif) ve güven skorunu döndürür.

**Örnek:** `"This software is terrible."` → **NEGATİF %95.97**

![Sentiment Analysis](https://github.com/isiktasyahya/NetCoreAINew/blob/main/images/2.png?raw=true)

---

### 📝 Project03 — HuggingFace Summarize Text (Metin Özetleme)
Uzun metinleri otomatik olarak özetler.

![Summarize Text](https://github.com/isiktasyahya/NetCoreAINew/blob/main/images/3.png?raw=true)

---

### 🏷️ Project04 — HuggingFace Named Entity Recognition (Varlık Tanıma)
Metindeki kişi, organizasyon ve konum gibi varlıkları tespit eder.

**Örnek:** `"Microsoft hired John Smith in London"`
- Microsoft → **ORG** (%99.85)
- John Smith → **PER** (%99.97)
- London → **LOC** (%99.95)

![NER Output](https://github.com/isiktasyahya/NetCoreAINew/blob/main/images/4.png?raw=true)

---

### ❓ Project05 — HuggingFace RoBERTa Base QA (Soru-Cevap)
Verilen bir bağlam içinde sorulan soruyu yanıtlar.

![QA Output](https://github.com/isiktasyahya/NetCoreAINew/blob/main/images/5.png?raw=true)

---

### 🚫 Project06 — HuggingFace ToxicBert (Toxicity Analizi)
Yorumların zararlı içerik kategorilerini (TOXIC, INSULT, OBSCENE vb.) analiz eder.

**Örnek:** `"You are an idiot and nobody likes you."`
- TOXIC → **%98.69**
- INSULT → **%94.8**

![ToxicBert Output](https://github.com/isiktasyahya/NetCoreAINew/blob/main/images/6.png?raw=true)

---

### 💬 Project07 — Anthropic Claude Chat
Claude API ile sohbet uygulaması. LinkedIn paylaşımı, içerik üretimi gibi görevler yapabilir.

![Claude Chat](https://github.com/isiktasyahya/NetCoreAINew/blob/main/images/7.png?raw=true)

---

### 📄 Project08 — Anthropic Claude PDF Summary (PDF Özetleme)
PDF dosyalarını Claude ile özetler. Tolstoy'un "İnsan Ne İle Yaşar" kitabı örneği.

![PDF Summary](https://github.com/isiktasyahya/NetCoreAINew/blob/main/images/8.png?raw=true)

---

### 📧 Project09 — Anthropic Claude Job Email (İş Başvuru E-postası)
Claude ile profesyonel iş başvuru e-postası oluşturur.

![Job Email](https://github.com/isiktasyahya/NetCoreAINew/blob/main/images/9.png?raw=true)

---

### 🖼️ Project10 — Replicate AI Draw Image (Görsel Üretimi)
Replicate API ile prompt'tan görsel üretir.

---

### 🔊 Project11 — Microsoft Azure Text To Speech (Metinden Ses)
Azure Cognitive Services ile metin seslendirilir.

---

### 👁️ Project12 — Microsoft Azure Computer Vision (Görüntü Analizi)
Azure ile görüntü içeriği analiz edilir ve açıklanır.

![Computer Vision](https://github.com/isiktasyahya/NetCoreAINew/blob/main/images/12.png?raw=true)

---

### 🔍 Project13 — Microsoft Azure Computer Vision Objects (Nesne Tespiti)
Görseldeki nesneleri tespit eder ve güven skorlarını döndürür.

**Örnek:** Maple (%58.70), Person (%89.80)

![Object Detection](https://github.com/isiktasyahya/NetCoreAINew/blob/main/images/13.png?raw=true)

---

### 🌐 Project14 — Google Gemini Question Answer (Soru-Cevap)
Google Gemini API ile soru yanıtlama uygulaması.

---

### 🎭 Project15 — Google Gemini Role Simulation (Rol Simülasyonu)
Gemini ile farklı rollerde (Psikolog, Maç Yorumcusu, Finansal Uzman vb.) simülasyon yapılır.

**Örnek:** Maç Yorumcusu olarak Galatasaray - Fenerbahçe derbi yorumu.

![Role Simulation](https://github.com/isiktasyahya/NetCoreAINew/blob/main/images/15.png?raw=true)

---

### 🤖 Project16 — Google Gemini Auto Agent Prompt (Oto Ajan)
Gemini ile adım adım soru sorarak içerik planı oluşturan ajan sistemi.

![Auto Agent](https://github.com/isiktasyahya/NetCoreAINew/blob/main/images/16.png?raw=true)

---

### 💻 Project17 — OpenAI Code Editor (Kod Asistanı)
OpenAI ile kod açıklama, refactor ve test case oluşturma.

![Code Editor](https://github.com/isiktasyahya/NetCoreAINew/blob/main/images/17.png?raw=true)

---

### 🎨 Project18 — Stability AI Stable Diffusion (Görsel Üretimi)
Stability AI ile prompt'tan gerçekçi görsel üretir.

**Örnek Prompt:** *"A realistic cat wearing sunglasses on a beach, cinematic lighting, ultra detailed"*

![Stability AI](https://github.com/isiktasyahya/NetCoreAINew/blob/main/images/18_1.png?raw=true)

---

### 🎬 Project19 — Replicate AI Create Video From Prompt (Video Üretimi)
Replicate API ile metin prompt'undan video üretir.

**Örnek:** Ortaçağ kalesi sahnesinden sinematik video oluşturuldu.

![Video Generation](https://github.com/isiktasyahya/NetCoreAINew/blob/main/images/19.png?raw=true)

---

### 🎤 Project20 — OpenAI Speech ChatBot (Sesli Sohbet Botu)
OpenAI Whisper ile ses kaydeder, transkrip eder ve yanıt üretir.

**Örnek:** *"How are you? My name is Yahya."* → ChatBot sesli yanıt verir.

![Speech ChatBot](https://github.com/isiktasyahya/NetCoreAINew/blob/main/images/20.png?raw=true)

---

## 🛠️ Kullanılan Teknolojiler

| Teknoloji | Kullanım Alanı |
|-----------|---------------|
| .NET 9 / C# | Ana geliştirme platformu |
| DeepGram | Ses tanıma (STT) |
| Hugging Face | NLP modelleri |
| Anthropic Claude | Chat, PDF özet, e-posta |
| Google Gemini | Soru-cevap, rol simülasyonu, ajan |
| Microsoft Azure AI | Görüntü analizi, metin-ses |
| OpenAI | Kod asistanı, sesli chatbot |
| Replicate | Görsel ve video üretimi |
| Stability AI | Stable Diffusion görsel üretimi |

---

## 🚀 Kurulum

```bash
git clone https://github.com/isiktasyahya/NetCoreAINew.git
cd NetCoreAINew
```

Her projede `Program.cs` dosyasındaki `YOUR_API_KEY_HERE` alanlarını kendi API key'lerinizle değiştirin.

---

## 📜 Lisans

MIT License © 2025 isiktasyahya
