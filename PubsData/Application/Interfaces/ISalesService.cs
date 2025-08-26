using PubsData.Domain.Entities;
namespace PubsData.Application.Interfaces
{
    public interface ISalesService
    {
        Task<IEnumerable<Sales>> ListAsync(string? search = null);
        Task<Sales?> GetAsync(string storId, string ordNum, string titleId);
        Task CreateAsync(Sales sale);
        Task UpdateAsync(Sales sale);
        Task DeleteAsync(string storId, string ordNum, string titleId);

        Task<List<Title>> GetTitlesAsync();
        Task<List<Store>> GetStoresAsync();
    }
}