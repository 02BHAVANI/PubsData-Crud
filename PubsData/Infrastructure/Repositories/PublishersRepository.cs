using Microsoft.EntityFrameworkCore;
using PubsData.Domain.Entities;
using PubsData.Infrastructure.Data;

namespace PubsData.Infrastructure.Repositories
{
    public class PublishersRepository
    {
        private readonly PubsContext _ctx;
        public PublishersRepository(PubsContext ctx) => _ctx = ctx;

        public Task<List<Publisher>> ListAsync() =>
    _ctx.Publishers.FromSqlRaw("EXEC dbo.Publishers_List").ToListAsync();

        public async Task<Publisher?> GetAsync(string pubId)
        {
            var list = await _ctx.Publishers.FromSqlInterpolated($"EXEC dbo.Publishers_Get {pubId}").ToListAsync();
            return list.FirstOrDefault();
        }

        public Task<int> CreateAsync(Publisher p) =>
            _ctx.Database.ExecuteSqlInterpolatedAsync($"EXEC dbo.Publishers_Insert {p.PubId}, {p.PubName}, {p.City}, {p.State}, {p.Country}");

        public Task<int> UpdateAsync(Publisher p) =>
            _ctx.Database.ExecuteSqlInterpolatedAsync($"EXEC dbo.Publishers_Update {p.PubId}, {p.PubName}, {p.City}, {p.State}, {p.Country}");

        public Task<int> DeleteAsync(string pubId) =>
            _ctx.Database.ExecuteSqlInterpolatedAsync($"EXEC dbo.Publishers_Delete {pubId}");
    }

}

