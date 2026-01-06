namespace CatAdoption.Domain.Models;

public class Cat
{
    // primary key
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;
    
    public DateTime AdoptionDate { get; set; }
    
    // storing the actual id of the owner
    public int OwnerId { get; set; }
    
    // navigation property
    // we decide to grab a cat but forget to .Include() owner, this will be null
    // instance of cat : myCat.Owner.Name
    public Owner? Owner { get; set; }
}