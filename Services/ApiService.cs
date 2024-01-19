using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace FrontBlazor.Services;

public class ApiService : IApiService
{
    private readonly HttpClient _httpClient;
    private readonly HttpContextAccessor _httpContextAccessor;

    public ApiService()
    {
        _httpContextAccessor = new HttpContextAccessor();
    }

    //Optener objeto especifico de un unico endpoint
    public async Task<T> GetAsync<T>(string endpoint)
    {
        try
        {
            var response = await _httpClient.GetAsync(endpoint);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<T>();
        }
        catch (System.Exception)
        {
            throw;
        }
    }

    //Optener lista de objeto un endpoint
    public async Task<IEnumerable<T>> GetAllAsync<T>(string endpoint)
    {
        try
        {
            var response = await _httpClient.GetAsync(endpoint);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<IEnumerable<T>>();
        }
        catch (System.Exception)
        {
            throw;
        }
    }

    //En desarrollo
    //Buscar una lista de objetos
    /* public async Task<IEnumerable<T>> FindAsync<T>(
        string endpoint,
        Expression<Func<T, bool>> expression
    )
    {
        try
        {
            var query = expression.Body.ToString();
            var encodedQuery = Uri.EscapeDataString(query);

            var requestUri = $"{endpoint}?query={encodedQuery}";

            var response = await _httpClient.GetAsync(requestUri);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<IEnumerable<T>>();
        }
        catch (System.Exception)
        {
            throw;
        }
    } */

    //Peticion post
    public async Task<T> PostAsync<T>(string endpoint, object data)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync(endpoint, data);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<T>();
        }
        catch (System.Exception)
        {
            throw;
        }
    }

    //Peticion para un update
    public async Task<T> PutAsync<T>(string endpoint, object data)
    {
        try
        {
            var response = await _httpClient.PutAsJsonAsync(endpoint, data);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<T>();
        }
        catch (System.Exception)
        {
            throw;
        }
    }

    //Peticion para delete
    public async Task<bool> DeleteAsync(string endpoint)
    {
        try
        {
            var response = await _httpClient.DeleteAsync(endpoint);
            return response.IsSuccessStatusCode;
        }
        catch (System.Exception)
        {
            throw;
        }
    }
}
