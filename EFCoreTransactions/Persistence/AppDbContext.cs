using EFCoreTransactions.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace EFCoreTransactions.Persistence;

public class AppDbContext : IdentityDbContext, IDbContext
{
    private IDbContextTransaction? _transaction = null;

    public AppDbContext(DbContextOptions<AppDbContext> options) 
        : base(options)
    {
    }

    public DbSet<Product> Products { get; set; } = null!;

    public async Task BeginTransactionAsync()
    {
        _transaction = await Database.BeginTransactionAsync();
    }

    public async Task CommitAsync()
    {
        if (_transaction == null)
            throw new ArgumentException("IDbContextTransaction is null");

        await SaveChangesAsync();
        await _transaction.CommitAsync();
        await _transaction.DisposeAsync();
    }

    public async Task RollbackAsync()
    {
        if (_transaction == null)
            throw new ArgumentException("IDbContextTransaction is null");

        await _transaction.RollbackAsync();
        await _transaction.DisposeAsync();
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Product>(entity => 
        {
            // Product table related settings...
        });
    }
}
