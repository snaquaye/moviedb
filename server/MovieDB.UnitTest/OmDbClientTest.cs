using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MovieDb.HttpClient;
using MovieDB.Services.Abstractions;
using MovieDB.Shared.Dto;
using MovieDbB.Test.Mock;
using NSubstitute;

namespace MovieDbB.Test;

public class OmDbClientTests
{
    private readonly IConfiguration _configuration;
    private readonly IRedisCacheService _redisCacheService;
    private readonly ILogger<OmDbClient> _logger;

    public OmDbClientTests()
    {
        // Substitute mocks
        _configuration = Substitute.For<IConfiguration>();
        _logger = Substitute.For<ILogger<OmDbClient>>();
        _redisCacheService = Substitute.For<IRedisCacheService>();
    }

    [Fact]
    public async Task SearchMoviesAsync_ReturnsCachedResult_WhenCacheIsHit()
    {
        // Arrange
        var query = "Inception";
        var page = "1";
        var expectedCacheKey = $"{query}_{page}";
        var cachedResult = await GetMoviesAsync();

        _redisCacheService.GetCachedData<SearchResult>(expectedCacheKey).Returns(cachedResult);
        
        var client = SetupHttpClient("", HttpStatusCode.OK);

        // Act
        var result = await client.SearchMoviesAsync(query, page);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(cachedResult, result.Value);
    }

    [Fact]
    public async Task SearchMoviesAsync_MakesHttpRequest_WhenCacheIsMissed()
    {
        // Arrange
        var query = "Inception";
        var page = "1";
        var expectedCacheKey = $"{query}_{page}";
        var data = await GetMoviesAsync();
    
        _redisCacheService.GetCachedData<SearchResult>(expectedCacheKey).Returns((SearchResult?)null);
        var expectedSearchResult = await GetMoviesAsync();
    
        var httpResponse = new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = JsonContent.Create(expectedSearchResult)
        };
    
        var httpMessageHandler = new MockHttpMessageHandler(httpResponse.Content.ReadAsStringAsync().Result, HttpStatusCode.OK);
        var httpClient = new HttpClient(httpMessageHandler);
        httpClient.BaseAddress = new Uri("http://www.omdbapi.com/");
        var client = new OmDbClient(httpClient, _logger, _redisCacheService, _configuration);
    
        // Act
        var result = await client.SearchMoviesAsync(query, page);
    
