using System;

namespace NetPointDNS.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string message) 
            : base(message) => HelpLink = "https://www.pointhq.com/api/docs/";
    }
}
