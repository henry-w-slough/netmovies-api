using System.ComponentModel.DataAnnotations;


/// <summary>
/// Instance of movie-related information for configuration, referencing, and storage.
/// </summary>
public class Movie
{
    public int Id {get; set;}
    public string? Name {get; set;}
    public DateTime DateAdded {get; set;}
    public string? Description {get; set;}
}
