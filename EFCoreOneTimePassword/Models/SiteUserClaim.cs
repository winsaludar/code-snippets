#nullable disable

using Microsoft.AspNetCore.Identity;

namespace EFCoreOneTimePassword.Models;

public class SiteUserClaim : IdentityUserClaim<string>
{
    public virtual SiteUser User { get; set; }
}