using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestProject.WebAPI.Models;
using TestProject.WebAPI.Resolvers;

namespace TestProject.WebAPI.Configuration
{
    public class AutoMapperProfile : Profile
    {

        public AutoMapperProfile(UserEmailResolver userEmailResolver)
        {
            Setup(userEmailResolver);
        }

        public AutoMapperProfile() //for unit tests
        {
            Setup(new UserEmailResolver());
        }

        public void Setup(UserEmailResolver userEmailResolver)
        {
            CreateMap<UserModel, User>()
                 .ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<User, UserModel>();

            CreateMap<AccountModel, Account>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<Account, AccountModel>()
                .ForMember(dest => dest.Email, opt => opt.MapFrom(userEmailResolver));
        }

    }
}
