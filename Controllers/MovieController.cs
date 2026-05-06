using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


/// <summary>
/// Controls all Movie-related HTTP requests and acts accordingly to them.
/// </summary>
[ApiController]
[Route("metadata/[controller]")]
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


    [HttpPost("createMovie")]
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
            DateAdded = DateTime.Now,
            StorageId = Guid.NewGuid()
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


    [HttpDelete("deleteMovieByMovieId/{id:int}")]
    public async Task<ActionResult<Movie>> DeleteMovieByMovieId(int id)
    {
        Movie? movie = dbContext.Movies.Find(id);

        if (movie == null)
        {
            throw new MovieNotFoundException($"Movie requested of Id: {id} could not be found.");
        }

        return Ok(movie);
    }


    //NOTE: using {name} restrains name to string, as by default it is string
    [HttpGet("getAllMoviesByName/{name}")]
    public async Task<ActionResult<Movie>> GetAllMoviesByName(string name)
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