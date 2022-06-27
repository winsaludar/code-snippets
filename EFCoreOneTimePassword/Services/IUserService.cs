namespace EFCoreOneTimePassword.Services;

public interface IUserService
{
    public Task<(bool isSuccessful, string message, string? otp)> RequestOtp(string username);
}