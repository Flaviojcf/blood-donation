using System.Net;

namespace BloodDonationSystem.Domain.Validations
{
    public class ErrorValidation
    {
        public string TraceId { get; private set; }
        public HttpStatusCode Status { get; set; }
        public List<ErrorDetails> Errors { get; private set; }

        public ErrorValidation(HttpStatusCode status)
        {
            TraceId = Guid.NewGuid().ToString();
            Status = status;
            Errors = [];
        }

        public ErrorValidation(string message, HttpStatusCode status)
        {
            TraceId = Guid.NewGuid().ToString();
            Status = status;
            Errors = [];
            AddError(message);
        }

        public class ErrorDetails(string message)
        {
            public string Message { get; private set; } = message;
        }

        public void AddError(string message)
        {
            Errors.Add(new ErrorDetails(message));
        }
    }
}
