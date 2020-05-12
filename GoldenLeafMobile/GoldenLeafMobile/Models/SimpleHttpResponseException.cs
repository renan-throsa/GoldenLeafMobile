using System;
using System.Net;

namespace GoldenLeafMobile.Models
{
    class SimpleHttpResponseException : Exception
    {
        public HttpStatusCode StatusCode { get; private set; }
        public string ReasonPhrase { get; set; }

        public SimpleHttpResponseException(HttpStatusCode statusCode, string _reasonPhrase, string content)
            : base(content)
        {
            StatusCode = statusCode;
            ReasonPhrase = _reasonPhrase;
        }
    }
}
