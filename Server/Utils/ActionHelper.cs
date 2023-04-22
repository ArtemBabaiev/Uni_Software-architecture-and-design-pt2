using Server.DTOs;
using System.Net;

namespace Server.Utils
{
    internal class ActionHelper
    {
        public static ActionResponse Ok(object responseObj)
        {
            return new ActionResponse(responseObj, HttpStatusCode.OK);
        }

        public static ActionResponse NotFound()
        {
            return new ActionResponse(HttpStatusCode.NotFound);
        }

        public static ActionResponse BadRequest()
        {
            return new ActionResponse(HttpStatusCode.BadRequest);
        }

        public static ActionResponse InternalServerError()
        {
            return new ActionResponse(HttpStatusCode.InternalServerError);

        }
    }
}
