using System.Net;

namespace TravelBookingPortal.Api.Exceptions
{
    public class NotFoundException : AppException
    {
        public NotFoundException(string message) : base(message, HttpStatusCode.NotFound)
        {
        }
    }
}
