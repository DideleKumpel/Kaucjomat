using Kaucjomat.Model;
using SQLite;

namespace Kaucjomat.Data
{
    class DatabaseContext
    {
        private SQLiteAsyncConnection _connection;
        private readonly string _dbPath;

        public DatabaseContext(string dbPath)
        {
            _dbPath = dbPath;
        }

        private async Task Init()
        {
            if (_connection is not null)
                return;

            _connection = new SQLiteAsyncConnection(_dbPath);

            await _connection.CreateTableAsync<Store>();
            await _connection.CreateTableAsync<Voucher>();

            await SeedStores();
        }

        private async Task SeedStores()
        {
            if (await _connection.Table<Store>().CountAsync() == 0)
            {
                var defaultStores = new List<Store>
            {
                new() { Name = "Biedronka", IsUserDefined = false },
                new() { Name = "Lidl", IsUserDefined = false },
                new() { Name = "Kaufland", IsUserDefined = false }
            };
                await _connection.InsertAllAsync(defaultStores);
            }
        }
    }
}
