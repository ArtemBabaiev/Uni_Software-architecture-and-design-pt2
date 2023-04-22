using System.Net;

namespace Server.Utils
{
    internal class ActionData
    {
        public object? ResponseObj { get; protected set; }
        public HttpStatusCode StatusCode { get; protected set; }
        public ActionData(object? responseObj, HttpStatusCode statusCode)
        {
            ResponseObj = responseObj;
            StatusCode = statusCode;
        }

        public ActionData(HttpStatusCode statusCode) : this(null, statusCode)
        {
        }
    }
}
