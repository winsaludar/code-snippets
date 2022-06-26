using EFCoreTransactions.Models;
using Microsoft.EntityFrameworkCore;

namespace EFCoreTransactions.Persistence;

public interface IDbContext
{
    public DbSet<Product> Products { get; set; }

    public Task BeginTransactionAsync();
    public Task CommitAsync();
    public Task RollbackAsync();
}
