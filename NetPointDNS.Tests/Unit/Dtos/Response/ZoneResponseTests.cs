using NUnit.Framework;
using Should;
using NetPointDNS.Dtos.Response;
using NetPointDNS.Resources;
using Newtonsoft.Json;

namespace NetPointDNS.Tests.Unit.Dtos.Response
{
    [TestFixture]
    public class ZoneResponseTests
    {
        private static int _id = 1;
        private static string _name = "example.com";
        private static string _group = "Default Group";
        private static int _userId = 3;
        private static int _ttl = 3600;

        private static string BaseResource =
            $"{{\"zone\":{{\"id\": {_id},\"name\": \"{_name}\",\"group\": \"{_group}\",\"user-id\": {_userId},\"ttl\": {_ttl}}}}}";

        public string BaseResourceList = $"[{BaseResource}]";

        [Test]
        public void should_get_zone_from_dto()
        {
            var dto = JsonConvert.DeserializeObject<ZoneResponse>(BaseResource);
            var zone = dto.Extract();

            should_match_zone(zone, _group, _id, _name, _ttl, _userId);
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
