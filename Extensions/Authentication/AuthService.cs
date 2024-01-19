using System.Security.Claims;
using System.Threading.Tasks;
using Extensions.CookieManagement;
using FrontBlazor.Models;
using FrontBlazor.Services;
using Microsoft.AspNetCore.Components;


namespace Extensions.Authentication;
public class AuthService
{
    private readonly CookieService _cookieService;
    private readonly ApiService _apiService;

    public AuthService(
        CookieService cookieService,
        ApiService apiService)
    {
        _cookieService = cookieService;
        _apiService = apiService;
    }

    public async Task<bool> LoginAsync(LogInModel loginModel)
    {
        // Realiza la lógica de autenticación utilizando ApiService para verificar las credenciales
        var result = await _apiService.PostAsync<TokenModel>("url_para_login", loginModel);

        if (result != null)
        {
            // Guarda el TokenModel en la cookie si la autenticación es exitosa
            await _cookieService.AddCookie("nombre_cookie_token", result);
            await NotifyAuthenticationStateChanged(result);
            return true;
        }

        return false;
    }

    public async Task LogoutAsync()
    {
        // Realiza la lógica de cierre de sesión
        await _cookieService.RemoveCookie("nombre_cookie_token");
        await NotifyAuthenticationStateChanged(null);
    }

    private async Task NotifyAuthenticationStateChanged(TokenModel tokenModel)
    {
        var user = tokenModel != null
            ? new ClaimsPrincipal(new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, tokenModel.UserName) }, "jwt"))
            : new ClaimsPrincipal(new ClaimsIdentity());

        var authenticationState = new AuthenticationState(user);
        await _authenticationStateProvider.NotifyAuthenticationStateChanged(Task.FromResult(authenticationState));
    }
}
