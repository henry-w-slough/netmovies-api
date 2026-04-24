using System.ComponentModel.DataAnnotations;


public class MovieCreateRequest
{
    [Required]
    [MinLength(1)]
    public string? Name {get; set;}

    [Required]
    [MinLength(1)]
    public string? Description {get; set;}
}