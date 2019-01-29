using System.Collections.Generic;
using Newtonsoft.Json;

namespace NetPointDNS.Dtos.Request
{
    public class ZoneMailRedirectRequest
    {
        [JsonProperty("source_address", NullValueHandling = NullValueHandling.Ignore)]
        public string SourceAddress { get; set; }

        [JsonProperty("destination_address", NullValueHandling = NullValueHandling.Ignore)]
        public string DestinationAddress { get; set; }
    }

    public static class ZoneMailRedirectRequestExtensions
    {
        public static string Prepare(this ZoneMailRedirectRequest zoneMailRedirectRequest)
        {
            var dto = new Dictionary<string, ZoneMailRedirectRequest>()
            {
                {"zone_mail_redirect", zoneMailRedirectRequest}
            };

            return JsonConvert.SerializeObject(dto);
        }
    }
}
