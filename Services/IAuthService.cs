namespace FrontBlazor.Services;
public interface IAuthService
{
    bool IsAuthenticated { get; }
    event Action<bool> AuthenticationChanged;
    void SetAuthentication(bool isAuthenticated);
}
