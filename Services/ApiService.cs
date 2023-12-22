using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text.Json;
using FrontCursos.Models;
using System.Security.Cryptography.X509Certificates;
using System.Runtime.CompilerServices;
using System.Net.Http.Json;

namespace FrontCursos.Services;

public class ApiService : IApiService
{
    private readonly HttpClient _httpClient;

    public ApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    //Optener objeto especifico de un unico endpoint
    public async Task<T> GetAsync<T>(string endpoint)
    {
        var response = await _httpClient.GetAsync(endpoint);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsAsync<T>();
    }

    //Optener lista de objeto un endpoint
    public async Task<IEnumerable<T>> GetAllAsync<T>(string endpoint)
    {
        var response = await _httpClient.GetAsync(endpoint);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsAsync<IEnumerable<T>>();
    }

    //Buscar una lista de objetos
    public async Task<IEnumerable<T>> FindAsync<T>(string endpoint, Expression<Func<T, bool>> expression)
    {
        var query = expression.Body.ToString();
        var encodedQuery = Uri.EscapeDataString(query);

        var requestUri = $"{endpoint}?query={encodedQuery}";

        var response = await _httpClient.GetAsync(requestUri);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsAsync<IEnumerable<T>>();
    }

    //Peticion post
    public async Task<T> PostAsync<T>(string endpoint, object data)
    {
        var response = await _httpClient.PostAsJsonAsync(endpoint, data);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsAsync<T>();
    }

    //Peticion para un update
    public async Task<T> PutAsync<T>(string endpoint, object data)
    {
        var response = await _httpClient.PutAsJsonAsync(endpoint, data);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsAsync<T>();
    }

    //Peticion para delete
    public async Task<bool> DeleteAsync(string endpoint)
    {
        var response = await _httpClient.DeleteAsync(endpoint);
        return response.IsSuccessStatusCode;
    }
}
