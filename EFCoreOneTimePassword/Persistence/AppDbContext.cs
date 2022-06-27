using EFCoreOneTimePassword.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace EFCoreOneTimePassword.Persistence;

public class AppDbContext : IdentityDbContext<SiteUser, SiteRole, string, SiteUserClaim, SiteUserRole, SiteUserLogin, SiteRoleClaim, SiteUserToken>,
                            IDbContext
{
    private IDbContextTransaction? _transaction = null;

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

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

        builder.Entity<SiteUser>(entity =>
        {
            entity.ToTable("SiteUser");

            // Each User can have many UserClaims
            entity.HasMany(e => e.Claims)
                .WithOne(e => e.User)
                .HasForeignKey(uc => uc.UserId)
                .IsRequired();

            // Each User can have many UserLogins
            entity.HasMany(e => e.Logins)
                .WithOne(e => e.User)
                .HasForeignKey(ul => ul.UserId)
                .IsRequired();

            // Each User can have many UserTokens
            entity.HasMany(e => e.Tokens)
                .WithOne(e => e.User)
                .HasForeignKey(ut => ut.UserId)
                .IsRequired();

            // Each User can have many entries in the UserRole join table
            entity.HasMany(e => e.UserRoles)
                .WithOne(e => e.User)
                .HasForeignKey(ur => ur.UserId)
                .IsRequired();
        });

        builder.Entity<SiteRole>(entity =>
        {
            entity.ToTable("SiteRole");

            // Each Role can have many entries in the UserRole join table
            entity.HasMany(e => e.UserRoles)
                .WithOne(e => e.Role)
                .HasForeignKey(ur => ur.RoleId)
                .IsRequired();

            // Each Role can have many associated RoleClaims
            entity.HasMany(e => e.RoleClaims)
                .WithOne(e => e.Role)
                .HasForeignKey(rc => rc.RoleId)
                .IsRequired();
        });

        builder.Entity<SiteUserRole>(entity => entity.ToTable("SiteUserRole"));
        builder.Entity<SiteUserClaim>(entity => entity.ToTable("SiteUserClaim"));
        builder.Entity<SiteUserLogin>(entity => entity.ToTable("SiteUserLogin"));
        builder.Entity<SiteRoleClaim>(entity => entity.ToTable("SiteRoleClaim"));
        builder.Entity<SiteUserToken>(entity => entity.ToTable("SiteUserToken"));
    }
}
