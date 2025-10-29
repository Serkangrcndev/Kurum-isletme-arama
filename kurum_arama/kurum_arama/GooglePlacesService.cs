using Newtonsoft.Json;
using System.Net.Http;

namespace kurum_arama
{
    public class GooglePlacesService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;
        
        // Türkiye şehirlerinin koordinatları
        private readonly Dictionary<string, (double lat, double lng)> _sehirKoordinatlari = new()
        {
            {"İstanbul", (41.0082, 28.9784)},
            {"Ankara", (39.9334, 32.8597)},
            {"İzmir", (38.4192, 27.1287)},
            {"Bursa", (40.1826, 29.0665)},
            {"Antalya", (36.8969, 30.7133)},
            {"Adana", (37.0000, 35.3213)},
            {"Konya", (37.8667, 32.4833)},
            {"Gaziantep", (37.0662, 37.3833)},
            {"Mersin", (36.8000, 34.6333)},
            {"Diyarbakır", (37.9144, 40.2306)},
            {"Kayseri", (38.7312, 35.4787)},
            {"Eskişehir", (39.7767, 30.5206)},
            {"Urfa", (37.1591, 38.7969)},
            {"Malatya", (38.3552, 38.3095)},
            {"Erzurum", (39.9334, 41.2767)},
            {"Van", (38.4891, 43.4089)},
            {"Batman", (37.8812, 41.1351)},
            {"Elazığ", (38.6810, 39.2264)},
            {"Isparta", (37.7648, 30.5566)},
            {"Denizli", (37.7765, 29.0864)},
            {"Kahramanmaraş", (37.5858, 36.9371)},
            {"Samsun", (41.2928, 36.3313)},
            {"Trabzon", (41.0015, 39.7178)},
            {"Ordu", (40.9839, 37.8764)},
            {"Giresun", (40.9128, 38.3895)},
            {"Rize", (41.0201, 40.5234)},
            {"Artvin", (41.1828, 41.8183)},
            {"Erzincan", (39.7500, 39.5000)},
            {"Bingöl", (38.8847, 40.4986)},
            {"Tunceli", (39.1079, 39.5401)},
            {"Muş", (38.9462, 41.7539)},
            {"Bitlis", (38.3938, 42.1232)},
            {"Hakkâri", (37.5833, 43.7333)},
            {"Siirt", (37.9274, 41.9403)},
            {"Mardin", (37.3212, 40.7245)},
            {"Şırnak", (37.4187, 42.4918)},
            {"Ağrı", (39.7191, 43.0503)},
            {"Kars", (40.6013, 43.0975)},
            {"Ardahan", (41.1105, 42.7022)},
            {"Iğdır", (39.9230, 44.0048)},
            {"Yozgat", (39.8181, 34.8146)},
            {"Çorum", (40.5506, 34.9556)},
            {"Amasya", (40.6499, 35.8353)},
            {"Tokat", (40.3167, 36.5500)},
            {"Sivas", (39.7477, 37.0179)},
            {"Çankırı", (40.6013, 33.6134)},
            {"Kastamonu", (41.3887, 33.7827)},
            {"Sinop", (42.0231, 35.1531)},
            {"Zonguldak", (41.4564, 31.7987)},
            {"Bartın", (41.6344, 32.3375)},
            {"Karabük", (41.2061, 32.6204)},
            {"Düzce", (40.8438, 31.1565)},
            {"Bolu", (40.7395, 31.6060)},
            {"Sakarya", (40.7889, 30.4053)},
            {"Kocaeli", (40.8533, 29.8815)},
            {"Tekirdağ", (40.9833, 27.5167)},
            {"Edirne", (41.6771, 26.5557)},
            {"Kırklareli", (41.7350, 27.2256)},
            {"Balıkesir", (39.6484, 27.8826)},
            {"Çanakkale", (40.1553, 26.4142)},
            {"Manisa", (38.6191, 27.4289)},
            {"Aydın", (37.8560, 27.8416)},
            {"Muğla", (37.2153, 28.3636)},
            {"Burdur", (37.7206, 30.2906)},
            {"Afyonkarahisar", (38.7507, 30.5567)},
            {"Kütahya", (39.4200, 29.9833)},
            {"Uşak", (38.6823, 29.4082)},
            {"Bilecik", (40.1501, 29.9833)},
            {"Kırıkkale", (39.8468, 33.4987)},
            {"Aksaray", (38.3687, 34.0370)},
            {"Nevşehir", (38.6939, 34.6857)},
            {"Niğde", (37.9667, 34.6833)},
            {"Karaman", (37.1759, 33.2287)},
            {"Kırşehir", (39.1425, 34.1709)},
            {"Yalova", (40.6550, 29.2769)},
            {"Kilis", (36.7184, 37.1212)},
            {"Osmaniye", (37.0742, 36.2478)},
            {"Hatay", (36.4018, 36.3498)}
        };

