using System.ComponentModel.DataAnnotations;


/// <summary>
/// Request used to create a new Movie.
/// </summary>
public class MovieCreateRequest
{
    [Required]
    [MaxLength(255)]    
    public string? Name {get; set;}

    [MaxLength(255)]
    public string? Description {get; set;} = "No description provided for this movie.";
}