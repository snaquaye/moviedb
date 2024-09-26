using FluentAssertions;
using NSubstitute;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using MovieDB.Core.Entities;
using MovieDB.Core.Repositories;
using MovieDB.Services;
using MovieDB.Services.Abstractions;

namespace MovieDB.Test
{
    public class SearchHistoryServiceTest
    {
        private readonly ISearchHistoryRepository _searchHistoryRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISearchHistoryService _searchHistoryService;

        public SearchHistoryServiceTest()
        {
            // Initialize the mocks
            _searchHistoryRepository = Substitute.For<ISearchHistoryRepository>();
            _unitOfWork = Substitute.For<IUnitOfWork>();

            // Create the service with the mocked dependencies
            _searchHistoryService = new SearchHistoryService(_searchHistoryRepository, _unitOfWork);
        }

        [Fact]
        public async Task AddAsync_ShouldAddSearchHistory_AndCommit()
        {
            // Arrange
            var searchHistory = new SearchHistory
            {
                SearchTerm = "Inception"
            };
            var ct = CancellationToken.None;

            // Act
            await _searchHistoryService.AddAsync(searchHistory, ct);

            // Assert
            await _searchHistoryRepository.Received(1).AddAsync(searchHistory, ct);
            await _unitOfWork.Received(1).CommitAsync(ct);
        }

        [Fact]
        public void GetAllAsync_ShouldReturnAllSearchHistories()
        {
            // Arrange
            var searchHistories = new List<SearchHistory>
            {
                new SearchHistory { SearchTerm = "Inception" },
                new SearchHistory { SearchTerm = "Interstellar" }
            };

            var page = 1;
            var limit = 10;
            var ct = CancellationToken.None;

            // Mock the repository method to return the search histories
            _searchHistoryRepository
                .GetAllAsync(page, limit, ct)
                .Returns(Task.FromResult((IEnumerable<SearchHistory>)searchHistories));

            // Act
            var result = _searchHistoryService.GetAll(page, limit, ct);

            // Assert
            result.Should().BeEquivalentTo(searchHistories);
        }
    }
}
