using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


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
    public Movie CreateMovie(MovieCreateRequest request)
    {
        if (!ModelState.IsValid)
        {
            throw new InvalidInputException("Movie Create Request is invalid.", ModelState);
        }
        //accepted create request
        else {
            //creating a new Movie instance to pass on to dbContext
            Movie newMovie = new Movie {    
                Name = request.Name,
                Description = request.Description,
                DateAdded = DateTime.Now
            };


            try
            {
                dbContext.Movies.Add(newMovie);
                dbContext.SaveChanges();
            }
            //catching Concurrency because it's the more specific error and also a subclass to DbUpdateException
            catch (DbUpdateConcurrencyException exception)
            {
                throw new DbUpdateConcurrencyException("Requested Movie currently being modified.", exception);
            }
            //the more general exception, these are both bubbled up to and caught in the global exception handler
            catch (DbUpdateException exception)
            {
                throw new DbUpdateException("Failed to save requested Movie to database.", exception);
            } 


            return newMovie;
        }
    }


    [HttpGet]
    public IActionResult GetMovieById(int id)
    {
        Movie? movie = dbContext.Movies.Find(id);
        if (movie == null)
        {
            throw new MovieNotFoundException($"Movie requested of Id: {id} could not be found.");
        }

        //note to self: this is the reason we use a IActionResult
        //Returning Ok allows global exception handler to do it's thing
        //while the api continues to work.
        return Ok(movie);
    }


}