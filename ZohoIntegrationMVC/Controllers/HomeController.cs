using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Diagnostics;
using ZohoIntegrationMVC.Configurations;
using ZohoIntegrationMVC.Models;

namespace ZohoIntegrationMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppSettings _appSettings;

        public HomeController(ILogger<HomeController> logger, IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Users()
        {
            var oAuthToken = HttpContext.Session.GetString("OAuthToken");

            ViewBag.Users = Helpers.ListUsers(oAuthToken, _appSettings);
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Callback(string code = "")
        {
            ViewBag.AccessToken = string.Empty;
            ViewBag.RefreshToken = string.Empty;
            ViewBag.ExpiresIn = string.Empty;
            ViewBag.AccessToken = string.Empty;
            ViewBag.ApiDomain = string.Empty;
            ViewBag.TokenType = string.Empty;

            try
            {
                //code = Grant Token
                if (string.IsNullOrWhiteSpace(code))
                {
                    string ParentURL = string.Format("https://accounts.zoho.com/oauth/v2/auth?scope={0}&prompt=consent&client_id={1}&response_type=code&access_type={2}&redirect_uri={3}",
                        _appSettings.Scopes, _appSettings.ClientId, _appSettings.AccessType, _appSettings.RedirectUri);
                    return Redirect(ParentURL);
                }
                else
                {
                    var response = Helpers.GenerateZohoToken(code, _appSettings);
                    ViewBag.AccessToken = response.AccessToken;
                    ViewBag.RefreshToken = response.RefreshToken;
                    ViewBag.ExpiresIn = response.ExpiresIn;
                    ViewBag.ApiDomain = response.ApiDomain;
                    ViewBag.TokenType = response.TokenType;
                    HttpContext.Session.SetString("OAuthToken", response.AccessToken);

                }
            }
            catch (Exception Ex)
            {
            }
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
