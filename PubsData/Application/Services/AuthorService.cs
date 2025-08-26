using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PubsData.Application.Interfaces;
using PubsData.Domain.Entities;
using PubsData.Infrastructure.Repositories;

namespace PubsData.Application.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly AuthorRepository _repo;
        public AuthorService(AuthorRepository repo) => _repo = repo;

        public async Task<IEnumerable<Author>> ListAsync(string? search)
        {
            var all = await _repo.ListAsync();
            if (string.IsNullOrWhiteSpace(search)) return all;
            var q = search.Trim().ToLowerInvariant();
            return all.Where(a =>
                a.AuId.ToLower().Contains(q) ||
                a.AuLName.ToLower().Contains(q) ||
                a.AuFName.ToLower().Contains(q) ||
                (a.City ?? "").ToLower().Contains(q) ||
                (a.State ?? "").ToLower().Contains(q));
        }

        public Task<Author?> GetAsync(string auId) => _repo.GetAsync(auId);
        public async Task CreateAsync(Author author)
        {
            try
            {
                await _repo.CreateAsync(author);
            }
            catch (SqlException ex)
            {
                throw new DbUpdateException("Error inserting Author.", ex);
            }
        }

        public async Task UpdateAsync(Author author)
        {
            try
            {
                await _repo.UpdateAsync(author);
            }
            catch (SqlException ex)
            {
                throw new DbUpdateException("Error updating Author.", ex);
            }
        }

        public Task DeleteAsync(string auId) => _repo.DeleteAsync(auId);
    }

}
