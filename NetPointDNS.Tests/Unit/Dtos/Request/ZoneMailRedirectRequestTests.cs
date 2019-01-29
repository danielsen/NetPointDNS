using NUnit.Framework;
using Should;
using NetPointDNS.Dtos.Request;

namespace NetPointDNS.Tests.Unit.Dtos.Request
{
    [TestFixture]
    public class ZoneMailRedirectRequestTests
    {
        private const string SourceAddress = "admin";
        private const string DestinationAddress = "user@example.com";

        [Test]
        public void should_corectly_prepare_dto()
        {
            var dto = new ZoneMailRedirectRequest
            {
                DestinationAddress = DestinationAddress,
                SourceAddress = SourceAddress
            };

            var json = dto.Prepare();

            json.ShouldContain(SourceAddress);
            json.ShouldContain(DestinationAddress);
        }
    }
}
