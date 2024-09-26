using Microsoft.AspNetCore.Builder;

namespace MovieDB.Api.Contracts;

public interface IEndpoint
{
    public void AddEndpoints(WebApplication app);
}