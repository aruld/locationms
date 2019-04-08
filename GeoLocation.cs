using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace locationms
{
    public class GeoLocation
    {
        [JsonProperty("query")]
        public string Query { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("countryCode")]
        public string CountryCode { get; set; }

        [JsonProperty("region")]
        public string Region { get; set; }

        [JsonProperty("regionName")]
        public string RegionName { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("zip")]
        public string Zip { get; set; }

        [JsonProperty("lat")]
        public float Lat{ get; set; }

        [JsonProperty("lon")]
        public float Lon { get; set; }

        [JsonProperty("timezone")]
        public string Timezone { get; set; }

        [JsonProperty("isp")]
        public string ISP { get;set;}

        [JsonProperty("org")]
        public string Org { get; set; }

        [JsonProperty("as")]
        public string As { get; set; }
        [JsonProperty("version")]
        public string Version {
            get {
                return "v2";
            }
        }
        private GeoLocation() { }

        public static async Task<GeoLocation> QueryGeographicalLocationAsync(string ipAddress)
        {
            HttpClient client = new HttpClient();
            string url = "http://ip-api.com/json/" + ipAddress;
            Console.WriteLine(url);
            string result = await client.GetStringAsync(url);

            return JsonConvert.DeserializeObject<GeoLocation>(result);
        }
    }
}
