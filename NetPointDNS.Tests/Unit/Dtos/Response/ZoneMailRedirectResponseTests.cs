using Newtonsoft.Json;
using NUnit.Framework;
using Should;
using NetPointDNS.Dtos.Response;
using NetPointDNS.Resources;

namespace NetPointDNS.Tests.Unit.Dtos.Response
{
    [TestFixture]
    public class ZoneMailRedirectResponseTests
    {
        private static string _sourceAddress = "admin";
        private static string _destinationAddress = "user@example.com";
        private static int _id = 5;
        private static int _zoneId = 1;

        private static string BaseResource =
            $@"{{
                ""zone_mail_redirect"": {{
                    ""source_address"": ""{_sourceAddress}"",
                    ""destination_address"": ""{_destinationAddress}"",
                    ""id"": {_id},
                    ""zone_id"": {_zoneId}
                    }}
                }}";

        [Test]
        public void should_correctly_get_mail_redirect_from_dto()
        {
            var dto = JsonConvert.DeserializeObject<ZoneMailRedirectResponse>(BaseResource);
            var redirect = dto.Extract();

            should_match_mail_redirect(redirect, _sourceAddress, _destinationAddress, _id, _zoneId);
        }

        public void should_match_mail_redirect(ZoneMailRedirect mailRedirect, string sourceAddress,
            string destinationAddress, int id, int zoneid)
        {
            mailRedirect.Id.ShouldEqual(id);
            mailRedirect.DestinationAddress.ShouldEqual(destinationAddress);
            mailRedirect.SourceAddress.ShouldEqual(sourceAddress);
            mailRedirect.ZoneId.ShouldEqual(zoneid);
        }
    }
}
