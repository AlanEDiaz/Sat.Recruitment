using Sat.Recruitment.Domain.Interfaces;
using Sat.Recruitment.Infrastructure.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sat.Recruitment.Infrastructure.Repositories.Base
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, new()
    {
        protected readonly SatRecruitmentContext _satRecruitmentContext;

        public Repository(SatRecruitmentContext repositorySatRecruitmentContext)
        {
            _satRecruitmentContext = repositorySatRecruitmentContext;
        }


        public async Task<TEntity> AddAsync(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException($"{nameof(AddAsync)} entity must not be null");
            }

            try
            {
                await _satRecruitmentContext.AddAsync(entity);
                await _satRecruitmentContext.SaveChangesAsync();

                return entity;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
