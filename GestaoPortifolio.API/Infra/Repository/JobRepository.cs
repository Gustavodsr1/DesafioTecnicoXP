using GestaoPortfolio.Domain.Interfaces.Repository;
using GestaoPortfolio.Domain.Models;
using GestaoPortfolio.Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace GestaoPortfolio.Infra.Repository
{
    public class JobRepository : BaseRepository<Job>, IJobRepository
    {
        public JobRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<Job> GetSiglaAsync(string sigla)
        {
            var linq = from e in _entities
                       where e.Sigla == sigla
                       select e;

            return await linq.FirstOrDefaultAsync();
        }
    }
}
