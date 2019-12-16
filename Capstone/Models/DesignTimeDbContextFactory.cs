using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Capstone.Models
{
  public class CapstoneContextFactory : IDesignTimeDbContextFactory<CapstoneContext>
  {

    CapstoneContext IDesignTimeDbContextFactory<CapstoneContext>.CreateDbContext(string[] args)
    {
      IConfigurationRoot configuration = new ConfigurationBuilder()
          .SetBasePath(Directory.GetCurrentDirectory())
          .AddJsonFile("appsettings.json")
          .Build();

      var builder = new DbContextOptionsBuilder<CapstoneContext>();
      var connectionString = configuration.GetConnectionString("DefaultConnection");

      builder.UseMySql(connectionString);

      return new CapstoneContext(builder.Options);
    }
  }
}
