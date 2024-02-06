using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WebAppsProj1.Data;
using WebAppsProj1.Models;
using WebAppsProj1.Repository.IRepository;

namespace WebAppsProj1.Repository
{
    public class VillaRepository : Repository<Villa>, IVillaRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public VillaRepository(ApplicationDbContext dbContext) :base (dbContext) 
        {
            this._dbContext = dbContext;
        }
        
        public async Task<Villa> UpdateAsync(Villa entity)
        {
            entity.UpdatedDate = DateTime.Now;
            _dbContext.Villas.Update(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }
        
    }
}
