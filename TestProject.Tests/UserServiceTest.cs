using AutoMapper;
using NSubstitute;
using System;
using System.Threading.Tasks;
using TestProject.WebAPI.Configuration;
using TestProject.WebAPI.Models;
using TestProject.WebAPI.Resolvers;
using TestProject.WebAPI.Services;
using Xunit;

namespace TestProject.Tests
{
    public class UserServiceTest
    {
        [Fact]
        public async Task Test_EmailsAreMadeLowercase()
        {
            // arrange
            var repo = Substitute.For<IUserRepository>();
            var config = new MapperConfiguration(cfg => cfg.AddProfile<AutoMapperProfile>());
            var mapper = config.CreateMapper();

            var target = new UserService(mapper, repo);

            //act
            var email = await target.Create(new UserModel() { Email="PETER@RIZK.CO",Expenses=11, Salary=70000 });

            //assert
            Assert.Equal("peter@rizk.co", email); 
        }
    }
}