using System.ComponentModel.DataAnnotations;


/// <summary>
/// The request used for creating a new Movie instance.
/// </summary>
public class MovieCreateRequest
{
    [Required]
    [MinLength(3)]
    public string? Name {get; set;}

    //with default value
    public string Description {get; set;} = "No description provided for this movie.";
}