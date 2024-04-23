using GestaoPortfolio.Domain.Models;

namespace GestaoPortfolio.Domain.Interfaces.Repository
{
    public interface IJobRepository : IBaseRepository<Job>
    {
        Task<Job> GetSiglaAsync(string sigla);
    }
}
