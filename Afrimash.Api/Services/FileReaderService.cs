using System.Text;
using Afrimash.Api.Models;
using ExcelDataReader;

namespace Afrimash.Api.Services;

public class FileReaderService(ILogger<FileReaderService> logger)
{
    private List<Customer> _customers = [];
    public void LoadCustomerData()
    {
        var filInDirectory = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
        var filePath = Path.Combine(filInDirectory!, "data.xlsx");
        
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        
        var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
        
        using var reader = ExcelReaderFactory.CreateReader(stream);
        var ds = reader.AsDataSet();
        reader.Close();

        var data = ds.Tables[0];
        
        var totalRows = data.Rows.Count;

        for (var i = 1; i < totalRows; i++)
        {
            var customerId = data.Rows[i][0].ToString()?.Trim();
            var frequency = data.Rows[i][1].ToString()?.Trim();
            var monetary = data.Rows[i][2].ToString()?.Trim();
            var avgOrderValue = data.Rows[i][3].ToString()?.Trim();
            var customerLifetimeDays = data.Rows[i][4].ToString()?.Trim();
            var purchaseDate = data.Rows[i][5].ToString()?.Trim();
            var customerType = data.Rows[i][6].ToString()?.Trim();
            var attribution = data.Rows[i][7].ToString()?.Trim();
            var totalItemsSold = data.Rows[i][8].ToString()?.Trim();
            var email = data.Rows[i][9].ToString()?.Trim();
            
            _customers.Add(new Customer
            {
                CustomerId = customerId ?? string.Empty,
                Email_Address = email ?? string.Empty,
                Frequency = frequency ?? string.Empty,
                Monetary = monetary ?? string.Empty,
                Avg_Order_Value = avgOrderValue ?? string.Empty,
                Customer_Type = customerType ?? string.Empty,
                Customer_Lifetime_Days = customerLifetimeDays ?? string.Empty,
                Purchase_Rate = purchaseDate ?? string.Empty,
                Attribution = attribution ?? string.Empty,
                Total_Items_Sold = totalItemsSold ?? string.Empty
            });
        }
        
        logger.LogInformation("Successfully loaded customer data");
    }


    public Customer? GetCustomerByEmail(string email)
    {
        var customer = _customers.Find(x => x.Email_Address == email);

        return customer;
    }
}
