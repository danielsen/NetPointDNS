using System;
using System.Text;

namespace NetPointDNS.Http
{
    public class Credentials
    {
        public string User { get; set; }
        public string Token { get; set; }

        public string Encode()
        {
            var bytes = Encoding.UTF8.GetBytes($"{User}:{Token}");
            return Convert.ToBase64String(bytes);
        }
    }
}
