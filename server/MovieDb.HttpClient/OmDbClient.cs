using System.Net.Http.Json;
using Ardalis.Result;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MovieDB.Services.Abstractions;
using MovieDB.Shared.Dto;

namespace MovieDb.HttpClient;

public class OmDbClient(
    System.Net.Http.HttpClient httpClient,
    ILogger<OmDbClient> logger,
    IRedisCacheService redisCacheService,
    IConfiguration configuration)
    : IOmDbClient
{
    public async Task<Result<SearchResult?>> SearchMoviesAsync(string query, string page = "1")
    {
        try
        {
            var apiKey = configuration["OmDb:ApiKey"];
            var requestUri = $"?apikey={apiKey}&s={query}&page={page}";
            var cacheKey = $"{query}_{page}";
            var searchResult = await redisCacheService.GetCachedData<SearchResult>(cacheKey);

            if (searchResult is not null)
            {
                return Result.Success(searchResult)!;
            }

            var response = await httpClient.GetAsync(requestUri);

            response.EnsureSuccessStatusCode();

            searchResult = await response.Content.ReadFromJsonAsync<SearchResult>();
            await redisCacheService.SetCachedData<SearchResult>(cacheKey, searchResult!, TimeSpan.FromMinutes(1));
            return Result.Success(searchResult);
        }
        catch (HttpRequestException ex)
        {
            logger.LogError(ex, "Error searching for movies with OMDb");
            return Result.Error("Error communicating with OMDb");
        }
    }

    public async Task<Result<Movie>> GetMovieAsync(string id)
    {
        var apiKey = configuration["OmDb:ApiKey"];
        var requestUri = $"?apikey={apiKey}&i={id}";

        var movie = await redisCacheService.GetCachedData<Movie>(id);
        if (movie is not null)
        {
            return Result.Success(movie);
        }

        var response = await httpClient.GetAsync(requestUri);

        if (response.IsSuccessStatusCode)
        {
            movie = await response.Content.ReadFromJsonAsync<Movie>();
            await redisCacheService.SetCachedData<Movie>(id, movie!, TimeSpan.FromMinutes(1));
            return movie is not null
                ? Result.Success(movie)
                : Result.NotFound();
        }

        return Result.Error("Error retrieving movie from OMDb");
    }
}