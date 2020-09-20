using System.Collections.Generic;
using System.Threading.Tasks;
using TestProject.WebAPI.Models;

namespace TestProject.WebAPI.Services
{
    public interface IUserRepository
    {
        Task<string> Create(User userDal);

        Task<User> Get(string email);

        Task<IEnumerable<User>> List();

        Task CreateAccount(Account accountDal);

        Task<IEnumerable<Account>> ListAccounts();
    }
}
