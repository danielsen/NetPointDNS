using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace NetPointDNS.Resources
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum RecordType
    {
        [EnumMember(Value = "A")]
        A,

        [EnumMember(Value = "CNAME")]
        Cname,

        [EnumMember(Value = "MX")]
        Mx,

        [EnumMember(Value = "TXT")]
        Txt,

        [EnumMember(Value = "SRV")]
        Srv,

        [EnumMember(Value = "AAAA")]
        Aaaa,

        [EnumMember(Value = "SSHFP")]
        Sshfp,

        [EnumMember(Value = "PTR")]
        Ptr,

        [EnumMember(Value = "ALIAS")]
        Alias
    }
}
