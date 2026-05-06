using System.ComponentModel.DataAnnotations;


/// <summary>
/// The request used for creating a new Movie instance.
/// </summary>
public class MovieCreateRequest
{
    [Required]
    [MinLength(1)]
    [MaxLength(255)]
    public string? Name {get; set;}

    [MaxLength(255)]
    public string? Description {get; set;} = "No description provided for this movie.";
}