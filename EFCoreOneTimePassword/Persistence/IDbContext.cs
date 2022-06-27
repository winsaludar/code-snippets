using EFCoreOneTimePassword.Models;
using Microsoft.EntityFrameworkCore;

namespace EFCoreOneTimePassword.Persistence;

public interface IDbContext
{
    public DbSet<SiteUser> Users { get; set; }
    public DbSet<SiteRole> Roles { get; set; }
    public DbSet<SiteUserRole> UserRoles { get; set; }
    public DbSet<SiteRoleClaim> RoleClaims { get; set; }

    public Task BeginTransactionAsync();
    public Task CommitAsync();
    public Task RollbackAsync();
}