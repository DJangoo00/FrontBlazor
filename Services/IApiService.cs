using System.Linq.Expressions;

namespace FrontBlazor.Services;
public interface IApiService
{
    //Optener objeto especifico de un unico endpoint
    Task<T> GetAsync<T>(string endpoint);
    //Optener lista de objeto un endpoint
    Task<IEnumerable<T>> GetAllAsync<T>(string endpoint);
    //Buscar una lista de objetos, esta en desarrollo
    //Task<IEnumerable<T>> FindAsync<T>(string endpoint, Expression<Func<T, bool>> expression);
    //Peticion post
    Task<T> PostAsync<T>(string endpoint, object data);
    //Peticion para un update
    Task<T> PutAsync<T>(string endpoint, object data);
    //Peticion para delete
    Task<bool> DeleteAsync(string endpoint);
}
