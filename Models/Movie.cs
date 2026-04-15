


/// <summary>
/// Instance of movie data-related information for configuration, referencing, and storage.
/// </summary>
public class Movie
{

    string Id {get; set;}
    string Name {get; set;}


    public Movie (string name, string id) {
        Name = name;
        //Note: Even though Id is set through a parameter, Id should be generated in a Service nontheless
        Id = id;
    }
}