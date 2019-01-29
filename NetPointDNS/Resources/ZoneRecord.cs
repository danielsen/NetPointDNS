using Newtonsoft.Json;

namespace NetPointDNS.Resources
{
    /// <summary>
    /// Represents a single record in a zone.
    /// </summary>
    public class ZoneRecord
    {
        /// <summary>
        /// The record id.
        /// </summary>
        [JsonProperty("id")]
        public int Id { get; set; }

        /// <summary>
        /// The record's FQDN.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// The record data field.
        /// </summary>
        [JsonProperty("data")]
        public string Data { get; set; }

        /// <summary>
        /// The record TTL.
        /// </summary>
        [JsonProperty("ttl")]
        public int Ttl { get; set; }

        /// <summary>
        /// The record type.
        /// </summary>
        [JsonProperty("record_type")]
        public RecordType RecordType { get; set; }
        
        /// <summary>
        /// The record AUX data (used in MX records).
        /// </summary>
        [JsonProperty("aux")]
        public string Aux { get; set; }

        /// <summary>
        /// The id of the zone the record belongs to.
        /// </summary>
        [JsonProperty("zone_id")]
        public int ZoneId { get; set; }
    }
}
