using EFCoreTransactions.Models;
using EFCoreTransactions.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreTransactions.Services;

public class ProductService : IProductService
{
    private readonly IDbContext _dbContext;

    // Inserted through dependency injection
    public ProductService(IDbContext dbContext) => _dbContext = dbContext;

    public async Task<IEnumerable<Product>> GetAll() => await _dbContext.Products.AsNoTracking().ToListAsync();

    public async Task<Product?> Get(int id) => await _dbContext.Products.FirstOrDefaultAsync(x => x.Id == id);

    public async Task<(bool isSuccessful, string message)> Add(Product product)
    {
        try
        {
            // Begin our db transaction
            await _dbContext.BeginTransactionAsync();

            // Do something here before adding the product...

            // Add the product (no db changes yet)
            _dbContext.Products.Add(product);

            // Do something here after adding the product...

            // If everything is ok, commit our changes to the database
            await _dbContext.CommitAsync();

            return (true, "Success");
        }
        catch (Exception)
        {
            // Rollback our changes if something throws an exception
            await _dbContext.RollbackAsync();

            return (false, "Error!");
        }
    }
}
