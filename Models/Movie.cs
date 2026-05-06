using System.ComponentModel.DataAnnotations;


/// <summary>
/// Instance of movie-related information for reference.
/// </summary>
public class Movie
{
    public int Id {get; set;}
    public string? Name {get; set;}
    public DateTime DateAdded {get; set;}
    public string? Description {get; set;}

    //Note: MovieUuid is only practically used in the Python on-disk movie storage. It is sent in http requests for use of movie-searching there.
    public Guid StorageId {get; set;}
}
