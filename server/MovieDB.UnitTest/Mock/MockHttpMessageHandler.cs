using System.Net;

namespace MovieDbB.Test.Mock;

public class MockHttpMessageHandler(string response, HttpStatusCode statusCode) : HttpMessageHandler
{
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        return new HttpResponseMessage
        {
            StatusCode = statusCode,
            Content = new StringContent(response)
        };
    }
}