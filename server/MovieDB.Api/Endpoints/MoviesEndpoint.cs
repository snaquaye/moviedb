using Microsoft.AspNetCore.Mvc;
using MovieDB.Api.Contracts;
using MovieDB.Services.Abstractions;
using MovieDB.Shared.Dto;
using MovieDB.Shared.Response;

namespace MovieDB.Api.Endpoints;

public sealed class MoviesEndpoint : IEndpoint
{
    public void AddEndpoints(WebApplication app)
    {
        app.MapGet("/api/v1/movies", SearchMoviesAsync)
            .WithName("SearchMovies")
            .Produces<SearchResponse>()
            .WithOpenApi();
        app.MapGet("/api/v1/movies/{id}", GetMovieAsync)
            .WithName("GetMovie")
            .Produces<Movie>()
            .WithOpenApi();
    }

    private async Task<SearchResponse> SearchMoviesAsync(string query, [FromServices] IOmDbClient omDbClient, string page = "1")
    {
        if (query.Length == 0)
        {
            return new SearchResponse()
            {
                Movies = new List<MovieSearchResult>(),
                TotalResults = 0,
            };
        }

        var result = await omDbClient.SearchMoviesAsync(query, page);
        var movies = result.Value?.Search!.Select(m => new MovieSearchResult()
        {
            Title = m.Title!,
            Type = m.Type!,
            Year = m.Year!,
            ImdbId = m.ImdbId!,
            Poster = m.Poster!.ToString(),
        });

        return new SearchResponse()
        {
            Movies = movies,
            TotalResults = int.Parse(result.Value?.TotalResults!),
        };
    }

    private async Task<Movie> GetMovieAsync(string id, [FromServices] IOmDbClient omDbClient)
    {
        var result = await omDbClient.GetMovieAsync(id);
        return result.Value!;
    }
}