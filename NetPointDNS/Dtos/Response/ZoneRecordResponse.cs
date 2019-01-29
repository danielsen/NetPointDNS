using NetPointDNS.Resources;
using Newtonsoft.Json;

namespace NetPointDNS.Dtos.Response
{
    public class ZoneRecordResponse
    {
        [JsonProperty("zone_record")]
        public ZoneRecord ZoneRecord { get; set; }
    }

    public static class ZoneRecordResponseExtensions
    {
        public static ZoneRecord Extract(this ZoneRecordResponse zoneRecordResponse)
        {
            return zoneRecordResponse.ZoneRecord;
        }
    }
}
