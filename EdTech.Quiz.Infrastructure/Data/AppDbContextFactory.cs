using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace EdTech.Quiz.Infrastructure.Data;

public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {

        var basePath = Path.Combine(Directory.GetCurrentDirectory(), "../EdTech.Quiz.WebAPI");
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(basePath)
            .AddJsonFile("appsettings.json")
            .Build();

        DbContextOptionsBuilder<AppDbContext>? builder = new();

        var connectionString = configuration.GetConnectionString("MyConnectionString");
        builder.UseNpgsql(connectionString);

        return new AppDbContext(builder.Options);

    }

}
