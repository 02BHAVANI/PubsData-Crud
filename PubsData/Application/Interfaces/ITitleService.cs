using PubsData.Domain.Entities;
namespace PubsData.Application.Interfaces
{
    public interface ITitleService
    {
        Task<IEnumerable<Title>> ListAsync(string? search = null);
        Task<Title?> GetAsync(string titleId);
        Task CreateAsync(Title title);
        Task UpdateAsync(Title title);
        Task DeleteAsync(string titleId);
    }
}