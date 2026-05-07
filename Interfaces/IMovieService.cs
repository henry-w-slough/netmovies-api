using Microsoft.AspNetCore.Mvc;


/// <summary>
/// The interface for a MovieService. Defines a contract for a MovieService implementation
/// </summary>
public interface IMovieService
{
    //In practice, these would be used as async
    //also note these are not IActionResult, that is returned in the controller
    Task<Movie> CreateMovie(MovieCreateRequest request);
    Task DeleteMovieByStorageId(Guid storageId);
    Task<List<Movie>> GetAllMovies();
    Task<List<Movie>> GetMoviesByName(string name);
}