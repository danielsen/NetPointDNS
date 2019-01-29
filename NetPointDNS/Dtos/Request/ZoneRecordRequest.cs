using System.Collections.Generic;
using NetPointDNS.Resources;
using Newtonsoft.Json;

namespace NetPointDNS.Dtos.Request
{
    public class ZoneRecordRequest
    {
        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        [JsonProperty("data", NullValueHandling = NullValueHandling.Ignore)]
        public string Data { get; set; }

        [JsonProperty("aux", NullValueHandling = NullValueHandling.Ignore)]
        public string Aux { get; set; }

        [JsonProperty("record_type", NullValueHandling = NullValueHandling.Ignore)]
        public RecordType? RecordType { get; set; }

        [JsonProperty("ttl", NullValueHandling = NullValueHandling.Ignore)]
        public int? Ttl { get; set; } = 3600;
    }

    public static class ZoneRecordRequestExtensions
    {
        public static string Prepare(this ZoneRecordRequest zoneRecordRequest)
        {
            var dto = new Dictionary<string, ZoneRecordRequest>(){{"zone_record", zoneRecordRequest}};

            return JsonConvert.SerializeObject(dto);
        }
    }
}
