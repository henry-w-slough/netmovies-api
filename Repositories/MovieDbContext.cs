using Microsoft.EntityFrameworkCore;


/// <summary>
/// The Connection to the database, specifically for Movie-related operations and storage.
/// </summary>
public class MovieDbContext : DbContext
{
    //reference to db table for movies
    public DbSet<Movie> Movies {get; set;}
    

    public MovieDbContext(DbContextOptions<MovieDbContext> options) : base(options)
    {
        
    }
}