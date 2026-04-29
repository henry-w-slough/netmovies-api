

/// <summary>
/// Exception thrown when a Movie is not found when called from the database.
/// </summary>
class MovieNotFoundException : Exception
{

    public MovieNotFoundException(string message) : base(message)
    {
        
    }
}