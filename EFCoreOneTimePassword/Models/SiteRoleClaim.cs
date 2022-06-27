#nullable disable

using Microsoft.AspNetCore.Identity;

namespace EFCoreOneTimePassword.Models;

public class SiteRoleClaim : IdentityRoleClaim<string>
{
    public virtual SiteRole Role { get; set; }
}