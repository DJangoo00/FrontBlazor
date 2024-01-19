namespace FrontBlazor.Services;
public class AuthService : IAuthService
{
    private bool _isAuthenticated = false;

    public bool IsAuthenticated => _isAuthenticated;

    public event Action<bool> AuthenticationChanged;

    public void SetAuthentication(bool isAuthenticated)
    {
        _isAuthenticated = isAuthenticated;
        AuthenticationChanged?.Invoke(_isAuthenticated);
        Console.WriteLine($"auth: {isAuthenticated}");
    }
}