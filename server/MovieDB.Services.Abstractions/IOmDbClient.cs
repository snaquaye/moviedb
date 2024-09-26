using Ardalis.Result;
using MovieDB.Shared.Dto;

namespace MovieDB.Services.Abstractions;

public interface IOmDbClient
{
    Task<Result<SearchResult?>> SearchMoviesAsync(string query, string page = "1");

    Task<Result<Movie>> GetMovieAsync(string id);
}