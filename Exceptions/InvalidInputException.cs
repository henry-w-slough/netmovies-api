using Microsoft.AspNetCore.Mvc.ModelBinding;


/// <summary>
/// Exception thrown when the given client input is invalid.
/// </summary>
class InvalidInputException : Exception
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