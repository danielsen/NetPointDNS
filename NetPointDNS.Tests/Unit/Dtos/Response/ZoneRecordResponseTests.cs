using NUnit.Framework;
using Should;
using NetPointDNS.Dtos.Response;
using NetPointDNS.Resources;
using Newtonsoft.Json;

namespace NetPointDNS.Tests.Unit.Dtos.Response
{
    [TestFixture]
    public class ZoneRecordResponseTests
    {
        private static int _id = 141;
        private static string _name = "site.example.com";
        private static string _data = "1.2.3.4";
        private static string _aux = null;
        private static RecordType _recordType = RecordType.A;
        private static int _ttl = 3600;
        private static int _zoneId = 1;

        private string BaseResource =
            $"{{\"zone_record\":{{\"name\":\"{_name}\",\"data\":\"{_data}\",\"id\":{_id},\"aux\":null,\"record_type\":\"{_recordType}\",\"ttl\":{_ttl},\"zone_id\":{_zoneId}}}}}";

        [Test]
        public void should_get_record_from_dto()
        {
            var dto = JsonConvert.DeserializeObject<ZoneRecordResponse>(BaseResource);
            var record = dto.Extract();

            should_match_record(record, _id, _name, _data, _aux, _recordType, _ttl, _zoneId);
        }

        public void should_match_record(ZoneRecord record, int id, string name, string data,
            string aux, RecordType type, int ttl, int zoneId)
        {
            record.RecordType.ShouldEqual(type);
            record.Aux.ShouldEqual(aux);
            record.Data.ShouldEqual(data);
            record.Name.ShouldEqual(name);
            record.Id.ShouldEqual(id);
            record.Ttl.ShouldEqual(ttl);
            record.ZoneId.ShouldEqual(zoneId);
        }
    }
}
