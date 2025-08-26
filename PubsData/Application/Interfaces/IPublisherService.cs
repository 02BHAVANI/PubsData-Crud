using PubsData.Domain.Entities;
namespace PubsData.Application.Interfaces
{
    public interface IPublisherService
    {
        Task<IEnumerable<Publisher>> ListAsync(string? search = null);
        Task<Publisher?> GetAsync(string pubId);
        Task CreateAsync(Publisher publisher);
        Task UpdateAsync(Publisher publisher);
        Task DeleteAsync(string pubId);
    }
}