        public GooglePlacesService(string apiKey)
        {
            _apiKey = apiKey;
            _httpClient = new HttpClient();
        }

        public async Task<List<GooglePlace>> AramaYap(string aramaTerimi, string sehir)
        {
            try
            {
                if (!_sehirKoordinatlari.ContainsKey(sehir))
                {
                    throw new ArgumentException($"Şehir bulunamadı: {sehir}");
                }

                var (lat, lng) = _sehirKoordinatlari[sehir];
                var radius = 50000; // 50km yarıçap
                
                var url = $"https://maps.googleapis.com/maps/api/place/textsearch/json?" +
                         $"query={aramaTerimi}+{sehir}+Türkiye&" +
                         $"location={lat},{lng}&" +
                         $"radius={radius}&" +
                         $"key={_apiKey}";

                var response = await _httpClient.GetStringAsync(url);
                var result = JsonConvert.DeserializeObject<GooglePlacesResponse>(response);

                if (result?.Status != "OK")
                {
                    throw new Exception($"Google Places API Hatası: {result?.Status}");
                }

                var places = new List<GooglePlace>();
                
                foreach (var place in result.Results ?? new List<GooglePlaceResult>())
                {
                 
                    var details = await PlaceDetaylariniGetir(place.PlaceId);
                    if (details != null)
                    {
                        places.Add(details);
                    }
                }

                return places;
            }
            catch (Exception ex)
            {
                throw new Exception($"Google Places API hatası: {ex.Message}");
            }
        }

        private async Task<GooglePlace?> PlaceDetaylariniGetir(string placeId)
        {
            try
            {
                var url = $"https://maps.googleapis.com/maps/api/place/details/json?" +
                         $"place_id={placeId}&" +
                         $"fields=name,formatted_address,formatted_phone_number,website,rating,user_ratings_total&" +
                         $"key={_apiKey}";

                var response = await _httpClient.GetStringAsync(url);
                var result = JsonConvert.DeserializeObject<GooglePlaceDetailsResponse>(response);

                if (result?.Status == "OK" && result.Result != null)
                {
                    return new GooglePlace
                    {
                        Ad = result.Result.Name ?? "",
                        Adres = result.Result.FormattedAddress ?? "",
                        Telefon = result.Result.FormattedPhoneNumber ?? "",
                        Website = result.Result.Website ?? "",
                        Rating = result.Result.Rating ?? 0,
                        UserRatingsTotal = result.Result.UserRatingsTotal ?? 0
                    };
                }
            }
            catch (Exception ex)
            {
              
                Console.WriteLine($"Place details hatası: {ex.Message}");
            }

            return null;
        }

        public void Dispose()
        {
            _httpClient?.Dispose();
        }
    }

  
    public class GooglePlacesResponse
    {
        [JsonProperty("status")]
        public string Status { get; set; } = "";

        [JsonProperty("results")]
        public List<GooglePlaceResult>? Results { get; set; }
    }

    public class GooglePlaceResult
    {
        [JsonProperty("place_id")]
        public string PlaceId { get; set; } = "";

        [JsonProperty("name")]
        public string Name { get; set; } = "";

        [JsonProperty("formatted_address")]
        public string FormattedAddress { get; set; } = "";
    }

    public class GooglePlaceDetailsResponse
    {
        [JsonProperty("status")]
        public string Status { get; set; } = "";

        [JsonProperty("result")]
        public GooglePlaceDetailsResult? Result { get; set; }
    }

    public class GooglePlaceDetailsResult
    {
        [JsonProperty("name")]
        public string? Name { get; set; }

        [JsonProperty("formatted_address")]
        public string? FormattedAddress { get; set; }

        [JsonProperty("formatted_phone_number")]
        public string? FormattedPhoneNumber { get; set; }

        [JsonProperty("website")]
        public string? Website { get; set; }

        [JsonProperty("rating")]
        public double? Rating { get; set; }

        [JsonProperty("user_ratings_total")]
        public int? UserRatingsTotal { get; set; }
    }

    public class GooglePlace
    {
        public string Ad { get; set; } = "";
        public string Adres { get; set; } = "";
        public string Telefon { get; set; } = "";
        public string Website { get; set; } = "";
        public double Rating { get; set; }
        public int UserRatingsTotal { get; set; }
    }
}
