using Newtonsoft.Json.Linq;

namespace Helper
{
    public class TwitchHelper
    {
        static readonly HttpClientHandler hcHandle = new();

        public static async Task<bool> IsOnline(string channel)
        {
            using (var hc = new HttpClient(hcHandle, false))
            // false here prevents disposing the handler, which should live for the duration of the program and be shared by all requests that use the same handler properties
            {
                hc.DefaultRequestHeaders.Add("Client-ID", "your client id");
                hc.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", "your oauth token, should you have one (you should, but not required)");
                hc.DefaultRequestHeaders.UserAgent.ParseAdd("this would be good practice to set to your own");
                hc.Timeout = TimeSpan.FromSeconds(5); // good idea to set to something reasonable

                using (var response = await hc.GetAsync($"https://api.twitch.tv/helix/streams?user_login={channel}"))
                {
                    response.EnsureSuccessStatusCode(); // throws, if fails, can check response.StatusCode yourself if you prefer
                    string jsonString = await response.Content.ReadAsStringAsync();
                    dynamic data = JObject.Parse(jsonString);

                    if (data.live == true)
                    {
                        return true;
                    }
                }
                return false;
            }
        }
    }
}
