using Microsoft.EntityFrameworkCore;
using PubsData.Domain.Entities;
using PubsData.Infrastructure.Data;

namespace PubsData.Infrastructure.Repositories
{
    public class SalesRepository
    {
        private readonly PubsContext _ctx;
        public SalesRepository(PubsContext ctx) => _ctx = ctx;

        public Task<List<Sales>> ListAsync()
        {
            return _ctx.Sales
                .FromSqlRaw("EXEC dbo.Sales_List")
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Sales?> GetAsync(string storId, string ordNum, string titleId)
        {
            var list = await _ctx.Sales
                .FromSqlInterpolated($"EXEC dbo.Sales_Get {storId}, {ordNum}, {titleId}")
                .AsNoTracking()
                .ToListAsync();

            return list.FirstOrDefault();
        }

        public Task<int> CreateAsync(Sales s) =>
            _ctx.Database.ExecuteSqlInterpolatedAsync(
                $"EXEC dbo.Sales_Insert {s.StorId}, {s.OrdNum}, {s.TitleId}, {s.OrdDate}, {s.Qty}, {s.Payterms}");

        public Task<int> UpdateAsync(Sales s) =>
            _ctx.Database.ExecuteSqlInterpolatedAsync(
                $"EXEC dbo.Sales_Update {s.StorId}, {s.OrdNum}, {s.TitleId}, {s.OrdDate}, {s.Qty}, {s.Payterms}");

        public Task<int> DeleteAsync(string storId, string ordNum, string titleId) =>
            _ctx.Database.ExecuteSqlInterpolatedAsync(
                $"EXEC dbo.Sales_Delete {storId}, {ordNum}, {titleId}");

        public Task<List<Title>> GetTitlesAsync() =>
            _ctx.Titles
                .FromSqlRaw("EXEC dbo.Titles_List")
                .AsNoTracking()
                .ToListAsync();

        public Task<List<Store>> GetStoresAsync() =>
            _ctx.Stores
                .FromSqlRaw("EXEC dbo.Stores_List")
                .AsNoTracking()
                .ToListAsync();
    }

}
