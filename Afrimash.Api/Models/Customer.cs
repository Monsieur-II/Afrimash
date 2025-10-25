using System.Text.Json.Serialization;

namespace Afrimash.Api.Models;

public class Customer
{
    [JsonPropertyName("customerId")] 
    public string CustomerId { get; set; }

    [JsonPropertyName("emailAddress")] 
    public string Email_Address { get; set; }

    [JsonPropertyName("frequency")] 
    public string Frequency { get; set; }

    [JsonPropertyName("monetary")] 
    public string Monetary { get; set; }

    [JsonPropertyName("avgOrderValue")] 
    public string Avg_Order_Value { get; set; }

    [JsonPropertyName("customerLifetimeDays")]
    public string Customer_Lifetime_Days { get; set; }

    [JsonPropertyName("purchaseRate")] 
    public string Purchase_Rate { get; set; }

    [JsonPropertyName("customerType")] 
    public string Customer_Type { get; set; }

    [JsonPropertyName("attribution")] 
    public string Attribution { get; set; }

    [JsonPropertyName("totalItemsSold")] 
    public string Total_Items_Sold { get; set; }
}
