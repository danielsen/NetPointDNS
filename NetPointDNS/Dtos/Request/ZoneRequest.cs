using System.Collections.Generic;
using Newtonsoft.Json;

namespace NetPointDNS.Dtos.Request
{
    public class ZoneRequest
    {
        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        [JsonProperty("group", NullValueHandling = NullValueHandling.Ignore)]
        public string Group { get; set; }

        [JsonProperty("template", NullValueHandling = NullValueHandling.Ignore)]
        public string Template { get; set; }

        [JsonProperty("ttl")]
        public int Ttl { get; set; } = 3600;
    }

    /*
     * The PointDNS API places the represented entity under and JSON key
     * that states the entity type rather than accepting anonymous objects.
     * E.g. {"zone": { ... }}.
     */
    public static class ZoneRequestExtensions
    {
        public static string Prepare(this ZoneRequest zoneRequest)
        {
            var dto = new Dictionary<string, ZoneRequest>(){{"zone", zoneRequest}};

            return JsonConvert.SerializeObject(dto);
        }
    }
}
