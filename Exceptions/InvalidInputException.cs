using Microsoft.AspNetCore.Mvc.ModelBinding;


/// <summary>
/// This exception is thrown when the requester provides invalid input,
/// whether for a request body or endpoint parameter.
/// </summary>
public class InvalidInputException : Exception
{
    public ModelStateDictionary ModelState {get; set;}

    /// <summary>
    /// Constructor account for a custom message as well as the ModelStateDictionary containing the validation errors.
    /// </summary>
    /// <param name="message">The custom exception message</param>
    /// <param name="modelState">The ModelStateDictionary containing the validation errors</param>
    /// <returns>The InvalidInputException to return</returns>
    public InvalidInputException(string message, ModelStateDictionary modelState): base(message) {
        ModelState = modelState;
    }
}