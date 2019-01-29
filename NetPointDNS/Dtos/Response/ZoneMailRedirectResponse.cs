using Newtonsoft.Json;
using NetPointDNS.Resources;

namespace NetPointDNS.Dtos.Response
{
    public class ZoneMailRedirectResponse
    {
        [JsonProperty("zone_mail_redirect")]
        public ZoneMailRedirect ZoneMailRedirect { get; set; }
    }

    public static class ZoneMailRedirectResponseExtensions
    {
        public static ZoneMailRedirect Extract(this ZoneMailRedirectResponse zoneMailRedirectResponse)
        {
            return zoneMailRedirectResponse.ZoneMailRedirect;
        }
    }
}
