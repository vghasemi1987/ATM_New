namespace Web.Extensions.Middleware
{
    public class ErrorDetails
    {
        public int StatusCode { get; private set; }
        public string Message { get; private set; }
        public string Detail { get; private set; }
        public bool IsError { get; private set; }

        public ErrorDetails(int statusCode, string message, string detail)
        {
            StatusCode = statusCode;
            Message = message;
            Detail = detail;
            IsError = true;
        }
    }
}
