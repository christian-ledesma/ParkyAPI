using ParkyWeb.Models;
using System.Threading.Tasks;

namespace ParkyWeb.Repository.Interfaces
{
    public interface IAccountRepository : IRepository<User>
    {
        Task<User> Login(string url, User user);
        Task<bool> Register(string url, User user);
    }
}
