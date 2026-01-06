using CatAdoption.Domain.Interfaces;
using CatAdoption.Domain.Models;

namespace CatAdoption.Application.Services;

public class AdoptionService
{
    private readonly IRepository<Owner> _ownerRepo;
    private readonly IRepository<Cat> _catRepo;

    public AdoptionService(IRepository<Owner> ownerRepo, IRepository<Cat> catRepo)
    {
        _ownerRepo = ownerRepo;
        _catRepo = catRepo;
    }

    public async Task CreateOwnerAsync(string name, int age)
    {
        var owner = new Owner
        {
            Name = name,
            Age = age
        };
        
        // insert staged, known as tracked or tracking
        await _ownerRepo.AddAsync(owner);

        // commit all tracked changes/
        // you can track as many changes as you want, just keep in mind that 
        // it will slow down your application if you start to track too many
        await _ownerRepo.SaveChangesAsync();
        
        Console.WriteLine($"Owner {name} created with the id of {owner.Id}");
    }

    public async Task AddingCatAsync(string name, DateTime adoptDate, int ownerId)
    {
        var owner = await _ownerRepo.GetByIdAsync(ownerId);

        if (owner == null)
        {
            Console.WriteLine("Error: Owner Id not found");
            return;
        }

        var cat = new Cat
        {
            Name = name,
            AdoptionDate = adoptDate,
            // linking the relationship between owner and cats
            // ef core handles the linking between the two using the config and navigation properties
            OwnerId = ownerId
        };

        await _catRepo.AddAsync(cat);
        await _catRepo.SaveChangesAsync();
        Console.WriteLine($"Cat {name} successfully adopted by {owner.Name}");
    }

    public async Task ListAllCatAsync()
    {
        var cats = await _catRepo.GetAllAsync();
        
        Console.WriteLine("\t\t\t\t Data on all cats ");
        foreach (var c in cats)
        {
            Console.WriteLine($"Cat: {c.Name} - Adopted Date: {c.AdoptionDate.ToShortDateString()} - Owner Id: {c.OwnerId}");
        }
    }

    public async Task ListAllOwnerAsync()
    {
        var owners = await _ownerRepo.GetAllAsync();
        
        Console.WriteLine("\t\t\t\t Data on all owners");
        foreach (var o in owners)
        {
            Console.WriteLine($"Owner Name: {o.Name} - Age: {o.Age}");
        }
    }
}
