using System;
using NUnit.Framework;
using Should;
using NetPointDNS.Dtos.Response;
using NetPointDNS.Resources;
using Newtonsoft.Json;

namespace NetPointDNS.Tests.Unit.Dtos.Response
{
    [TestFixture]
    public class ZoneRedirectResponseTests
    {
        private static string _name = "site.example.com";
        private static string _redirectTo = "http://example.com";
        private static int _redirectType = 302;
        private static string _iframeTitle = null;
        private static bool _redirectQueryString = false;
        private static int _id = 3;
        private static int _zoneId = 1;

        private static string BaseResource =
            $@"{{
                ""zone_redirect"": {{
                    ""name"": ""{_name}"",
                    ""redirect_to"": ""{_redirectTo}"",
                    ""id"": {_id},
                    ""redirect_type"": {_redirectType},
                    ""iframe_title"": null,
                    ""redirect_query_string"": false,
                    ""zone_id"": {_zoneId}
            }}}}";

        [Test]
        public void should_get_redirect_from_dto()
        {
            var dto = JsonConvert.DeserializeObject<ZoneRedirectResponse>(BaseResource);
            var redirect = dto.Extract();

            should_match_redirect(redirect, _id,  _iframeTitle, _name, _redirectTo,
                _redirectType, _redirectQueryString, _zoneId);
        }

        public void should_match_redirect(ZoneRedirect redirect, int id, string iframeTitle,
            string name, string redirectTo, int redirectType, bool redirectqueryString,
            int zoneId)
        {
            redirect.Id.ShouldEqual(id);
            redirect.Name.ShouldEqual(name);
            redirect.RedirectTo.ShouldEqual(redirectTo);
            redirect.RedirectType.ShouldEqual((RedirectType)redirectType);
            redirect.RedirectQueryString.ShouldEqual(redirectqueryString);
            redirect.ZoneId.ShouldEqual(zoneId);
        }
    }
}
