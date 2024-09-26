using System.Globalization;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using MovieDB.Api.Contracts;
using MovieDB.Infrastructure.Data;

namespace MovieDB.Api;

public static class Extension
{
    public static WebApplication MapAllEndpoints(this WebApplication app)
    {
        var endPointType = typeof(IEndpoint);

        var assembly = Assembly.GetExecutingAssembly();

        var endpointTypes = assembly.GetExportedTypes()
            .Where(t => t.IsAbstract == false &&
                        t.GetInterfaces().Contains(endPointType));

        foreach (var type in endpointTypes)
        {
            if (Activator.CreateInstance(type) is IEndpoint instance)
            {
                instance.AddEndpoints(app);
            }
        }

        return app;
    }
    
    public static void ApplyMigrations(this WebApplication app)
    {
        try
        {
            using var scope = app.Services.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            
            CultureInfo.DefaultThreadCurrentCulture = CultureInfo.InvariantCulture;
            CultureInfo.DefaultThreadCurrentUICulture = CultureInfo.InvariantCulture;
            
            db.Database.Migrate();
            db.Database.EnsureCreated();
        }
        catch (CultureNotFoundException ex)
        {
            Console.WriteLine($"Culture not found: {ex.Message}");
            Console.WriteLine($"Culture name: {ex.ToString()}");
            Console.WriteLine($"Inner exception: {ex.InnerException?.Message}");
        }
    }
}