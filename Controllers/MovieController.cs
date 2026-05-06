using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


/// <summary>
/// Controls all Movie-related HTTP requests and acts accordingly to them.
/// </summary>
[ApiController]
[Route("movies/[controller]")]
public class MovieController : ControllerBase
{

    //reference to the database for this object
    private MovieDbContext dbContext;


    /// <summary>
    /// Creates new instances of MovieController.
    /// </summary>
    /// <param name="context">The context to the database, used for database reference.</param>
    public MovieController(MovieDbContext context)
    {
        dbContext = context;
    }


    [HttpPost]
    public async Task<ActionResult<Movie>> CreateMovie(MovieCreateRequest request)
    {
        //checking to see if request matches all required Movie fields.
        if (!ModelState.IsValid)
        {
            throw new InvalidInputException("Movie Create Request is invalid.", ModelState);
        }

        //instance of Movie to be filled out
        Movie newMovie = new Movie
        {
            Name = request.Name,
            Description = request.Description,
            DateAdded = DateTime.Now
        };

        try
        {
            dbContext.Movies.Add(newMovie);
            await dbContext.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException exception)
        {
            throw new DbUpdateConcurrencyException("Requested Movie currently being modified.", exception);
        }
        catch (DbUpdateException exception)
        {
            throw new DbUpdateException("Failed to save requested Movie to database.", exception);
        }

        //returning 201 for Created. We use string.Empty to show that we deliberately didn't give location.
        return Created(string.Empty, newMovie);
    }


    [HttpGet]
    public async Task<ActionResult<Movie>> GetMovieById([FromQuery] int id)
    {
        Movie? movie = await dbContext.Movies.FindAsync(id);

        if (movie == null)
        {
            throw new MovieNotFoundException($"Movie requested of Id: {id} could not be found.");
        }

        //note to self: this is the reason we use a IActionResult
        //Returning Ok allows program to continue while GEH can do it's thing,
        //Errors can just be treated as any other data structure
        return Ok(movie);
    }


}