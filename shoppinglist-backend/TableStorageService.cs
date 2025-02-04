using Azure.Data.Tables;

namespace shoppinglist_backend
{
    public class TableStorageService
    {
        private readonly TableClient _tableClient;

        public TableStorageService(IConfiguration configuration)
        {
        
            var connectionString = configuration.GetConnectionString("AzureStorage");
            var serviceClient = new TableServiceClient(connectionString);
            _tableClient = serviceClient.GetTableClient(GetTableName());
            _tableClient.CreateIfNotExists();
        }

        public static string GetTableName()
        {
            return $"{StaticConfiguration.Name}SinHandleliste";
        }

        public async Task AddItemAsync(ShoppingItem item)
        {
            await _tableClient.AddEntityAsync(item);
        }

        public async Task<List<ShoppingItem>> GetItemsAsync()
        {
            return await _tableClient.QueryAsync<ShoppingItem>().ToListAsync();
        }
    }
}