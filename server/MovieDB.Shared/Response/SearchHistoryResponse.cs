namespace MovieDB.Shared.Response;

public class SearchHistoryResponse
{
    public IEnumerable<string?> SearchTerms { get; set; }
}