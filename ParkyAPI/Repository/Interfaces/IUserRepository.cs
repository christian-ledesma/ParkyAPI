using ParkyAPI.Models;
using System.Threading.Tasks;

namespace ParkyAPI.Repository.Interfaces
{
    public interface IUserRepository
    {
        Task<bool> UserIsUnique(string username);
        Task<User> Authenticate(string username, string password);
        Task<User> Register (string username, string password);
    }
}
