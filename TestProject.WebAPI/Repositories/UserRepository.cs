using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestProject.WebAPI.Models;

namespace TestProject.WebAPI.Services
{

    public class UserRepository : IUserRepository
    {
        private readonly IMapper mapper;

        public UserRepository(IMapper mapper, DataContext dataContext)
        {
            this.mapper = mapper;
            DataContext = dataContext;
        }

        protected DataContext DataContext { get; }

        public async Task<string> Create(User userDal)
        {
            await DataContext.SaveChangesAsync();
            return userDal.Email;
        }

        public async Task<User> Get(string email)
        {
            return await DataContext.Users.FirstOrDefaultAsync<User>(predicate: u => u.Email == email);
        }

        public async Task<IEnumerable<User>> List()
        {
            return await DataContext.Users.ToListAsync();
        }

        public async Task CreateAccount(Account accountDal)
        {
            DataContext.Accounts.Add(accountDal);
            await DataContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Account>> ListAccounts()
        {
            return await DataContext.Accounts.Include(a => a.User).ToListAsync();
        }
    }
}
