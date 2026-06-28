namespace GrindCore.Domain.Interfaces;

public interface IAuthService
{
    bool ValidateCredentials(string username, string password);
}
