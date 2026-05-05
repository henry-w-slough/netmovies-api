using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


/// <summary>
/// Handles all Movie-related HTTP request operations.
/// </summary>
[ApiController]
[Route("movies/[controller]")]
public class MovieController : ControllerBase
{

    //reference to the database for this object
    private MovieDbContext dbContext;


    /// <summary>
    /// Creates new instance of MovieController
    /// </summary>
    /// <param name="context">The context of the database.</param>
    public MovieController(MovieDbContext context)
    {
        dbContext = context;
    }


    [HttpPost]
    public Movie AddMovie(MovieCreateRequest request)
    {
        //creating a new Movie instance to pass on to dbContext
        Movie newMovie = new Movie {    
            Name = request.Name,
            Description = request.Description,
            DateAdded = DateTime.Now
        };

        //Adds new movie instance in-memory
        dbContext.Movies.Add(newMovie);
        dbContext.SaveChanges();

        return newMovie;
    }


    //restrains id param to int as specified
    [HttpGet("/movies/{id:int}")]
    public async Task<IActionResult> GetMovieById(int id)
    {
        //using Task for async operation and IActionResult
        Movie? movieToReturn = await dbContext.Movies.FindAsync(id);

        if (movieToReturn == null)
        {
            throw new MovieNotFoundException($"Requested movie of Id: {id} was not found in the database.");
        }

        return Ok(movieToReturn);
    }


    //NOTE: using {name} restrains name to string, as by default it is string
    [HttpGet("/movies/{name}")]
    public async Task<IActionResult> GetAllMoviesByName(string name)
    {
        List<Movie> moviesToReturn = await dbContext.Movies
            //filtering based on found names. Note that names are case-insensitive
            .Where(m => m.Name != null && m.Name == name)
            //enumerates through given values and creates a list of it
            .ToListAsync();

        if (!moviesToReturn.Any())
        {
            throw new MovieNotFoundException($"No movies of Name: {name} found in database.");
        }

        return Ok(moviesToReturn);
    }

}