using System.Net;
namespace TravelBookingPortal.Api.Exceptions
{
    public abstract class AppException : Exception
    {
        protected AppException(string message, HttpStatusCode statusCode) : base(message)
        {
            StatusCode = statusCode;
        }

        public HttpStatusCode StatusCode { get; }
    }
}