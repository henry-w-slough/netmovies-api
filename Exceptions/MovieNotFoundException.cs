

/// <summary>
/// Exception thrown when a Movie is not found when called from the database.
/// </summary>
class MovieNotFoundException : Exception
{

    /// <summary>
    /// Create a new MovieNotFoundException.
    /// </summary>
    /// <param name="message">The message provided when the exception is thrown.</param>
    public MovieNotFoundException(string message) : base(message)
    {
        
    }
}