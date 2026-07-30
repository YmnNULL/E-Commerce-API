using E_Commerce.Domin.Commen;
using E_Commerce.Domin.Contracts;
using E_Commerce.Infrastructure.Data;
using E_Commerce.Infrastructure.Specificaions;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Infrastructure.Repository
{
    internal class GenericRepository<TEntity, Tkey>(StoreDbContext dbContext) 
        : IGenericRepository<TEntity, Tkey> where TEntity : BaseEntity<Tkey>
    {
        public void Add(TEntity entity) => dbContext.Set<TEntity>().Add(entity);

        public async Task<IReadOnlyList<TEntity>> GetAllAsync(CancellationToken ct = default)
            => await dbContext.Set<TEntity>().ToListAsync(ct);

        public async Task<IReadOnlyList<TEntity>> GetAllAsync(ISpecifications<TEntity, Tkey> spec, CancellationToken ct = default)
        {
            var query = SpecificationEvaluatuator.CreateQuery(dbContext.Set<TEntity>(), spec);
            return await query.ToListAsync(ct);
        }

        public async Task<TEntity?> GetByIdAsync(Tkey id, CancellationToken ct = default)
            => await dbContext.Set<TEntity>().FindAsync(id, ct);

        public async Task<TEntity?> GetByIdAsync(ISpecifications<TEntity, Tkey> spec, CancellationToken ct = default)
        {
            var query = SpecificationEvaluatuator.CreateQuery(dbContext.Set<TEntity>(), spec);
            return await query.FirstOrDefaultAsync();
        }

        public void Remove(TEntity entity) => dbContext.Set<TEntity>().Remove(entity);

        public void Update(TEntity entity) => dbContext.Set<TEntity>().Update(entity);

    }
}
