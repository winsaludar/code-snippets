#nullable disable

using Microsoft.AspNetCore.Identity;

namespace EFCoreOneTimePassword.Models;

public class SiteUserLogin : IdentityUserLogin<string>
{
    public virtual SiteUser User { get; set; }
}