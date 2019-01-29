using NetPointDNS.Resources;
using Newtonsoft.Json;

namespace NetPointDNS.Dtos.Response
{
    public class ZoneRedirectResponse
    {
        [JsonProperty("zone_redirect")]
        public ZoneRedirect ZoneRedirect { get; set; }
    }

    public static class ZoneRedirectResponseExtensions
    {
        public static ZoneRedirect Extract(this ZoneRedirectResponse zoneRedirectResponse)
        {
            return zoneRedirectResponse.ZoneRedirect;
        }
    }
}
