using Microsoft.AspNetCore.Mvc;


[ApiController]
[Route("movies/[controller]")]
public class MovieController : ControllerBase
{
    //reference to the database for this object
    private MovieDbContext dbContext;


    public MovieController(MovieDbContext context)
    {
        dbContext = context;
    }


    [HttpPost]
    public Movie AddMovie(MovieCreateRequest request)
    {
        //creating a new Movie instance to pass on to Service
        Movie newMovie = new Movie { 
            Name = request.Name,
            Description = request.Description,
            DateAdded = request.DateAdded
        };

        //Adds new movie instance in-memory
        dbContext.Movies.Add(newMovie);
        dbContext.SaveChanges();

        return newMovie;
    }
}