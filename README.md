# 🏢 İşletme Arama Sistemi

Türkiye'nin 81 ilinde hastane, restoran, otel, eczane gibi işletmeleri kolayca bulmanızı ve iletişim bilgilerine anında ulaşmanızı sağlayan modern masaüstü uygulaması.

![.NET Version](https://img.shields.io/badge/.NET-8.0-512BD4?logo=dotnet)
![Windows Forms](https://img.shields.io/badge/Windows%20Forms-blue?logo=windows)
![License](https://img.shields.io/badge/License-MIT-green.svg)
![Status](https://img.shields.io/badge/Status-Active-success)

## 📸 Ekran Görüntüleri

<img width="1235" height="750" alt="Ekran görüntüsü 2025-10-30 010626" src="https://github.com/user-attachments/assets/fa28b750-aa70-41eb-9c06-97b27167d639" />
*Modern ve kullanıcı dostu arayüz*

## ✨ Özellikler

- 🔍 **Kapsamlı Arama**: 81 il için detaylı işletme araması
- 📊 **Veri Dışa Aktarma**: Excel ve PDF formatında sonuçları kaydetme
- 💬 **WhatsApp Entegrasyonu**: Sonuçları doğrudan WhatsApp'ta paylaşma
- 🌐 **Google Places API**: Güncel ve güvenilir işletme bilgileri
- ⭐ **Puanlama Sistemi**: İşletme derecelendirmeleri ve kullanıcı yorumları
- 📍 **Konum Bilgileri**: Detaylı adres ve harita entegrasyonu
- 🎨 **Modern UI**: Şık ve responsive arayüz tasarımı
- 📞 **İletişim Filtreleri**: Telefon, e-posta veya her ikisini birden seçme

## 🚀 Başlangıç

### Gereksinimler

- Windows 10 veya üstü
- [.NET 8.0 Runtime](https://dotnet.microsoft.com/download/dotnet/8.0)
- Google Places API anahtarı ([Nasıl alınır?](#google-places-api-anahtarı-nasıl-alınır))

### Kurulum

1. **Projeyi klonlayın**
```bash
git clone https://github.com/serkangrcndev/kurum_arama.git
cd kurum_arama
```

2. **Yapılandırma dosyasını oluşturun**
```bash
# appsettings.example.json dosyasını kopyalayın
copy appsettings.example.json appsettings.json
```

3. **API anahtarınızı ekleyin**

`appsettings.json` dosyasını düzenleyin ve kendi Google Places API anahtarınızı ekleyin:

```json
{
  "GooglePlacesAPI": {
    "ApiKey": "YOUR_GOOGLE_PLACES_API_KEY_HERE"
  }
}
```

4. **Projeyi derleyin ve çalıştırın**
```bash
dotnet restore
dotnet build
dotnet run
```

**Hızlı Adımlar:**
1. [Google Cloud Console](https://console.cloud.google.com/) adresine gidin
2. Yeni bir proje oluşturun veya mevcut bir projeyi seçin
3. "APIs & Services" > "Library" bölümünden "Places API"yi etkinleştirin
4. "Credentials" bölümünden "Create Credentials" > "API Key" seçin
5. Oluşturulan API anahtarını kopyalayın ve `appsettings.json` dosyasına yapıştırın

## 📖 Kullanım

1. **İşletme Türü Girin**: Arama kutusuna aramak istediğiniz işletme türünü yazın (örn: "Hastane", "Restoran", "Otel")
2. **İl Seçin**: Açılır menüden arama yapmak istediğiniz ili seçin
3. **İletişim Bilgisi Seçin**: Telefon, e-posta veya her ikisini birden seçin
4. **Ara**: Arama butonuna tıklayın
5. **Sonuçları Dışa Aktarın**: Excel, PDF veya WhatsApp ile sonuçları paylaşın

## 🛠️ Kullanılan Teknolojiler

### Framework & Platform
- **.NET 8.0** - Modern ve performanslı framework
- **Windows Forms** - Masaüstü uygulama geliştirme

### Kütüphaneler & Paketler
- **Google Places API** - İşletme verilerini çekme
- **EPPlus 7.0.0** - Excel dosyası oluşturma
- **iTextSharp 5.5.13.3** - PDF oluşturma
- **Newtonsoft.Json 13.0.3** - JSON işleme
- **Microsoft.Extensions.Configuration** - Yapılandırma yönetimi

### API & Servisler
- **Google Places API** - Text Search & Place Details
- **HttpClient** - RESTful API iletişimi

## 📁 Proje Yapısı

```
kurum_arama/
├── Form1.cs                      # Ana form ve UI mantığı
├── Form1.Designer.cs             # Form tasarımı (auto-generated)
├── Form1.resx                    # Form kaynakları
├── GooglePlacesService.cs        # Google Places API servisi
├── OverpassService.cs            # Overpass API servisi (gelecek)
├── Program.cs                    # Uygulama başlangıç noktası
├── appsettings.json              # Yapılandırma dosyası (git ignore)
├── appsettings.example.json      # Yapılandırma şablonu
├── kurum_arama.csproj            # Proje dosyası
├── README.md                     # Bilgilendirme dosyası
└── LICENSE                       # Lisans dosyası
```

## 📝 Lisans

Bu proje MIT lisansı altında lisanslanmıştır. Detaylar için [LICENSE](LICENSE) dosyasına bakın.

## 👨‍💻 Geliştirici

**Serkan Gürcan & Emirhan Karakuş**

- GitHub: [@[Serkangrcndev]](https://github.com/Serkangrcndev)
- LinkedIn: [https://www.linkedin.com/in/emrhankarakus/] 
- LinkedIn: [Serkan Gürcan](https://linkedin.com/in/serkan-gürcan-0ab912332)

## 🙏 Teşekkürler

- [Google Places API](https://developers.google.com/maps/documentation/places/web-service) - İşletme verileri için
- [EPPlus](https://github.com/EPPlusSoftware/EPPlus) - Excel işlemleri için
- [iTextSharp](https://github.com/itext/itextsharp) - PDF oluşturma için

## 📞 İletişim

Sorularınız veya önerileriniz için:
- Email: [serkan.gurcan.kariyer@gmail.com]
- LinkedIn: [https://linkedin.com/in/serkan-gürcan-0ab912332]

---

⭐ Bu projeyi beğendiyseniz yıldız vermeyi unutmayın!

**Not**: Bu uygulama ticari olmayan kullanım için ücretsizdir.


