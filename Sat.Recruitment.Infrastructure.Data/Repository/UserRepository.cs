using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sat.Recruitment.Domain.Entities;
using Sat.Recruitment.Domain.Interfaces;
using Sat.Recruitment.Infrastructure.Data.Context;
using Sat.Recruitment.Infrastructure.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sat.Recruitment.Infrastructure.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly ILogger<UserRepository> _logger;
        public UserRepository(SatRecruitmentContext satRecruitmentContext,ILogger<UserRepository> logger) : base(satRecruitmentContext)
        {
            _logger = logger;
        }

        public async Task<User> GetUserAsync(User user)
        {
            try
            {
                var userResponse = await _satRecruitmentContext.User.SingleOrDefaultAsync(e => e.Email == user.Email || e.Phone == user.Phone || e.Address == user.Address && e.Name == user.Name);

                return userResponse;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetUserAsync error");
                throw ;
            }
            
        }

    }
}
