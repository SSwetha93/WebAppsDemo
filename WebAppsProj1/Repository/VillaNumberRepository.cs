using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WebAppsProj1.Data;
using WebAppsProj1.Models;
using WebAppsProj1.Repository.IRepository;

namespace WebAppsProj1.Repository
{
    public class VillaNumberRepository : Repository<VillaNumber>, IVillaNumberRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public VillaNumberRepository(ApplicationDbContext dbContext) :base (dbContext) 
        {
            this._dbContext = dbContext;
        }
        
        public async Task<VillaNumber> UpdateAsync(VillaNumber entity)
        {
            entity.UpdatedDate = DateTime.Now;
            _dbContext.VillaNumbers.Update(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }
        
    }
}
