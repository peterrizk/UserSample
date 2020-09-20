using System.Collections.Generic;
using System.Threading.Tasks;
using TestProject.WebAPI.Models;

namespace TestProject.WebAPI.Services
{
    public interface IUserService
    {
        Task<string> Create(UserModel user);

        Task<UserModel> Get(string email);

        Task<IEnumerable<UserModel>> List();

        Task CreateAccount(AccountModel account);

        Task<IEnumerable<AccountModel>> ListAccounts();

    }
}
