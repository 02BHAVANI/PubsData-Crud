using Microsoft.EntityFrameworkCore;
using PubsData.Domain.Entities;
using PubsData.Infrastructure.Data;

namespace PubsData.Infrastructure.Repositories
{
    public class TitlesRepository
    {
        private readonly PubsContext _ctx;
        public TitlesRepository(PubsContext ctx) => _ctx = ctx;

        public Task<List<Title>> ListAsync() =>
    _ctx.Titles.FromSqlRaw("EXEC dbo.Titles_List").ToListAsync();

        public async Task<Title?> GetAsync(string titleId)
        {
            var list = await _ctx.Titles.FromSqlInterpolated($"EXEC dbo.Titles_Get {titleId}").ToListAsync();
            return list.FirstOrDefault();
        }

        public Task<int> CreateAsync(Title t) =>
            _ctx.Database.ExecuteSqlInterpolatedAsync($"EXEC dbo.Titles_Insert {t.TitleId}, {t.Name}, {t.Type}, {t.PubId}, {t.Price}, {t.Advance}, {t.Royalty}, {t.YtdSales}, {t.Notes}, {t.PubDate}");

        public Task<int> UpdateAsync(Title t) =>
            _ctx.Database.ExecuteSqlInterpolatedAsync($"EXEC dbo.Titles_Update {t.TitleId}, {t.Name}, {t.Type}, {t.PubId}, {t.Price}, {t.Advance}, {t.Royalty}, {t.YtdSales}, {t.Notes}, {t.PubDate}");

        public Task<int> DeleteAsync(string titleId) =>
            _ctx.Database.ExecuteSqlInterpolatedAsync($"EXEC dbo.Titles_Delete {titleId}");
    }

}

