using GrindCore.Domain.Interfaces;

namespace GrindCore.Infrastructure.Services;

public class MockAuthService : IAuthService
{
    private const string ValidUsername = "admin";
    private const string ValidPassword = "admin123";

    public bool ValidateCredentials(string username, string password) =>
        username == ValidUsername && password == ValidPassword;
}
