using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;

namespace Infrastructure.Data.Context
{
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {

            string environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";
            Console.WriteLine($"Environment detected: {environment}");

            string connectionString = ParseConnectionStringFromCommandLine(args);
            var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
            builder.UseRDMS(connectionString);

            return new ApplicationDbContext(builder.Options);
        }
        private string ParseConnectionStringFromCommandLine(string[] args)
        {
            foreach (var arg in args)
            {
                if (arg.StartsWith("--connection="))
                {
                    return arg.Substring("--connection=".Length);
                }
            }
            Console.WriteLine(JsonSerializer.Serialize(args));

            throw new ArgumentException("Connection string not found in command-line arguments. Expected format: dotnet ef database update -- --connection=<CONNECTION_STRING>");
        }
    }
}
