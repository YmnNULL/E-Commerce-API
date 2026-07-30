using E_Commerce.Domin.Commen;

namespace E_Commerce.Domin.Contracts
{
    public interface IGenericRepository<TEntity, Tkey> where TEntity : BaseEntity<Tkey>
    {
        void Add(TEntity entity);
        void Update(TEntity entity);
        void Remove(TEntity entity);
        Task<TEntity?> GetByIdAsync(Tkey id, CancellationToken ct = default);
        Task<TEntity?> GetByIdAsync(ISpecifications<TEntity,Tkey> spec , CancellationToken ct = default);
        Task<IReadOnlyList<TEntity>> GetAllAsync(CancellationToken ct = default);
        Task<IReadOnlyList<TEntity>> GetAllAsync( ISpecifications<TEntity, Tkey> spec ,CancellationToken ct = default);

    }
}
