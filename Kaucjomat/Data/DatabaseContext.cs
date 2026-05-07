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
                new() { Name = "Kaufland", IsUserDefined = false },
                new() { Name = "Netto", IsUserDefined = false }
            };
                await _connection.InsertAllAsync(defaultStores);
            }
        }

        public async Task<List<Store>> GetStoresAsync()
        {
            await Init();
            return await _connection.Table<Store>().ToListAsync();
        }

        public async Task<int> SaveStoreAsync(Store store)
        {
            await Init();
            if (store.Id != 0)
            {
                return await _connection.UpdateAsync(store);
            }
            else
            {
                return await _connection.InsertAsync(store);
            }
        }

        public async Task<int> DeleteStoreAsync(int id)
        {
            await Init();
            return await _connection.DeleteAsync<Store>(id);
        }


        public async Task<List<Voucher>> GetVouchersAsync()
        {
            await Init();
            return await _connection.Table<Voucher>().ToListAsync();
        }

        public async Task<int> SaveVoucherAsync(Voucher voucher)
        {
            await Init();
            if (voucher.Id != 0)
                return await _connection.UpdateAsync(voucher);
            else
                return await _connection.InsertAsync(voucher);
        }

        public async Task<int> DeleteVoucherAsync(int id)
        {
            await Init();
            return await _connection.DeleteAsync<Voucher>(id);
        }
    }
}
