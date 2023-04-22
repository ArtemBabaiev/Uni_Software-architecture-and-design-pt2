using Serilog;
using Server.Configuration;
using Server.DTOs;
using Server.ObjectManagers;
using System.Net;
using System.Text;
using System.Text.Json;

namespace Server.Utils
{
    internal static class ResponseHelper
    {
        private static ILogger logger = Log.Logger;
        public static void PackResponse(HttpListenerContext ctx, ActionResponse action, string contentType = "application/json")
        {
            using (var response = ctx.Response)
            {
                response.ContentType = contentType;
                response.StatusCode = (int)action.StatusCode;
                if (action.ResponseObj != null)
                {
                    var buffer = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(action.ResponseObj));
                    response.OutputStream.Write(buffer, 0, buffer.Length);
                }

                logger.Information($"Request finished HTTP/{ctx.Request.ProtocolVersion} {ctx.Request.HttpMethod} {ctx.Request.Url.PathAndQuery} - - - {(int) action.StatusCode}");
            }
        }

        public static RequestData UnpackRequest(HttpListenerRequest request, string apiPath)
        {
            var url = request.Url;
            var qParams = GetQueryParams(url.Query);
            var noApi = url.AbsolutePath.Replace(ApiPath.Author, "");

            var requestData = new RequestData() { AbsolutePath = url.AbsolutePath, QueryParams = qParams, NoApiPath = noApi };

            if (!request.HasEntityBody)
            {
                return requestData;
            }
            string json;
            using (var reader = new StreamReader(request.InputStream, request.ContentEncoding))
            {
                json = reader.ReadToEnd();
            }
            requestData.Json = json;
            return requestData;

        }

        private static Dictionary<string, string> GetQueryParams(string query)
        {
            var keyPairString = query.Split('&');
            Dictionary<string, string> qParams = new Dictionary<string, string>();

            foreach (var str in keyPairString)
            {
                try
                {
                    var keyValue = str.Split('?');
                    qParams.Add(keyValue[0], keyValue[1]);
                }
                catch (Exception) { }
            }
            return qParams;
        }
    }
}
