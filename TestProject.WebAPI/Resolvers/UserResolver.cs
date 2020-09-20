using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestProject.WebAPI.Models;

namespace TestProject.WebAPI.Resolvers
{
    public class UserEmailResolver : IValueResolver<Account, AccountModel, string>
    {
        public string Resolve(Account source, AccountModel destination, string destMember, ResolutionContext context)
        {
            return context.Mapper.Map<string>(source.User?.Email);
        }
    }
}
