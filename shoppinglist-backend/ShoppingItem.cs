using Azure;
using Azure.Data.Tables;

namespace shoppinglist_backend;

public class ShoppingItem : ITableEntity
{
    public string PartitionKey { get; set; } = "ShoppingItem";
    public string RowKey { get; set; } // Unique identifier
    public string Name { get; set; }
    public string Category { get; set; }
    public DateTimeOffset? Timestamp { get; set; } 
    public ETag ETag { get; set; }
}