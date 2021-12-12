using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ZohoIntegrationMVC.Configurations;

namespace ZohoIntegrationMVC.ApiController
{
    [Route("api/[controller]")]
    [ApiController]
    public class ZohoController : ControllerBase
    {
        private readonly AppSettings _appSettings;

        public ZohoController(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }
        [HttpGet("GetTokenFromRefresh")]
        public IActionResult GetAccessFromRefreshToken(string refreshToken)
        {
            var response = Helpers.GenerateZohoTokenFromRefreshToken(refreshToken, _appSettings);
            return Ok(response);
        }
    }
}
