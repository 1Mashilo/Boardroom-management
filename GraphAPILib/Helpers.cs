using boardroom_management.TokenStorage;
using Microsoft.Graph;
using Microsoft.Identity.Client;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace boardroom_management.GraphAPILib
{
    public class Helpers
    {
        public static async Task<string> GetAccessToken(HttpContext httpContext)
        {
            string accessToken = null;

            // Load the app config from appsettings.json
            string appId = System.Configuration.ConfigurationManager.AppSettings["ida:AppId"];
            string appPassword = System.Configuration.ConfigurationManager.AppSettings["ida:AppPassword"];
            string redirectUri = System.Configuration.ConfigurationManager.AppSettings["ida:RedirectUri"];
            string[] scopes = System.Configuration.ConfigurationManager.AppSettings["ida:AppScopes"]
                .Replace(' ', ',').Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            // Get the current user's ID
            string userId = httpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (!string.IsNullOrEmpty(userId))
            {
                // Get the user's token cache
                SessionTokenCache tokenCache = new SessionTokenCache(userId, httpContext, ConfidentialClientApplicationBuilder.Create(appId)
                    .WithClientSecret(appPassword)
                    .WithRedirectUri(redirectUri)
                    .Build());

                IConfidentialClientApplication cca = ConfidentialClientApplicationBuilder.Create(appId)
                    .WithClientSecret(appPassword)
                    .WithRedirectUri(redirectUri)
                    .Build();

                // Call AcquireTokenSilent, which will return the cached
                // access token if it has not expired. If it has expired, it will
                // handle using the refresh token to get a new one.
                AuthenticationResult result = await cca.AcquireTokenSilent(scopes, cca.GetAccountsAsync().Result.FirstOrDefault()).ExecuteAsync();

                accessToken = result.AccessToken;
            }

            return accessToken;
        }

        public static async Task<string> GetUserEmail(HttpContext httpContext)
        {
            GraphServiceClient client = new GraphServiceClient(
                new DelegateAuthenticationProvider(
                    async (requestMessage) =>
                    {
                        string accessToken = await Helpers.GetAccessToken(httpContext);
                        requestMessage.Headers.Authorization =
                            new AuthenticationHeaderValue("Bearer", accessToken);
                    }));

            // Get the user's email address
            try
            {
                Microsoft.Graph.User user = await client.Me.Request().GetAsync();
                return user.Mail;
            }
            catch (ServiceException ex)
            {
                return string.Format("#ERROR#: Could not get user's email address. {0}", ex.Message);
            }
        }

        public const string GraphQueryUrl = "https://graph.microsoft.com/v1.0/me/calendars/AAMkADA0NDU2M2Q3LWNmYmYtNDQxOS1hMGNiLTQ5MzZmNDE4N2E4YQBGAAAAAADH6Gkr6C_wQZBa2ATY99PWBwDwBEJQvRi-TIFWAWJVJieuAAAAAAEGAADwBEJQvRi-TIFWAWJVJieuAAAFo3SGAAA=/events";
        public static async Task<bool> AddEvent(HttpContext httpContext, Models.BaseEventRequestBody eventRequestBody)
        {
            string token = await GetAccessToken(httpContext);

            HttpClient client = new HttpClient();
            HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Post, GraphQueryUrl);
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            string json = JsonConvert.SerializeObject(eventRequestBody);

            requestMessage.Content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.SendAsync(requestMessage);

            var statusCode = response.IsSuccessStatusCode;

            string respJson = await response.Content.ReadAsStringAsync();
            dynamic test = JsonConvert.DeserializeObject(respJson);

            return statusCode;
        }

        public static async Task CreateEvent(HttpContext httpContext, Models.BaseEventRequestBody eventRequestBody)
        {
            await AddEvent(httpContext, eventRequestBody);
        }
    }
}