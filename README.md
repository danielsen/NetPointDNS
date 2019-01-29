# NetPointDNS

`NetPointDNS` is a class library for interacting with the [PointHQ](https://pointhq.com) DNS API (PointDNS).
`NetPointDNS` implements all create, read, update, and delete operations on the resources described in the
[PointHQ DNS API documentation](http://pointhq.com/api/docs).

### Features
- Create and manage DNS zones.
- Create and manage DNS zone records.
- Create and manage HTTP redirects and mail redirects.

### Packages

Current Version: `1.0.0`

Target Framework: `.NET Standard 2.0`

### Dependencies

- [Newtonsoft.Json](https://www.nuget.org/packages/Newtonsoft.Json/) v10.0.3+

### Development Dependencies

- [Newtonsoft.Json](https://www.nuget.org/packages/Newtonsoft.Json/) v10.0.3+
- [Microsoft.NET.Test.SDK](https://www.nuget.org/packages/Microsoft.NET.Test.SDK) 
- [Nunit](https://www.nuget.org/packages/Nunit)
- [Nunit3TestAdapter](https://www.nuget.org/packages/Nunit3TestAdapter)
- [NSubstitute](https://www.nuget.org/packages/NSubstitute)
- [Should](https://www.nuget.org/packages/Should)

### Usage

Creating a new API object

```C#
using NetPointDNS;
using NetPointDNS.Resources;
...
var pointApi = new Api("yourUserName", "yourToken");
```

Creating a zone

```C#
var zone = await pointApi.CreateZoneAsync("yourdomain.com");
```

Adding a zone record

```C#
var records = await pointApi.CreateRecordAsync(zone.Id, "www", RecordType.A, "1.2.3.4", 7200);
```

### References

- [Full PointHQ DNS API documentation](https://pointhq.com/api/docs)