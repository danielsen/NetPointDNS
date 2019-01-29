using System;

namespace NetPointDNS.Exceptions
{
    public class ForbiddenException : Exception
    {
        public ForbiddenException(string message)
            : base(message) => HelpLink = "https://www.pointhq.com/api/docs/";
    }
}
