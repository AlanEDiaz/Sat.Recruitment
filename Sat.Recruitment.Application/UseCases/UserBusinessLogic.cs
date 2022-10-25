using Microsoft.Extensions.Logging;
using Sat.Recruitment.Application.Exceptions;
using Sat.Recruitment.Application.Mapper;
using Sat.Recruitment.Application.UseCases.Interfaces;
using Sat.Recruitment.Domain.DTO;
using Sat.Recruitment.Domain.Entities;
using Sat.Recruitment.Domain.Enums;
using Sat.Recruitment.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Sat.Recruitment.Application.UseCases
{
    public class UserBusinessLogic : IUserBusinessLogic
    {
        private static IUserRepository _userRepository;
        private readonly ILogger<UserBusinessLogic> _logger;

        public UserBusinessLogic(IUserRepository userRepository,ILogger<UserBusinessLogic>logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }

        public async Task<Response> CreateUser(UserDto userDto)
        {
            User newUser = UserMapper.Mapper.Map<User>(userDto);
            Response response = new Response();


            newUser.Money += newUser.Money * GetMultiplier(newUser.Money, newUser.UserType);
            try
            {
                if (_userRepository.GetUserAsync(newUser).Result != null)
                {
                    response.Errors = "The user is duplicated";
                    response.IsSuccess = false;
                    return response;

                }

                await _userRepository.AddAsync(newUser);
                response.IsSuccess = true;
                response.Errors = "User Created";
                return response;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "CreateUser Error");
                throw new DbAccessException("Error on query db");
            }
        }

        private static decimal GetMultiplier(decimal money, UserType userType)
        {
            decimal multiplier = 0;

            if (money < 100 && money > 10 && userType == UserType.Normal)
            {
                multiplier = Convert.ToDecimal(0.8);

            }
            if (money > 100)
            {
                switch (userType)
                {
                    case UserType.Normal:
                        multiplier = Convert.ToDecimal(0.12);
                        break;
                    case UserType.SuperUser:
                        multiplier = Convert.ToDecimal(0.20);

                        break;
                    case UserType.Premium:
                        multiplier = 2;

                        break;
                    default:
                        break;
                }

            }
            if (money == 100)
            {
                multiplier = 1;
            }
            return multiplier;
        }
    }
}
