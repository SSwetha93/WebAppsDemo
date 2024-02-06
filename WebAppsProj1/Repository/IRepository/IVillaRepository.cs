using System.Linq.Expressions;
using WebAppsProj1.Models;

namespace WebAppsProj1.Repository.IRepository
{
    public interface IVillaRepository : IRepository<Villa>
    {

        Task<Villa> UpdateAsync(Villa entity);
    }
}
