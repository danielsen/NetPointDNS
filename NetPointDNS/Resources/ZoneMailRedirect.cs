using Newtonsoft.Json;

namespace NetPointDNS.Resources
{
    /// <summary>
    /// Mail redirects for a zone.
    /// </summary>
    public class ZoneMailRedirect
    {
        /// <summary>
        /// The record id.
        /// </summary>
        [JsonProperty("id")]
        public int Id { get; set; }

        /// <summary>
        /// The mail redirect source address.
        /// </summary>
        [JsonProperty("source_address")]
        public string SourceAddress { get; set; }

        /// <summary>
        /// The mail redirect destination address.
        /// </summary>
        [JsonProperty("destination_address")]
        public string DestinationAddress { get; set; }

        /// <summary>
        /// The zone this record belongs to.
        /// </summary>
        [JsonProperty("zone_id")]
        public int ZoneId { get; set; }
    }
}
