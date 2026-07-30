namespace E_Commerce.Domin.Contracts
{
    public interface IDataSeeder
    {
        Task SeedDataAsync(CancellationToken ct = default);
    }
}
