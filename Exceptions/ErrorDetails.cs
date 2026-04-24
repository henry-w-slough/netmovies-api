


/// <summary>
/// Holds all info related to exceptions that needs to be returned to the user.
/// </summary>
class ErrorDetails
{
    public int StatusCode {get; set;}
    public string? Message {get; set;}
    public string? ExceptionMessage {get; set;}
}