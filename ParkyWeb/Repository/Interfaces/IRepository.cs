using System.Collections.Generic;
using System.Threading.Tasks;

namespace ParkyWeb.Repository.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<T> GetById(string url, int id);
        Task<IEnumerable<T>> GetAll(string url);
        Task<bool> Create(string url, T createDto);
        Task<bool> Update(string url, T updateDto);
        Task<bool> Delete(string url, int id);
    }
}
