using Newtonsoft.Json;

namespace NetPointDNS.Resources
{
    /// <summary>
    /// Represents a zone redirect.
    /// </summary>
    public class ZoneRedirect
    {
        /// <summary>
        /// The id of the record.
        /// </summary>
        [JsonProperty("id")]
        public int Id { get; set; }

        /// <summary>
        /// The FQDN of the record.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Redirect destination.
        /// </summary>
        [JsonProperty("redirect_to")]
        public string RedirectTo { get; set; }

        /// <summary>
        /// The type of redirect. Permanent (301), temporary (302), or Iframe (0).
        /// </summary>
        [JsonProperty("redirect_type")]
        public RedirectType RedirectType { get; set; }

        /// <summary>
        /// The title of the iframe (optional).
        /// </summary>
        [JsonProperty("iframe_title")]
        public string IframeTitle { get; set; }

        /// <summary>
        /// Include or exclude the query string.
        /// </summary>
        [JsonProperty("redirect_query_string")]
        public bool RedirectQueryString { get; set; }

        /// <summary>
        /// The zone this record belongs to.
        /// </summary>
        [JsonProperty("zone_id")]
        public int ZoneId { get; set; }
    }
}
