using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Should;
using NetPointDNS.Resources;
using Newtonsoft.Json;
using NetPointDNS.Http;
using NSubstitute;

namespace NetPointDNS.Tests.Unit
{
    [TestFixture]
    public class ApiZoneRecordTests
    {
        private static readonly int _id = 141;
        private static readonly string _name = "site.example.com";
        private static readonly string _data = "1.2.3.4";
        private static string _aux = null;
        private static readonly RecordType _recordType = RecordType.A;
        private static readonly int _ttl = 3600;
        private static readonly int _zoneId = 1;

        private static string BaseResource =
            $"{{\"zone_record\":{{\"name\":\"{_name}\",\"data\":\"{_data}\",\"id\":{_id},\"aux\":null,\"record_type\":\"{_recordType}\",\"ttl\":{_ttl},\"zone_id\":{_zoneId}}}}}";

        private string BaseResourceList = $"[{BaseResource}]";

        private readonly IClient _client = Substitute.For<IClient>();

        [Test]
        public async Task should_get_collection_of_records()
        {
            var responseMssage = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(BaseResourceList)
            };

            _client.Get(Arg.Any<string>()).Returns(responseMssage);

            var api = new Api(_client, "user", "token");

            var records = await api.GetRecordsForZoneAsync(_zoneId);
            records.Count().ShouldEqual(1);
            should_match_record(records.First(), _id, _name, _data, _aux, _recordType,
                _ttl, _zoneId);
        }

        [Test]
        public async Task should_get_collection_of_filtered_records()
        {
            var responseMessage = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(BaseResourceList)
            };

            _client.Get(Arg.Any<string>()).Returns(responseMessage);

            var api = new Api(_client, "user", "token");

            var records = await api.GetFilteredRecordsForZoneAsync(_zoneId, RecordType.A, _name);
            records.Count().ShouldEqual(1);
            should_match_record(records.First(), _id, _name, _data, _aux, _recordType,
                _ttl, _zoneId);
        }

        [Test]
        public async Task should_get_single_record()
        {
            var responseMessage = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(BaseResource)
            };

            _client.Get(Arg.Any<string>()).Returns(responseMessage);

            var api = new Api(_client, "user", "token");

            var record = await api.GetRecordForZoneAsync(_zoneId, _id);
            should_match_record(record, _id, _name, _data, _aux, _recordType,
                _ttl, _zoneId);
        }

        [Test]
        public async Task should_create_record()
        {
            var responseMessage = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(BaseResource)
            };

            _client.Post(Arg.Any<string>(), Arg.Any<string>()).Returns(responseMessage);

            var api = new Api(_client, "user", "token");

            var record = await api.CreateRecordAsync(_zoneId, _name, _recordType, _data, _ttl, _aux);
            should_match_record(record, _id, _name, _data, _aux, _recordType,
                _ttl, _zoneId);
        }

        [Test]
        public async Task should_update_record()
        {
            var responseMessage = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(BaseResource)
            };

            _client.Put(Arg.Any<string>(), Arg.Any<string>()).Returns(responseMessage);

            var api = new Api(_client, "user", "token");

            var record = await api.UpdateRecordAsync(_zoneId, _id, _name, _recordType, _data, _ttl, _aux);
            should_match_record(record, _id, _name, _data, _aux, _recordType,
                _ttl, _zoneId);
        }

        [Test]
        public async Task should_delete_record()
        {
            var responseMessage = new HttpResponseMessage(HttpStatusCode.OK)
            {
            };

            _client.Delete(Arg.Any<string>()).Returns(responseMessage);

            var api = new Api(_client, "user", "token");

            var result = await api.DeleteRecordAsync(_zoneId, _id);
            result.ShouldBeTrue();
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
