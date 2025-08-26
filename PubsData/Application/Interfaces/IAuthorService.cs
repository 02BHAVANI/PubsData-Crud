using PubsData.Domain.Entities;
namespace PubsData.Application.Interfaces
{
    public interface IAuthorService
    {
        Task<IEnumerable<Author>> ListAsync(string? search);
        Task<Author?> GetAsync(string auId);
        Task CreateAsync(Author author);
        Task UpdateAsync(Author author);
        Task DeleteAsync(string auId);
    }
}