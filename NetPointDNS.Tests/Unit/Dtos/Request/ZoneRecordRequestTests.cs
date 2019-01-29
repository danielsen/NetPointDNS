using NUnit.Framework;
using Should;
using NetPointDNS.Dtos.Request;
using NetPointDNS.Resources;

namespace NetPointDNS.Tests.Unit.Dtos.Request
{
    [TestFixture]
    public class ZoneRecordRequestTests
    {
        private const string Name = "site.example.com";
        private const string Data = "1.2.3.4";
        private const string Aux = "10";

        [Test]
        public void should_correctly_prepare_full_dto()
        {
            var dto = new ZoneRecordRequest
            {
                Data = Data,
                Name = Name,
                RecordType = RecordType.A,
                Ttl = 7200
            };

            var json = dto.Prepare();

            json.ShouldContain(Data);
            json.ShouldContain(Name);
            json.ShouldContain("A");
            json.ShouldContain("7200");
            json.ShouldNotContain("aux");
            json.ShouldNotContain(Aux);
        }

        [Test]
        public void should_correctly_prepare_dto_with_aux()
        {
            var dto = new ZoneRecordRequest
            {
                Data = Data,
                Name = Name,
                RecordType = RecordType.Mx,
                Ttl = 7200,
                Aux = Aux
            };

            var json = dto.Prepare();

            json.ShouldContain(Data);
            json.ShouldContain(Name);
            json.ShouldContain("MX");
            json.ShouldContain("7200");
            json.ShouldContain(Aux);
        }

        [TestCase(RecordType.A, "A")]
        [TestCase(RecordType.Aaaa, "AAAA")]
        [TestCase(RecordType.Alias, "ALIAS")]
        [TestCase(RecordType.Cname, "CNAME")]
        [TestCase(RecordType.Mx, "MX")]
        [TestCase(RecordType.Ptr, "PTR")]
        [TestCase(RecordType.Srv, "SRV")]
        [TestCase(RecordType.Sshfp, "SSHFP")]
        [TestCase(RecordType.Txt, "TXT")]
        public void should_correctly_prepare_record_type(RecordType type, string expected)
        {
            var dto = new ZoneRecordRequest
            {
                Data = Data,
                Name = Name,
                RecordType = type,
                Ttl = 7200,
            };

            var json = dto.Prepare();

            json.ShouldContain(expected);
        }
    }
}
