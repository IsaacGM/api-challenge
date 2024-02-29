using System.Net;

namespace ApiApplication.Abstractions.Errors
{
    public interface IApiException
    {
        public HttpStatusCode StatusCode { get; }
        public string ErrorMessage { get; }
    }
}
