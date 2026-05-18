

/// <summary>
/// Represents a Movie instance.
/// </summary>
public class Movie
{
    public int Id {get; set;}

    public string? Name {get; set;}
    public string? Description {get; set;}
    public DateTime DateAdded {get; set;}

    public Guid StorageId {get; set;}
}