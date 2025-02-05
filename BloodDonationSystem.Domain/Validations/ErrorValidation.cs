using System.Net;

namespace BloodDonationSystem.Domain.Validations
{
    public class ErrorValidation
    {
        public string TraceId { get; private set; }
        public List<ErrorDetails> Errors { get; private set; }

        public ErrorValidation(string message, HttpStatusCode status)
        {
            TraceId = Guid.NewGuid().ToString();
            Errors = [];
            AddError(message, status);
        }

        public class ErrorDetails(string message, HttpStatusCode status)
        {
            public string Message { get; private set; } = message;
            public HttpStatusCode Status { get; set; } = status;
        }

        public void AddError(string message, HttpStatusCode status)
        {
            Errors.Add(new ErrorDetails(message, status));
        }
    }
}
