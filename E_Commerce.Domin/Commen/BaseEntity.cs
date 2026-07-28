namespace E_Commerce.Domin.Commen
{
    public abstract class BaseEntity<TKey>
    {   
        public TKey Id { get; set; } = default!;
    }
}
