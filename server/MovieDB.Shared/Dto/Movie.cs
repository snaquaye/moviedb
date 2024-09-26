namespace MovieDB.Shared.Dto;

public class Movie
{
    public string? Title { get; init; }
    public string? Year { get; init; }
    public string? Rated { get; init; }
    public string? Released { get; init; }
    public string? Runtime { get; init; }
    public string? Genre { get; init; }
    public string? Director { get; init; }
    public string? Writer { get; init; }
    public string? Actors { get; init; }
    public string? Plot { get; init; }
    public string? Language { get; init; }
    public string? Country { get; init; }
    public string? Awards { get; init; }
    public Uri? Poster { get; init; }
    public Rating[]? Ratings { get; init; }
    public string? Metascore { get; init; }
    public string? ImdbRating { get; init; }
    public string? ImdbVotes { get; init; }
    public string? ImdbId { get; init; }
    public string? Type { get; init; }
    public string? Dvd { get; init; }
    public string? BoxOffice { get; init; }
    public string? Production { get; init; }
    public string? Website { get; init; }
    public string? Response { get; init; }
}