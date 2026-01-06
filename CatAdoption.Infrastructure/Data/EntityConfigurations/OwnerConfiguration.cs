using CatAdoption.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CatAdoption.Infrastructure.Data.EntityConfigurations;

public class OwnerConfiguration : IEntityTypeConfiguration<Owner>
{
    public void Configure(EntityTypeBuilder<Owner> builder)
    {
        // primary key
        // CREATE TABLE owners (id INT PRIMARY KEY, other properties)
        builder.HasKey(o => o.Id);

        // CREATE TABLE owners (id INT PRIMARY KEY,
        //                      Name VARCHAR(100) NOT NULL)
        builder.Property(o => o.Name)
            .IsRequired()
            .HasMaxLength(100);
        
        // relationships
        // owner has many cats
        builder.HasMany(o => o.Cats)
            // every cat belongs to one owner
            .WithOne(c => c.Owner)
            // linked by ownerid column
            .HasForeignKey(c => c.OwnerId);
        
        // *** side note *** 
        // EntityFrameworkCore is setup so that if you use the same naming conventions across, it should pickup the link
        // between two tables. 

    }
}