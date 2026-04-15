using Microsoft.EntityFrameworkCore;


/// <summary>
/// The Connection to the database, specifically for Movie model and operation storage.
/// </summary>
public class MovieDbContext : DbContext
{
    //connection to table in database for Movies.
    public DbSet<Movie> Movies {get; set;}
    

    public MovieDbContext(DbContextOptions<MovieDbContext> options) : base(options)
    {
        
    }
}