        // Assert
        Assert.True(result.IsSuccess);
        expectedSearchResult.Should().BeEquivalentTo(result.Value!);
        await _redisCacheService.Received().SetCachedData<SearchResult>(Arg.Any<string>(), Arg.Any<SearchResult>(), Arg.Any<TimeSpan>());
    }

    [Fact]
    public async Task SearchMoviesAsync_ReturnsError_WhenHttpRequestFails()
    {
        // Arrange
        var query = "Inception";
        var page = "1";
        var expectedCacheKey = $"{query}_{page}";

        _redisCacheService.GetCachedData<SearchResult>(expectedCacheKey).Returns((SearchResult?)null);

        _configuration["OMDbApiKey"].Returns("YOUR_API_KEY");

        var httpResponse = new HttpResponseMessage(HttpStatusCode.BadRequest);

        var httpMessageHandler = new MockHttpMessageHandler(httpResponse.Content.ReadAsStringAsync().Result, HttpStatusCode.BadRequest);
        var httpClient = new HttpClient(httpMessageHandler);
        httpClient.BaseAddress = new Uri("http://www.omdbapi.com/");
        var client = new OmDbClient(httpClient, _logger, _redisCacheService, _configuration);

        // Act
        var result = await client.SearchMoviesAsync(query, page);

        // Assert
        Assert.True(result.IsSuccess == false);
        await _redisCacheService.DidNotReceive()
            .SetCachedData<SearchResult>(Arg.Any<string>(), Arg.Any<SearchResult>(), Arg.Any<TimeSpan>());
    }

    [Fact]
    public async Task GetMovieAsync_ReturnsCachedResult_WhenCacheIsHit()
    {
        // Arrange
        var movieId = "tt1375666";
        var cachedMovie = await GetMovieAsync(movieId);
    
        _redisCacheService.GetCachedData<Movie>(movieId).Returns(cachedMovie);
        
        var httpMessageHandler = new MockHttpMessageHandler("", HttpStatusCode.OK);
        var httpClient = new HttpClient(httpMessageHandler);
        httpClient.BaseAddress = new Uri("http://www.omdbapi.com/");
        var client = new OmDbClient(httpClient, _logger, _redisCacheService, _configuration);
    
        // Act
        var result = await client.GetMovieAsync(movieId);
    
        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(cachedMovie, result.Value);
    }
    
    [Fact]
    public async Task GetMovieAsync_MakesHttpRequest_WhenCacheIsMissed()
    {
        // Arrange
        var movieId = "tt1375666";
        var expectedMovie = await GetMovieAsync(movieId);
    
        _redisCacheService.GetCachedData<Movie>(movieId).Returns((Movie?)null);
    
        _configuration["OMDbApiKey"].Returns("YOUR_API_KEY");
    
        var httpResponse = new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = JsonContent.Create(expectedMovie)
        };
    
        var httpMessageHandler = new MockHttpMessageHandler(httpResponse.Content.ReadAsStringAsync().Result, HttpStatusCode.OK);
        var httpClient = new HttpClient(httpMessageHandler);
        httpClient.BaseAddress = new Uri("http://www.omdbapi.com/");
        var client = new OmDbClient(httpClient, _logger, _redisCacheService, _configuration);
    
        // Act
        var result = await client.GetMovieAsync(movieId);
    
        // Assert
        Assert.True(result.IsSuccess);
        expectedMovie.Should().BeEquivalentTo(result.Value!);
        await _redisCacheService.Received().SetCachedData<Movie>(Arg.Any<string>(), Arg.Any<Movie>(), Arg.Any<TimeSpan>());
    }
    
    [Fact]
    public async Task GetMovieAsync_ReturnsError_WhenHttpRequestFails()
    {
        // Arrange
        var movieId = "tt1375666";
    
        _redisCacheService.GetCachedData<Movie>(movieId).Returns((Movie?)null);
    
        _configuration["OMDbApiKey"].Returns("YOUR_API_KEY");
    
        var httpResponse = new HttpResponseMessage(HttpStatusCode.BadRequest);
    
        var httpMessageHandler = new MockHttpMessageHandler(httpResponse.Content.ReadAsStringAsync().Result, HttpStatusCode.BadRequest);
        var httpClient = new HttpClient(httpMessageHandler);
        httpClient.BaseAddress = new Uri("http://www.omdbapi.com/");
        var client = new OmDbClient(httpClient, _logger, _redisCacheService, _configuration);
    
        // Act
        var result = await client.GetMovieAsync(movieId);
    
        // Assert
        Assert.True(result.IsSuccess == false);
        await _redisCacheService.DidNotReceive().SetCachedData<Movie>(Arg.Any<string>(), Arg.Any<Movie>(), Arg.Any<TimeSpan>());
    }

    private IOmDbClient SetupHttpClient(string response, HttpStatusCode statusCode)
    {
        _configuration["OMDbApiKey"].Returns("YOUR_API_KEY");
        
        var messageHandler = new MockHttpMessageHandler(response, statusCode);
        var httpClient = new HttpClient(messageHandler);
        var client = new OmDbClient(httpClient, _logger, _redisCacheService, _configuration);

        return client;
    }

    private async Task<SearchResult> GetMoviesAsync()
    {
        return new SearchResult
        {
            Search = new List<MovieSearchResult> { new MovieSearchResult()
            {
                Title = "Inception", 
                Poster = "http://image.tmdb.org/t/p/w185//nBNZadXqJSdt05SHLqgT0HuC5Gm.jpg", 
                Year = "2010", 
                Type = "movie", 
                ImdbId = "tt1375666"
            } },
            Response = "True",
            TotalResults = "1"
        };
    }
    
    private async Task<Movie> GetMovieAsync(string id)
    {
        return new Movie
        {
            Title = "Inception",
            Year = "2010",
            Type = "movie",
            Poster = new Uri("http://image.tmdb.org/t/p/w185//nBNZadXqJSdt05SHLqgT0HuC5Gm.jpg"),
            ImdbId = "tt1375666",
            Actors = "Leonardo DiCaprio, Joseph Gordon-Levitt, Elliot Page, Tom Hardy",
            Awards = "Won 4 Oscars. Another 143 wins & 198 nominations.",
            Plot = "A thief, who steals corporate secrets through use of dream-sharing technology, is given the inverse task of planting an idea into the mind of a CEO.",
            Rated = "PG-13",
            Released = "16 Jul 2010",
            Runtime = "148 min",
            Genre = "Action, Adventure, Sci-Fi",
            Director = "Christopher Nolan",
            Writer = "Christopher Nolan",
            Language = "English, Japanese, French",
            Country = "USA, UK"
        };
    }
}
