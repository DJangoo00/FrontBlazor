/* Servicios de gestion de cookies */
using System;
using System.Threading.Tasks;
using Microsoft.JSInterop;

namespace FrontBlazor.Services;

public class CookieService : ICookieService
{
    private readonly IJSRuntime _jsRuntime; //Uso de Runtime JavaScript para el manejo de las cookies
    private string cookieName = "user"; // Aquí se define el nombre de la cookie

    public CookieService()
    {
        _jsRuntime = new IJSRuntime();
    }

    public async Task AddOrUpdateTokenCookie(
        TokenModel token,
        CookieOptions cookieOptions
    )
    {
        string tokenJson = System.Text.Json.JsonSerializer.Serialize(token);

        var options = new
        {
            expires = cookieOptions.Expires?.ToUTCString(),
            httpOnly = cookieOptions.HttpOnly,
            secure = cookieOptions.Secure,
            sameSite = cookieOptions.SameSite.ToString()
        };

        await _jsRuntime.InvokeVoidAsync(
            "jsFunctions.setCookieWithOptions",
            cookieName,
            tokenJson,
            options
        );
    }

    public async Task<TokenModel> GetTokenFromCookie()
    {
        var tokenJson = await _jsRuntime.InvokeAsync<string>("jsFunctions.getCookie", cookieName);

        if (!string.IsNullOrEmpty(tokenJson))
        {
            return System.Text.Json.JsonSerializer.Deserialize<TokenModel>(tokenJson);
        }

        return null;
    }

    public async Task RemoveTokenCookie()
    {
        await _jsRuntime.InvokeVoidAsync("jsFunctions.deleteCookie", cookieName);
    }

    public async Task UpdateTokenCookie(
        string cookieName,
        TokenModel newToken,
        CookieOptions cookieOptions
    )
    {
        await RemoveTokenCookie(cookieName);
        await AddOrUpdateTokenCookie(cookieName, newToken, cookieOptions);
    }
}
