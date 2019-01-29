using System;
using System.Collections.Generic;
using System.Text;
using NetPointDNS.Dtos.Request;
using Newtonsoft.Json;
using NUnit.Framework;
using Should;

namespace NetPointDNS.Tests.Unit.Dtos.Request
{
    [TestFixture]
    public class ZoneRequestTests
    {
        private const string Name = "example.com";
        private const string Group = "Example Group";
        private const string Template = "Example Template";

        [Test]
        public void should_correctly_prepare_full_dto()
        {
            var dto = new ZoneRequest
            {
                Group = Group,
                Name = Name,
                Template = Template
            };

            var json = dto.Prepare();

            json.ShouldContain(Name);
            json.ShouldContain(Group);
            json.ShouldContain(Template);
        }

        [Test]
        public void should_correctly_prepare_dto_without_group()
        {
            var dto = new ZoneRequest
            {
                Name = Name,
                Template = Template
            };

            var json = dto.Prepare();
            json.ShouldContain(Name);
            json.ShouldNotContain(Group);
            json.ShouldContain(Template);
        }

        [Test]
        public void should_correctly_prepare_dto_without_template()
        {
            var dto = new ZoneRequest
            {
                Group = Group,
                Name = Name
            };

            var json = dto.Prepare();
            json.ShouldContain(Name);
            json.ShouldContain(Group);
            json.ShouldNotContain(Template);
        }

        [Test]
        public void should_correctly_prepare_dto_with_just_name()
        {
            var dto = new ZoneRequest
            {
                Name = Name
            };

            var json = dto.Prepare();
            json.ShouldContain(Name);
            json.ShouldNotContain(Group);
            json.ShouldNotContain(Template);
        }
    }
}
