using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
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


    [HttpDelete("deleteMovieByStorageId/{storageId:guid}")]
    public async Task<IActionResult> DeleteMovieByStorageId(Guid storageId)
    {   
        Movie? movieToDelete = await dbContext.Movies
            //filtering based on found id.
            .FirstOrDefaultAsync(m => m.StorageId == storageId);

        if (movieToDelete == null)
        {
            throw new MovieNotFoundException($"Movie of StorageId: {storageId} not found in database.");
        }

        //removing movie and saving changes
        dbContext.Movies.Remove(movieToDelete);
        await dbContext.SaveChangesAsync();

        //returning successful deletion
        return NoContent();
    }


    //NOTE: using {name} restrains name to string, as by default it is string
    [HttpGet("getAllMoviesByName/{name}")]
    public async Task<ActionResult<Movie>> GetAllMoviesByName(string name)
    {
        List<Movie> moviesToReturn = await dbContext.Movies
            //filtering based on found names. Note that names are case-insensitive
            .Where(m => m.Name != null && m.Name == name)
            //creates a list of it
            .ToListAsync();

        if (!moviesToReturn.Any())
        {
            throw new MovieNotFoundException($"No movies of Name: {name} found in database.");
        }

        return Ok(moviesToReturn);
    }

}