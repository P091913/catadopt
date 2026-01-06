using CatAdoption.Domain.Interfaces;
using CatAdoption.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CatAdoption.Infrastructure.Repositories;

// generic repository - allow us to use any model we've created and 
// we'll access to addasync, getasync, getbyidasync, and savechangesasync
public class Repository<T> : IRepository<T> where T : class
{
    private readonly AdoptionDbContext _context;

    private readonly DbSet<T> _dbSet;

    public Repository(AdoptionDbContext context)
    {
        _context = context;

        _dbSet = _context.Set<T>();
    }

    public async Task AddAsync(T entity)
    {
        // * represents our existing model value
        // insert into cats (id, name, adoptiondate, ownerid) values (*id, *name, *adoptiondate, *ownerid)
        await _dbSet.AddAsync(entity);
    }

    public async Task<List<T>> GetAllAsync()
    {
        // select * from cats
        return await _dbSet.ToListAsync();
    }

    public async Task<T?> GetByIdAsync(int id)
    {
        // select * from cats where id = *id
        return await _dbSet.FindAsync(id);
    }

    public async Task SaveChangesAsync()
    {
        // commit; 
        await _context.SaveChangesAsync();
    }

}