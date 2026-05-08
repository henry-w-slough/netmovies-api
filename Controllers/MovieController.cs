using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;


/// <summary>
/// Controls all Movie-related HTTP requests.
/// </summary>
[ApiController]
[Route("metadata/[controller]")]
public class MovieController : ControllerBase
{

    private readonly IMovieService movieService;


    /// <summary>
    /// Creates a new instance of MovieController.
    /// </summary>
    /// <param name="movieService">The service reference.</param>
    public MovieController(IMovieService movieService)
    {
        this.movieService = movieService;
    }


    [HttpPost("createMovie")]
    public async Task<ActionResult<Movie>> CreateMovie(MovieCreateRequest request)
    {
        //checking to see if request matches all required Movie fields.
        if (!ModelState.IsValid)
        {
            throw new InvalidInputException("Movie Create Request is invalid.", ModelState);
        }

        Movie toReturn = await movieService.CreateMovie(request);

        //returns the movie created with no location
        return Created(string.Empty, toReturn);
    }


    [HttpDelete("deleteMovieByStorageId/{storageId:guid}")]
    public async Task<IActionResult> DeleteMovieByStorageId(Guid storageId)
    {   
        await movieService.DeleteMovieByStorageId(storageId);
        //returning successful deletion
        return NoContent();
    }


    //NOTE: using {name} restrains name to string, as by default it is string
    [HttpGet("getMoviesByName/{name}")]
    public async Task<ActionResult<List<Movie>>> GetMoviesByName(string name)
    {
        //getting return from service to return back as response
        List<Movie> toReturn = await movieService.GetMoviesByName(name);
        return Ok(toReturn);
    }


    [HttpGet("getAllMovies")]
    public async Task<ActionResult<List<Movie>>> GetAllMovies()
    {
        List<Movie> toReturn = await movieService.GetAllMovies();
        return Ok(toReturn);
    }

}