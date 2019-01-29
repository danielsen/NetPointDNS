using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using NUnit.Framework;
using NSubstitute;
using Should;
using NetPointDNS.Http;
using NetPointDNS.Resources;

namespace NetPointDNS.Tests.Unit
{
    [TestFixture]
    public class ApiZoneTests
    {
        private const int _id = 1;
        private const string _name = "example.com";
        private const string _group = "Default Group";
        private const int _userId = 3;
        private const int _ttl = 3600;

        private static readonly string BaseResource =
            $"{{\"zone\":{{\"id\": {_id},\"name\": \"{_name}\",\"group\": \"{_group}\",\"user-id\": {_userId},\"ttl\": {_ttl}}}}}";

        private readonly string BaseResourceList = $"[{BaseResource}]";

        private readonly IClient _client = Substitute.For<IClient>();

        [Test]
        public async Task should_get_collection_of_zones()
        {
            var responseMessage = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(BaseResourceList)
            };

            _client.Get(Arg.Any<string>()).Returns(responseMessage);

            var api = new Api(_client, "user", "token");

            var zones = await api.GetZonesAsync();
            zones.Count().ShouldEqual(1);
            should_match_zone(zones.First(), _group, _id, _name, _ttl, _userId);
        }

        [Test]
        public async Task should_get_single_zone()
        {
            var responseMessage = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(BaseResource)
            };

            _client.Get(Arg.Any<string>()).Returns(responseMessage);

            var api = new Api(_client, "user", "token");

            var zone = await api.GetZoneAsync(_id);
            should_match_zone(zone, _group, _id, _name, _ttl, _userId);
        }

        [Test]
        public async Task should_create_zone()
        {
            var responseMessage = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(BaseResource)
            };

            _client.Post(Arg.Any<string>(), Arg.Any<string>()).Returns(responseMessage);

            var api = new Api(_client, "user", "token");

            var zone = await api.CreateZoneAsync(_name, _group);
            should_match_zone(zone, _group, _id, _name, _ttl, _userId);
        }

        [Test]
        public async Task should_update_zone()
        {
            var responseMessage = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(BaseResource)
            };

            _client.Put(Arg.Any<string>(), Arg.Any<string>()).Returns(responseMessage);

            var api = new Api(_client, "user", "token");

            var zone = await api.CreateZoneAsync(_name, _group);
            should_match_zone(zone, _group, _id, _name, _ttl, _userId);

        }

        [Test]
        public async Task should_delete_zone()
        {
            var responseMessage = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK
            };

            _client.Delete(Arg.Any<string>()).Returns(responseMessage);

            var api = new Api(_client, "user", "token");
            var result = await api.DeleteZoneAsync(_id);

            result.ShouldBeTrue();
        }

        public void should_match_zone(Zone target, string group, int id, 
            string name, int ttl, int userId)
        {
            target.Group.ShouldEqual(group);
            target.Id.ShouldEqual(id);
            target.Name.ShouldEqual(name);
            target.Ttl.ShouldEqual(ttl);
            target.UserId.ShouldEqual(userId);
        }
    }
}
