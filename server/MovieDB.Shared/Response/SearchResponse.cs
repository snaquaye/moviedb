using MovieDB.Shared.Dto;

namespace MovieDB.Shared.Response;

public class SearchResponse
{
    public IEnumerable<MovieSearchResult>? Movies { get; set; }
    public int TotalResults { get; set; }
}