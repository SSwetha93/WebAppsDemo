using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WebAppsProj1.Data;
using WebAppsProj1.Models;
using WebAppsProj1.Repository.IRepository;

namespace WebAppsProj1.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _dbContext;
        internal DbSet<T> dbset;
        public Repository(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
            //_dbContext.VillaNumbers.Include( x => x.Villa).ToList();
            this.dbset = _dbContext.Set<T>();
        }
        public async Task CreateAsync(T entity)
        {
            await dbset.AddAsync(entity);
            await SaveAsync();
        }

        //Added Optional Parameter for VillaNumber entity
        public async Task<T> GetAsync(Expression<Func<T, bool>>? filter = null, bool tracked = true, string? includeProperties = null)
        {
            IQueryable<T> query = dbset;
            if (!tracked)
            {
                query = query.AsNoTracking();
            }
            if (filter != null)
            {
                query = query.Where(filter);
            }
            
            if (includeProperties != null)
            { 
                //split is used when multiple entities needs to be used inside single entity, we need to split the entities by comma seperated
                foreach (var includeprop in includeProperties.Split(new char[] { ','}, StringSplitOptions.RemoveEmptyEntries)) 
                {
					query = query.Include(includeprop);
				}
            }
            return await query.FirstOrDefaultAsync();
        }

        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null, string? includeProperties = null, int pageSize = 5, int pageNumber = 1)
        {
            IQueryable<T> query = dbset;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            if (pageSize > 0)
            {
                if (pageSize > 100)
                {
                    pageSize = 100;
                }
                query = query.Skip(pageSize * (pageNumber - 1)).Take(pageSize);
            }
            if (includeProperties != null)
			{
				foreach (var includeprop in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
				{
					query = query.Include(includeprop);
				}
			}
			return await query.ToListAsync();
        }
        public async Task RemoveAsync(T entity)
        {
            dbset.Remove(entity);
            await SaveAsync();
        }

        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
