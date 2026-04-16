using Microsoft.AspNetCore.Mvc;


class MovieController : ControllerBase
{
    //reference to the database for this object
    private MovieDbContext dbContext;


    public MovieController(MovieDbContext context)
    {
        dbContext = context;
    }


    [HttpPost("/players")]
    public Movie AddMovie(MovieCreateRequest request)
    {
        //creating a new Movie instance to pass on to Service
        Movie newMovie = new Movie { 
            Name = request.Name 
        };

        //Adds new movie instance in-memory
        dbContext.Movies.Add(newMovie);
        dbContext.SaveChanges();

        return newMovie;
    }
}