using EFCoreOneTimePassword.Models;
using Microsoft.AspNetCore.Identity;

namespace EFCoreOneTimePassword.Services;

public class UserService : IUserService
{
    private readonly UserManager<SiteUser> _userManager;

    // Injected through dependency injection
    public UserService(UserManager<SiteUser> userManager) => _userManager = userManager;

    public async Task<(bool isSuccessful, string message, string? otp)> RequestOtp(string username)
    {
        // STEP 1: Check user if exist
        SiteUser? user;
        try
        {
            user = await _userManager.FindByNameAsync(username);
            if (user == null)
                throw new Exception($"No user with username of {username} in the database.");
        }
        catch (Exception)
        {
            return (false, "Error! User does not exist.", null);
        }

        // STEP 2: Generate OTP
        string otp;
        try
        {
            // This will generate a random number with 5 characters in length.
            Random r = new();
            int randNum = r.Next(99999);
            otp = randNum.ToString("D5");

            await _userManager.RemovePasswordAsync(user);
            await _userManager.AddPasswordAsync(user, otp);
            await _userManager.ResetAccessFailedCountAsync(user);

            // Add 5 minutes expiration on our OTP
            // The checking will be handle in our login method
            user.PasswordExpiration = DateTime.UtcNow.AddMinutes(5);
            user.LockoutEnd = null;

            await _userManager.UpdateAsync(user);
        }
        catch (Exception)
        {
            return (false, "Error! Unable to generate OTP.", null);
        }

        return (true, "Success", otp);
    }
}
