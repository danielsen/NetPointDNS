using System.Collections.Generic;
using NetPointDNS.Resources;
using Newtonsoft.Json;

namespace NetPointDNS.Dtos.Request
{
    public class ZoneRedirectRequest
    {
        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        [JsonProperty("redirect_to", NullValueHandling = NullValueHandling.Ignore)]
        public string RedirectTo { get; set; }

        [JsonProperty("redirect_type", NullValueHandling = NullValueHandling.Ignore)]
        public RedirectType RedirectType { get; set; }

        [JsonProperty("iframe_title", NullValueHandling = NullValueHandling.Ignore)]
        public string IframeTitle { get; set; }

        [JsonProperty("redirect_query_string", NullValueHandling = NullValueHandling.Ignore)]
        public bool RedirectQueryString { get; set; }
    }

    public static class ZoneRedirectRequestExtensions
    {
        public static string Prepare(this ZoneRedirectRequest zoneRedirectRequest)
        {
            var dto = new Dictionary<string, ZoneRedirectRequest>(){{"zone_redirect", zoneRedirectRequest}};

            return JsonConvert.SerializeObject(dto);
        }
    }
}
