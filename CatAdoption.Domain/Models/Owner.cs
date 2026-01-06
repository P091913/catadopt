namespace CatAdoption.Domain.Models;

public class Owner
{
    // primary key
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;
    
    public int Age { get; set; }

    // navigation property
    // the relationship between owner and cats: one owner can have multiple cats
    // this connects the gap between owner and cat, allowing us immediate access to all cats adopted by this person
    // if the owner doesn't tie to them, will just be an empty list of Cats
    public ICollection<Cat> Cats { get; set; } = new List<Cat>();
}

//var owner = await _context.Owners
//    .Include(o => o.Cats)
///    .SingleOrDefaultAsync(o => o.Id == id);
///  .Where(o => o.Id == id)
///  .ToListAsync();
/// *********
///
///


// var query  = from o in _context.Owners
//             join c in _context.Cats on o.Id equals c.OwnerId into cats
//             select new { Owner = o, Cats = cats.ToList() };