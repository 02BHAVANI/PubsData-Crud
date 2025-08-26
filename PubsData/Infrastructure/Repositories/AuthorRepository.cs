using Microsoft.EntityFrameworkCore;
using PubsData.Domain.Entities;
using PubsData.Infrastructure.Data;

namespace PubsData.Infrastructure.Repositories
{
    public class AuthorRepository
    {
        private readonly PubsContext _ctx;
        public AuthorRepository(PubsContext ctx) => _ctx = ctx;

        public async Task<IEnumerable<Author>> ListAsync()
        {
            return await _ctx.Authors.FromSqlRaw("EXEC dbo.Authors_List").ToListAsync();
        }

        public async Task<Author?> GetAsync(string auId)
        {
            var list = await _ctx.Authors.FromSqlInterpolated($"EXEC dbo.Authors_Get {auId}").ToListAsync();
            return list.FirstOrDefault();
        }

        public async Task CreateAsync(Author a)
        {
            await _ctx.Database.ExecuteSqlInterpolatedAsync($"EXEC dbo.Authors_Insert {a.AuId}, {a.AuLName}, {a.AuFName}, {a.Phone}, {a.Address}, {a.City}, {a.State}, {a.Zip}, {a.Contract}");
        }

        public async Task UpdateAsync(Author a)
        {
            await _ctx.Database.ExecuteSqlInterpolatedAsync($"EXEC dbo.Authors_Update {a.AuId}, {a.AuLName}, {a.AuFName}, {a.Phone}, {a.Address}, {a.City}, {a.State}, {a.Zip}, {a.Contract}");
        }

        public async Task DeleteAsync(string auId)
        {
            await _ctx.Database.ExecuteSqlInterpolatedAsync($"EXEC dbo.Authors_Delete {auId}");
        }
    }

}

