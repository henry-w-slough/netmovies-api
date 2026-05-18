using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


/// <summary>
/// The Service which handles all direct logic related to Movie HTTP Requests.
/// </summary>
public class MovieService : IMovieService
{
    private MovieDbContext dbContext;

    /// <summary>
    /// Creates a new MovieService.
    /// </summary>
    /// <param name="context">The context to the database.</param>
    public MovieService(MovieDbContext context)
    {
        dbContext = context;
    }


    public Task<Movie> CreateMovie(MovieCreateRequest request)
    {
        return dbContext.CreateMovie(
            new Movie {
                Name = request.Name,
                Description = request.Description,
                DateAdded = DateTime.Now,
                StorageId = Guid.NewGuid()
            }
        );
    }


    public Task DeleteMovieByStorageId(Guid storageId)
    {
        return dbContext.DeleteMovieByStorageId(storageId);
    }


    public Task<List<Movie>> GetAllMovies()
    {
        return dbContext.GetAllMovies();
    }


    public Task<List<Movie>> GetMoviesByName(string name)
    {
        return dbContext.GetMoviesByName(name);
    }


}