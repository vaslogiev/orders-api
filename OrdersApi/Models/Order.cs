using System.Text.Json.Serialization;

namespace OrdersApi.Models;

public class Order
{
    [JsonIgnore]
    public int Id { get; set; }
    
    public string CustomerName { get; set; } = string.Empty;
    
    public decimal TotalAmount { get; set; }
    
    [JsonIgnore]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}