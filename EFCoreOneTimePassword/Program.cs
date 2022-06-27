// Sample code snippet for Program.cs

/*
using EFCoreOneTimePassword.Models;
using EFCoreOneTimePassword.Persistence;
*/

/*
void AddDatabase(WebApplicationBuilder builder)
{
    string connectionString = builder.Configuration.GetConnectionString("AppDbContext");
    builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));
    builder.Services.AddDatabaseDeveloperPageExceptionFilter();
    builder.Services.AddScoped<IDbContext, AppDbContext>();
}
*/

/*
void AddAuthentication(WebApplicationBuilder builder)
{
    // Configure EF User Identity
    builder.Services.AddIdentity<SiteUser, SiteRole>(options =>
    {
        options.User.RequireUniqueEmail = true;
        options.Password.RequireLowercase = false;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireUppercase = false;
        options.Password.RequiredLength = 5;
        options.Password.RequiredUniqueChars = 0;
        options.Lockout.MaxFailedAccessAttempts = 3;
    }).AddEntityFrameworkStores<AppDbContext>();

    // Add other authentication codes here...
}
*/