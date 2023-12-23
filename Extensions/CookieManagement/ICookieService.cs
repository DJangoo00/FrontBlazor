using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrontBlazor.Extensions.CookieManagement
{
    public interface ICookieService
    {
        Task AddOrUpdateCookie(string cookieName, string cookieValue, int expirationDays = 365);
        Task<string> GetCookie(string cookieName);
        Task RemoveCookie(string cookieName);
        Task UpdateCookie(string cookieName, string newValue);
    }
}