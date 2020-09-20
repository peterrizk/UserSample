using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestProject.WebAPI.Models;

namespace TestProject.WebAPI.Services
{

    public class UserService : IUserService
    {
        private readonly IMapper mapper;
        private readonly IUserRepository userRepository;

        public UserService(IMapper mapper, IUserRepository userRepository)
        {
            this.mapper = mapper;
            this.userRepository = userRepository;
        }

        public async Task<string> Create(UserModel user)
        {
            user.Email = user.Email.ToLower();
            var userDal = mapper.Map<User>(user);
            await userRepository.Create(userDal);
            return userDal.Email;
        }

        public async Task<UserModel> Get(string email)
        {
            var user = await userRepository.Get(email.ToLower());

            var model = mapper.Map<UserModel>(user);

            return model;
        }

        public async Task<IEnumerable<UserModel>> List()
        {
            var users = await userRepository.List();

            var model = mapper.Map<IList<UserModel>>(users);

            return model;
        }

        public async Task CreateAccount(AccountModel account)
        {
            account.Points = 10;
            var user = await userRepository.Get(account.Email.ToLower());
            if (user is null) throw new KeyNotFoundException("user not found");
            user.Email = user.Email.ToLower();
            var accountDal = mapper.Map<Account>(account);
            accountDal.UserId = user.Id;
            await userRepository.CreateAccount(accountDal);
        }

        public async Task<IEnumerable<AccountModel>> ListAccounts()
        {
            var accounts = await userRepository.ListAccounts();

            var model = mapper.Map<IList<AccountModel>>(accounts);

            return model;
        }
    }
}
