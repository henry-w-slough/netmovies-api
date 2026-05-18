using System.Net;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;

/// <summary>
/// Intercepts all unhandled exceptions and acts accordingly. Provides specific output data and info based on the given exception.
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
        else if (exception is DbUpdateConcurrencyException)
        {
            errorDetails.Message = "Data requested for modification has already been modified and the request could not be complete.";
            errorDetails.ExceptionMessage = exception.Message;
            errorDetails.StatusCode = (int) HttpStatusCode.Conflict;
        }
        else if (exception is DbUpdateException)
        {
            errorDetails.Message = "Exception was thrown when attemping to save the database and the request could not be completed.";
            errorDetails.ExceptionMessage = exception.Message;
            errorDetails.StatusCode = (int) HttpStatusCode.ServiceUnavailable;
        }
        else if (exception is System.Data.SqlClient.SqlException)
        {
            errorDetails.Message = "Database returned an exception when trying to access it and the request could not be completed.";
            errorDetails.ExceptionMessage = exception.Message;
            errorDetails.StatusCode = (int) HttpStatusCode.ServiceUnavailable;
        }
        else {
            //handling every other exception if none of the specific ones match
            errorDetails.StatusCode = (int) HttpStatusCode.InternalServerError;
            errorDetails.Message = "An unhandled exception was thrown.";
            errorDetails.ExceptionMessage = $"{exception.GetType().FullName}: {exception.Message}";
        }


        //Configuring HTTP Response for our errorDetails
        httpContext.Response.StatusCode = errorDetails.StatusCode;
        httpContext.Response.ContentType = "application/json";
        
        // Writing errorDetails in a JSON type
        await httpContext.Response.WriteAsJsonAsync(errorDetails, cancellationToken);


        return true;
    }

}