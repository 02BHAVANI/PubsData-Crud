using PubsData.Application.Interfaces;
using PubsData.Domain.Entities;
using PubsData.Infrastructure.Repositories;

namespace PubsData.Application.Services
{
    public class SalesService : ISalesService
    {
        private readonly SalesRepository _repo;
        public SalesService(SalesRepository repo) => _repo = repo;

        public async Task<IEnumerable<Sales>> ListAsync(string? search = null)
        {
            var all = await _repo.ListAsync();
            if (string.IsNullOrWhiteSpace(search)) return all;

            var q = search.Trim().ToLowerInvariant();
            return all.Where(s =>
                s.StorId.ToLower().Contains(q) ||
                s.OrdNum.ToLower().Contains(q) ||
                s.TitleId.ToLower().Contains(q) ||
                (s.TitleName ?? "").ToLower().Contains(q) ||
                s.Payterms.ToLower().Contains(q));
        }

        public Task<Sales?> GetAsync(string storId, string ordNum, string titleId) =>
            _repo.GetAsync(storId, ordNum, titleId);

        public Task CreateAsync(Sales sale) => _repo.CreateAsync(sale);
        public Task UpdateAsync(Sales sale) => _repo.UpdateAsync(sale);
        public Task DeleteAsync(string storId, string ordNum, string titleId) =>
            _repo.DeleteAsync(storId, ordNum, titleId);

        public Task<List<Title>> GetTitlesAsync() => _repo.GetTitlesAsync();
        public Task<List<Store>> GetStoresAsync() => _repo.GetStoresAsync();
    }
}
