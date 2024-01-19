using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Extensions.CookieManagement;
public interface ICookieService
{
    Task AddCookie(string cookieName, object cookieValue);
    Task<object> GetCookie(string cookieName);
    Task RemoveCookie(string cookieName);
    Task UpdateCookie(string cookieName, object newValue);
}
