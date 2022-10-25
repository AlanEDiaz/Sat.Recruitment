using Sat.Recruitment.Domain.DTO;
using Sat.Recruitment.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sat.Recruitment.Application.UseCases.Interfaces
{
    public interface IUserBusinessLogic
    {
        Task<Response> CreateUser(UserDto userDto);
    }
}
