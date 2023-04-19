using System.Net;

namespace Server.DTOs
{
    internal class ActionResponse
    {
        public object? ResponseObj { get; protected set; }
        public HttpStatusCode StatusCode { get; protected set; }
        public ActionResponse(object? responseObj, HttpStatusCode statusCode)
        {
            ResponseObj = responseObj;
            StatusCode = statusCode;
        }

        public ActionResponse(HttpStatusCode statusCode) : this(null, statusCode)
        {
        }
    }
}
