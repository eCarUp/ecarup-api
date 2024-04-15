using System.Net.Http.Headers;
using System.Text.Json;

namespace ecarupGrpcApi
{
    public class SmartMeEcarupOauthClient
    {
        const string TokenEndpoint = "oauth/token";

        /// <summary>
        /// This method uses the OAuth Client Credentials Flow to get an Access Token to provide
        /// Authorization to the ecarup APIs.
        /// </summary>
        public static async Task<string> GetAccessToken(
            string identityProviderUrl,
            string client_id,
            string client_secret)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(identityProviderUrl);

                // We want the response to be JSON.
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // Build up the data to POST.
                var postData = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("grant_type", "client_credentials"),
                    new KeyValuePair<string, string>("client_id", client_id),
                    new KeyValuePair<string, string>("client_secret", client_secret)
                };

                var content = new FormUrlEncodedContent(postData);

                // Post to the Server and parse the response.
                var response = await client.PostAsync(TokenEndpoint, content);
                var jsonString = await response.Content.ReadAsStringAsync();
                var responseData = JsonSerializer.Deserialize<AuthResponse>(jsonString);

                // return the Access Token.
                return responseData.access_token;
            }
        }
    }
}
