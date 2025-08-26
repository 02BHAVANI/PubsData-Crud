using PubsData.Application.Interfaces;
using PubsData.Domain.Entities;
using PubsData.Infrastructure.Repositories;

namespace PubsData.Application.Services
{
    public class PublisherService : IPublisherService
    {
        private readonly PublishersRepository _repo;
        public PublisherService(PublishersRepository repo) => _repo = repo;

        public async Task<IEnumerable<Publisher>> ListAsync(string? search = null)
        {
            var all = await _repo.ListAsync();
            if (string.IsNullOrWhiteSpace(search)) return all;
            var q = search.Trim().ToLowerInvariant();
            return all.Where(p =>
                p.PubId.ToLower().Contains(q) ||
                p.PubName.ToLower().Contains(q) ||
                (p.City ?? "").ToLower().Contains(q) ||
                (p.State ?? "").ToLower().Contains(q) ||
                (p.Country ?? "").ToLower().Contains(q));
        }

        public Task<Publisher?> GetAsync(string pubId) => _repo.GetAsync(pubId);
        public Task CreateAsync(Publisher publisher) => _repo.CreateAsync(publisher);
        public Task UpdateAsync(Publisher publisher) => _repo.UpdateAsync(publisher);
        public Task DeleteAsync(string pubId) => _repo.DeleteAsync(pubId);
    }

}
