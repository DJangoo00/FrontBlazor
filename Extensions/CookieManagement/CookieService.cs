/* Servicios de gestion de cookies */
using System;
using System.Threading.Tasks;
using FrontBlazor.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.JSInterop;

namespace Extensions.CookieManagement;
public class CookieService : ICookieService
{
    private readonly IJSRuntime _jsRuntime; //Uso de Runtime JavaScript para el manejo de las cookies
    private CookieOptions cookieOptions = new()
    {
        Expires = DateTime.Now.AddDays(1), // Puedes ajustar la expiración
        HttpOnly = false, // Marcar la cookie como accesible solo por HTTP
        Secure = false, // Si tu aplicación usa HTTPS, establece esto en true
        SameSite = SameSiteMode.None // Controla la restricción de envío de cookies en solicitudes cross-site
    };
    public CookieService()
    {
        //_jsRuntime = new IJSRuntime();
    }

    public async Task AddCookie(string cookieName, object cookieValue)
    {
        string tokenJson = System.Text.Json.JsonSerializer.Serialize(cookieValue);
        await _jsRuntime.InvokeVoidAsync(
            "jsFunctions.setCookieWithOptions",
            cookieName,
            tokenJson,
            cookieOptions
        );
    }

    public async Task<object> GetCookie(string cookieName)
    {
        var tokenJson = await _jsRuntime.InvokeAsync<string>("jsFunctions.getCookie", cookieName);
        if (!string.IsNullOrEmpty(tokenJson))
        {
            return System.Text.Json.JsonSerializer.Deserialize<object>(tokenJson);
        }
        return null;
    }

    public async Task RemoveCookie(string cookieName)
    {
        await _jsRuntime.InvokeVoidAsync("jsFunctions.deleteCookie", cookieName);
    }

    public async Task UpdateCookie(string cookieName, object cookieValue)
    {
        await RemoveCookie(cookieName);
        await AddCookie(cookieName, cookieValue);
    }
}
