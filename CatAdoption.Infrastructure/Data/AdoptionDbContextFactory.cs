using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace CatAdoption.Infrastructure.Data;

public class AdoptionDbContextFactory : IDesignTimeDbContextFactory<AdoptionDbContext>
{
    public AdoptionDbContext CreateDbContext(string[] args)
    {
        // setup our builder
        // it's required because we don't have our main application to piggy back off of the scope being added
        var optionsBuilder = new DbContextOptionsBuilder<AdoptionDbContext>();

        optionsBuilder.UseSqlite("Data Source=cats.db");
        
        // return empty context so our DbSet properties and config are available and can be read in
        return new AdoptionDbContext(optionsBuilder.Options);
    }
}