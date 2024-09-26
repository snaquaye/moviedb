using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MovieDB.Core.Repositories;
using MovieDB.Infrastructure.Data;
using MovieDB.Infrastructure.Repository;

namespace MovieDB.Infrastructure;

public static class Extension
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connection = configuration.GetConnectionString("DefaultConnection");
        
        services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connection));
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<ISearchHistoryRepository, SearchHistoryRepository>();
    }
}