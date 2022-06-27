#nullable disable

using Microsoft.AspNetCore.Identity;

namespace EFCoreOneTimePassword.Models;

public class SiteUser : IdentityUser
{
    public DateTime PasswordExpiration { get; set; }

    // Add custom user properties here...

    public virtual ICollection<SiteUserClaim> Claims { get; set; }
    public virtual ICollection<SiteUserLogin> Logins { get; set; }
    public virtual ICollection<SiteUserToken> Tokens { get; set; }
    public virtual ICollection<SiteUserRole> UserRoles { get; set; }
}