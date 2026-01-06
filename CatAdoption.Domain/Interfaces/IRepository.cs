namespace CatAdoption.Domain.Interfaces;


// Owner model will be referenced if we pass in an instance of that model when using IRepository
// Cat model will be referenced if we pass in an instance of that model when using IRepository
// compiler is being told that this interface is waiting for a reference type class to passed in
public interface IRepository<T> where T : class
{
    // Async tasks allows further input from the user to occur without blocking the page / UI from being accessible
    // this calls out to our implementation, and it will handle the actual insert
    // adding queue, marking as "New" that way when the previous (if there is one) is done, it will be picked up by the queue
    Task AddAsync(T entity);

    // retrieve a list of everything from the table
    Task<List<T>> GetAllAsync();
    
    // retrieve a single item based on the primary key
    Task<T?> GetByIdAsync(int id);
    
    // update an item based on the primary key
    // Task UpdateAsync(T entity);

    // the commit method
    Task SaveChangesAsync();
}