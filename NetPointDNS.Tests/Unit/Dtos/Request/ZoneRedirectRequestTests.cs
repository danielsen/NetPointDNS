using NetPointDNS.Dtos.Request;
using NetPointDNS.Resources;
using NUnit.Framework;
using Should;

namespace NetPointDNS.Tests.Unit.Dtos.Request
{
    [TestFixture]
    public class ZoneRedirectRequestTests
    {
        private const string Name = "site.example.com";
        private const string RedirectTo = "http:/example.com";
        private const string IframeTitle = "IF Title";
        private bool RedirectQueryString = true;

        [Test]
        public void should_correctly_create_dto()
        {
            var dto = new ZoneRedirectRequest
            {
                Name = Name,
                RedirectType = RedirectType.Permanently,
                IframeTitle = IframeTitle,
                RedirectTo = RedirectTo,
                RedirectQueryString = RedirectQueryString
            };

            var json = dto.Prepare();

            json.ShouldContain(Name);
            json.ShouldContain(RedirectTo);
            json.ShouldContain(IframeTitle);
            json.ShouldContain("301");
        }

        [Test]
        public void should_correctly_create_dto_without_ifram_title()
        {
            var dto = new ZoneRedirectRequest
            {
                Name = Name,
                RedirectType = RedirectType.Permanently,
                RedirectTo = RedirectTo,
                RedirectQueryString = RedirectQueryString
            };

            var json = dto.Prepare();

            json.ShouldContain(Name);
            json.ShouldContain(RedirectTo);
            json.ShouldNotContain(IframeTitle);
            json.ShouldContain("301");
        }

        [TestCase(RedirectType.Iframe, "0")]
        [TestCase(RedirectType.Permanently, "301")]
        [TestCase(RedirectType.Temporary, "302")]
        public void should_correctly_prepare_redirect_type(RedirectType type, string expected)
        {
            var dto = new ZoneRedirectRequest
            {
                Name = Name,
                RedirectType = type,
                RedirectTo = RedirectTo,
                RedirectQueryString = RedirectQueryString
            };

            var json = dto.Prepare();

            json.ShouldContain(expected);
        }
    }
}
