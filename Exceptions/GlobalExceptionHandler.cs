using System.Net;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;


/// <summary>
/// Intercepts all unhandled exceptions and acta accordingly. Provides specific output data and info based on the given exception.
/// </summary>
public class GlobalExceptionHandler : IExceptionHandler
{
    /// <summary>
    /// Catches exceptions and returns an ErrorDetails object for related data.
    /// </summary>
    /// <param name="httpContext">Information about the current HTTP request.</param>
    /// <param name="exception">The given exception being asked to be handled.</param>
    /// <param name="cancellationToken">The request for the task to stop working when needed.</param>
    /// <returns>Returns True or False based on whether the exception was successfully handled or not.</returns>
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        //object of ErrorDetails which will be filled out based on the error thrown
        ErrorDetails errorDetails = new ErrorDetails();


        if (exception is MovieNotFoundException)
        {
            //filling out errorDetails based on the MovieNotFoundException
            errorDetails.Message = "Movie requested was not found and could not be returned.";
            errorDetails.ExceptionMessage = exception.Message;
            errorDetails.StatusCode = (int) HttpStatusCode.NotFound;
        }
        if (exception is DbUpdateConcurrencyException)
        {
            errorDetails.Message = "Requested update is already being modified, the operation could not be performed.";
            errorDetails.ExceptionMessage = exception.Message;
            errorDetails.StatusCode = (int) HttpStatusCode.Conflict;
        }
        else {
            //handling every other exception if none of the specific ones match
            errorDetails.StatusCode = (int) HttpStatusCode.InternalServerError;
            errorDetails.Message = "Something went wrong.";
            errorDetails.ExceptionMessage = exception.Message;
        }


        //Configuring HTTP Response for our errorDetails
        httpContext.Response.StatusCode = errorDetails.StatusCode;
        httpContext.Response.ContentType = "application/json";
        
        // Writing errorDetails in a JSON type
        await httpContext.Response.WriteAsJsonAsync(errorDetails, cancellationToken);


        return true;
    }


    /// <summary>
    /// Generates a descriptive error message that contains all validation errors.
    /// </summary>
    /// <param name="exception">The InvalidInputException to generate the error message for.</param>
    /// <returns>The error message to return.</returns>
    private string GenerateInvalidInputMessage(InvalidInputException exception)
    {
        string message = exception.Message;

        // We are going to add the entire collection of validation errors to our single error message to return.
        // For each value in our model state dictionary, we are going to put all of their error message's in to a list.
        List<string> errors = exception.ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)).ToList();

        // Join the error message list back into a single string and append to the original message.
        message += string.Join(", ", errors); 

        return message;
    }

}