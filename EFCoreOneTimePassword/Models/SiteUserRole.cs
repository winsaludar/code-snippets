#nullable disable

using Microsoft.AspNetCore.Identity;

namespace EFCoreOneTimePassword.Models;

public class SiteUserRole : IdentityUserRole<string>
{
    public virtual SiteUser User { get; set; }
    public virtual SiteRole Role { get; set; }
}