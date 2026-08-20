using System.Net;
namespace TravelBookingPortal.Api.Exceptions
{
    public class InsufficientSeatsException : AppException
    {
        public InsufficientSeatsException(string message) : base(message, HttpStatusCode.BadRequest)
        {
        }
    }
}
