using Server.Network;
using System.Net;

namespace Server.Utils
{
    internal class ActionHelper
    {
        public static ActionData Ok(object responseObj)
        {
            return new ActionData(responseObj, HttpStatusCode.OK);
        }

        public static ActionData NotFound()
        {
            return new ActionData(HttpStatusCode.NotFound);
        }

        public static ActionData BadRequest()
        {
            return new ActionData(HttpStatusCode.BadRequest);
        }

        public static ActionData BadRequest(string errorMessage)
        {
            return new ActionData(new ErrorResponse { ErrorMessage = errorMessage }, HttpStatusCode.BadRequest);
        }

        public static ActionData InternalServerError()
        {
            return new ActionData(HttpStatusCode.InternalServerError);
        }

        public static ActionData InternalServerError(string errorMessage)
        {
            return new ActionData(new ErrorResponse { ErrorMessage = errorMessage }, HttpStatusCode.InternalServerError);
        }
    }
}
