using System.Text.Json.Serialization;

namespace Afrimash.Api.Models;

public class Customer
{
    public string CustomerId { get; set; }
    public string Email_Address { get; set; }
    public string Frequency { get; set; }
    public string Monetary { get; set; }
    public string Avg_Order_Value { get; set; }
    public string Customer_Lifetime_Days { get; set; }
    public string Purchase_Rate { get; set; }
    public string Customer_Type { get; set; }
    public string Attribution { get; set; }
    public string Total_Items_Sold { get; set; }
}
