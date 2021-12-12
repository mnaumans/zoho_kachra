using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZohoIntegrationMVC.Configurations;
using ZohoIntegrationMVC.Models;

namespace ZohoIntegrationMVC
{
    public static class Helpers
    {
        public static string ListUsers(string oAuthToken, AppSettings appSettings)
        {
            try
            {
                var client = new RestClient($"{appSettings.UsersUrl}?organization_id={appSettings.Organization_id}");
                var userRequest = new RestRequest(Method.GET);
                userRequest.AddHeader("Authorization", $"Zoho-oauthtoken {oAuthToken}");

                var response = client.Execute(userRequest);
                return response.Content; 
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public static ResponseViewModels GenerateZohoToken(string GrantToken, AppSettings appSettings)
        {
            try
            {
                var client = new RestClient(appSettings.BaseUrl);
                var signInRequest = new RestRequest("oauth/v2/token");

                signInRequest.AddQueryParameter("grant_type", "authorization_code");
                signInRequest.AddQueryParameter("client_id", appSettings.ClientId);
                signInRequest.AddQueryParameter("client_secret", appSettings.ClientSecret);
                signInRequest.AddQueryParameter("redirect_uri", appSettings.RedirectUri);
                signInRequest.AddQueryParameter("code", GrantToken);

                var response = client.Post(signInRequest);
                return JsonConvert.DeserializeObject<ResponseViewModels>(response.Content); ;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public static ResponseViewModels GenerateZohoTokenFromRefreshToken(string refreshToken, AppSettings appSettings)
        {
            try
            {

                var client = new RestClient(appSettings.BaseUrl);
                var signInRequest = new RestRequest("oauth/v2/token");

                signInRequest.AddQueryParameter("grant_type", "refresh_token");
                signInRequest.AddQueryParameter("client_id", appSettings.ClientId);
                signInRequest.AddQueryParameter("client_secret", appSettings.ClientSecret);
                signInRequest.AddQueryParameter("refresh_token", refreshToken);

                var response = client.Post(signInRequest);
                return JsonConvert.DeserializeObject<ResponseViewModels>(response.Content); 
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
