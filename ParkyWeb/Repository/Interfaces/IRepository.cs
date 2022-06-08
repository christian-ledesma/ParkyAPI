using System.Collections.Generic;
using System.Threading.Tasks;

namespace ParkyWeb.Repository.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<T> GetById(string url, int id, string token);
        Task<IEnumerable<T>> GetAll(string url, string token);
        Task<bool> Create(string url, T createDto, string token);
        Task<bool> Update(string url, T updateDto, string token);
        Task<bool> Delete(string url, int id, string token);
    }
}
