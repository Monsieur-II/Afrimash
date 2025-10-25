using Afrimash.Api.Services;
using Afrimash.Api.Utils;

namespace Afrimash.Api;

public static class Extensions
{
    public static void LoadDataAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var logger = app.Services.GetRequiredService<ILogger<Program>>();
        var readerService = app.Services.GetRequiredService<FileReaderService>();

        try
        {
            readerService.LoadCustomerData();
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error occured while loading data");
        }
    }
    
    public static ApiResponse<T> ToApiResponse<T>(this T data, string message, int code) => new(message, code, data);
}
