#nullable disable

using Microsoft.AspNetCore.Identity;

namespace EFCoreOneTimePassword.Models;

public class SiteUserToken : IdentityUserToken<string>
{
    public virtual SiteUser User { get; set; }
}