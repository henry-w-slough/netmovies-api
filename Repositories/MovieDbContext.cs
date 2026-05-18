using Microsoft.EntityFrameworkCore;


public class MovieDbContext : DbContext
{

    //ref to row for movies in db
    public DbSet<Movie> Movies {get; set;}


    public MovieDbContext(DbContextOptions<MovieDbContext> options) : base(options)
    {
        
    }


    public async Task<List<Movie>> GetAllMovies()
    {
        return await Movies.ToListAsync();
    }


    public async Task<List<Movie>> GetMoviesByName(string name)
    {
        //dont have to load into memory just return, no reason to throw error
        return await Movies
            //filtering based on found matches in Movies dbSet
            .Where(m => m.Name != null && m.Name == name)
            //creates a list of it
            .ToListAsync();
    }


    public async Task<Movie> CreateMovie(Movie movie)
    {
        await Movies.AddAsync(movie);
        await SaveChangesAsync();

        return movie;
    } 


    public async Task DeleteMovieByStorageId(Guid storageId)
    {
        Movie? movieToDelete = await Movies
            //filtering based on found id.
            .FirstOrDefaultAsync(m => m.StorageId == storageId);
            
        if (movieToDelete == null)
        {
            throw new MovieNotFoundException("Movie of StorageId: {storageId} not found in the database.");
        }

        //removing movie and saving changes
        Movies.Remove(movieToDelete);
        await SaveChangesAsync();
    }


}