using RestSharp;
using System.Net;

namespace Demo.Ui
{
    public static class RestResponseExtenstion
    {
        public static bool IsOK(this IRestResponse response)
        {
            if (response.StatusCode == HttpStatusCode.OK)
                 return true;
            else
                return false;

        }
    }
}
