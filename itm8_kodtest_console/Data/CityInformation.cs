using Newtonsoft.Json;

namespace itm8_kodtest_console.Data
{
    [JsonObject]
    public class CityInformation
    {
        [JsonProperty("city")]
        public string City { get; set; }
        [JsonProperty("longitude")]
        public string Longitude { get; set; }
        [JsonProperty("latitude")]
        public string Latitude { get; set; }
        [JsonProperty("country")]
        public string Country { get; set; }
        [JsonProperty("temperature")]
        public double Temperature { get; set; }
    }
}