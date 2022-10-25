using AutoFixture;
using FluentAssertions;
using FluentAssertions.Execution;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using Sat.Recruitment.Application.Exceptions;
using Sat.Recruitment.Application.UseCases;
using Sat.Recruitment.Domain.DTO;
using Sat.Recruitment.Domain.Entities;
using Sat.Recruitment.Domain.Interfaces;
using Sat.Recruitment.Infrastructure.Repositories.Base;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Sat.Recruitment.Test
{
    [TestFixture]
    public class UserBusinessLogicTest
    {


        private Fixture _fixture;
        private Mock<IUserRepository> _userRepositoryMock;
        private Mock<ILogger<UserBusinessLogic>> _loggerMock;

        private UserBusinessLogic _userbl;


        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture();
            _userRepositoryMock = new Mock<IUserRepository>();
            _loggerMock = new Mock<ILogger<UserBusinessLogic>>();   
            _userbl = new UserBusinessLogic(_userRepositoryMock.Object, _loggerMock.Object);
        }


        [Test]
        public void Create_NonRepeated_User()
        {

            var newUser = _fixture.Create<UserDto>();

            _userbl.CreateUser(newUser).Result.Should().Match<Response>((e) => e.IsSuccess == true && e.Errors == "User Created");

        }

        [Test]
        public void Create_Repeated_User()
        {
            var newUser = _fixture.Create<User>();
            _userRepositoryMock.Setup(x => x.GetUserAsync(It.IsAny<User>())).ReturnsAsync(newUser);

            var newUserDto = _fixture.Create<UserDto>();

            _userbl.CreateUser(newUserDto).Result.Should().Match<Response>((e) =>  e.IsSuccess == false && e.Errors == "The user is duplicated" );

        }
        [Test]
        public void Create_CustomExceptionTest()
        {
            var newUser = _fixture.Create<User>();
            _userRepositoryMock.Setup(x => x.GetUserAsync(It.IsAny<User>())).Throws(new IOException());

            var newUserDto = _fixture.Create<UserDto>();
            Func<Task> func = async () => await _userbl.CreateUser(newUserDto);
            func.Should().ThrowAsync<DbAccessException>();

        }
    }
}
