using System.Collections.Generic;
using Newtonsoft.Json;

namespace NetPointDNS.Resources
{
    /// <summary>
    /// Represents a DNS zone.
    /// </summary>
    public class Zone
    {
        /// <summary>
        /// The zone id.
        /// </summary>
        [JsonProperty("id")]
        public int Id { get; set; }

        /// <summary>
        /// The domain name.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// The zone TTL (defaults to 3600 seconds).
        /// </summary>
        [JsonProperty("ttl")]
        public int Ttl { get; set; }

        /// <summary>
        /// The group the zone belongs to.
        /// </summary>
        [JsonProperty("group")]
        public string Group { get; set; }

        /// <summary>
        /// The ID of the user who created the zone.
        /// </summary>
        [JsonProperty("user-id")]
        public int UserId { get; set; }

        [JsonIgnore]
        public IEnumerable<ZoneRecord> Records { get; set; }

        [JsonIgnore]
        public IEnumerable<ZoneRedirect> Redirects { get; set; }

        [JsonIgnore]
        public IEnumerable<ZoneMailRedirect> MailRedirects { get; set; }
    }
}
