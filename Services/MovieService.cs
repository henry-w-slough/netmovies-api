using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


/// <summary>
/// The Service which handles all direct logic related to Movie HTTP Requests.
/// </summary>
public class MovieService : IMovieService
{
    //context of the database
    private MovieDbContext dbContext;

    
    /// <summary>
    /// Creates a new MovieService.
    /// </summary>
    /// <param name="context">The context to the database.</param>
    public MovieService(MovieDbContext context)
    {
        dbContext = context;
    }


    async public Task<Movie> CreateMovie(MovieCreateRequest request)
    {

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

        return newMovie;
    }


    public async Task DeleteMovieByStorageId(Guid storageId)
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
    }


    public async Task<List<Movie>> GetAllMovies()
    {
        return await dbContext.Movies.ToListAsync();
    }


    public async Task<List<Movie>> GetMoviesByName(string name)
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

        return moviesToReturn;
    }


}