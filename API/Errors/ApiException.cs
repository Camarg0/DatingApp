namespace API.Errors;


// Details is gonna be the field that represents the stack trace, not always present
public class ApiException(int statusCode, string message, string? details)
{
    public int StatusCode { get; set; } = statusCode;
    public string Message { get; set; } = message;
    public string? Details { get; set; } = details;
}
