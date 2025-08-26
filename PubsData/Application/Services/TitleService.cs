using PubsData.Application.Interfaces;
using PubsData.Domain.Entities;
using PubsData.Infrastructure.Repositories;

namespace PubsData.Application.Services
{
    public class TitleService : ITitleService
    {
        private readonly TitlesRepository _repo;
        public TitleService(TitlesRepository repo) => _repo = repo;

        public async Task<IEnumerable<Title>> ListAsync(string? search = null)
        {
            var all = await _repo.ListAsync();
            if (string.IsNullOrWhiteSpace(search)) return all;
            var q = search.Trim().ToLowerInvariant();
            return all.Where(t =>
                t.TitleId.ToLower().Contains(q) ||
                t.Name.ToLower().Contains(q) ||
                t.Type.ToLower().Contains(q) ||
                (t.PubId ?? "").ToLower().Contains(q));
        }

        public Task<Title?> GetAsync(string titleId) => _repo.GetAsync(titleId);
        public Task CreateAsync(Title title) => _repo.CreateAsync(title);
        public Task UpdateAsync(Title title) => _repo.UpdateAsync(title);
        public Task DeleteAsync(string titleId) => _repo.DeleteAsync(titleId);
    }
}