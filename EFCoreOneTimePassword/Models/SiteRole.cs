#nullable disable

using Microsoft.AspNetCore.Identity;

namespace EFCoreOneTimePassword.Models;

public class SiteRole : IdentityRole
{
    // Add custom site role properties here...

    public virtual ICollection<SiteUserRole> UserRoles { get; set; }
    public virtual ICollection<SiteRoleClaim> RoleClaims { get; set; }
}
