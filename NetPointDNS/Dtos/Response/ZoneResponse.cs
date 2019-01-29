using NetPointDNS.Resources;
using Newtonsoft.Json;

namespace NetPointDNS.Dtos.Response
{
    public class ZoneResponse
    {
        [JsonProperty("zone")]
        public Zone Zone { get; set; }
    }

    public static class ZoneResponseExtensions
    {
        public static Zone Extract(this ZoneResponse zoneResponse)
        {
            return zoneResponse.Zone;
        }
    }
}
