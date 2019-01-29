using System;

namespace NetPointDNS.Exceptions
{
    public class UnprocessableEntityException : Exception
    {
        public UnprocessableEntityException(string message)
            : base(message) => HelpLink = "https://www.pointhq.com/api/docs/";
    }
}
