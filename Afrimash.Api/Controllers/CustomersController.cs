using Afrimash.Api.Services;
using Afrimash.Api.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Afrimash.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class CustomersController(FileReaderService readerService, IOptions<ExternalUrls> config) : ControllerBase
{
    [HttpPost("login")]
    public IActionResult GetCustomerData(CustomerLoginRequest request)
    {
        var data = readerService.GetCustomerByEmail(request.email);

        if (data is null)
            return NotFound();

        return Ok(data);
    }
}


public class CustomerLoginRequest
{
    public string email { get; set; }
}
