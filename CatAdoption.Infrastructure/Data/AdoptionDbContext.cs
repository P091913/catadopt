using Microsoft.EntityFrameworkCore;
using CatAdoption.Domain.Models;
using CatAdoption.Infrastructure.Data.EntityConfigurations;

namespace CatAdoption.Infrastructure.Data;


// dbcontext is how we interact with our database from c#
// dbcontext will know everything about our database, the tables, columns
// know any configs we've set up
// track our models and use them to retrieve and update data in the database
public class AdoptionDbContext : DbContext
{
    public AdoptionDbContext(DbContextOptions<AdoptionDbContext> options) : base(options)
    {
        
    }
    
    // creating our tables
    // DbSet<T> is a generic type that allows us to use our model classes
    // -- _context.Cats.GetAllAsync();
    
    public DbSet<Cat> Cats { get; set; }
    public DbSet<Owner> Owners { get; set; }

    // This determines what database we're using
    // this will be for local testing - later we'll utilize our contextfactory file and connect it using program.cs
    // 
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlite("Data Source=cats.db");
        }
    }

    // when your application and your setup this service or scope, this is the first thing that runs
    // any primary keys, relationships, constraints, length of fields)
    // whatever we define in our config, will determine how our database is set up
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // rules / config file
        modelBuilder.ApplyConfiguration(new OwnerConfiguration());
    }
}