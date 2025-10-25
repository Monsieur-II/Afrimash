using Afrimash.Api.Services;
using Afrimash.Api.Utils;
using Flurl.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Afrimash.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class CustomersController(FileReaderService readerService, IOptions<ExternalUrls> config) : ControllerBase
{
    [HttpPost("login")]
    public async Task<IActionResult> GetCustomerData(CustomerLoginRequest request)
    {
        try
        {
            var data = readerService.GetCustomerByEmail(request.email);

            string url;

            if (data is null)
            {
                url = $"{config.Value.BaseUrl}/api/v1/new-customer";
                
                var newCustomerRes = await url
                    .WithTimeout(TimeSpan.FromMinutes(10))
                    .PostJsonAsync(data);
            
                var popularProducts = await newCustomerRes.GetJsonAsync<List<RecommendationResponse>>();

                return Ok(popularProducts);
            }
            
            url = $"{config.Value.BaseUrl}/api/v1/recommendations";
           
            var response = await url
                .WithTimeout(TimeSpan.FromMinutes(10))
                .PostJsonAsync(data);
            
            var recommendations = await response.GetJsonAsync<List<RecommendationResponse>>();

            return Ok(recommendations);
        }
        catch (Exception e)
        {
            return BadRequest();
        }
    }
}

public class RecommendationResponse
{
    public string title { get; set; }
    public string image_url { get; set; }
}



public class CustomerLoginRequest
{
    public string email { get; set; }
}
