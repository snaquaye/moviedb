namespace MovieDB.Shared.Dto;

public enum TypeEnum
{
    Movie,
    Series,
}

public class SearchResult
{
    public IList<MovieSearchResult>? Search { get; init; }
    public string? TotalResults { get; init; }
    public string? Response { get; init; }
}