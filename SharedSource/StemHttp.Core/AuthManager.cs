using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using static StemHttp.Core.HttpModuleEnums;

namespace StemHttp.Core
{
    public class AuthManager
    {
        public static TokenInfo GetAccessToken(OAuthInfo provider)
        {
            //// eBay and other sites set the token expiry seconds so we nned to check for the expiry limit of the token to reset it
            //if (OAuthSiteEntry.TokenInfo == null ||
            //    DateTime.UtcNow.Subtract(OAuthSiteEntry.TokenInfo.CreatedDate).Seconds >= (OAuthSiteEntry.TokenExpiryLimitSec - 5))
            //    SetAccessToken(OAuthSiteEntry);
            //return OAuthSiteEntry.TokenInfo;

            //TODO: Add access token into te cahe and retrieve from there

            SetAccessToken(provider);
            return provider.TokenInfo;

        }

        static void SetAccessToken(OAuthInfo options)
        {
            using (HttpClient httpClient = new HttpClient())
            {

                var getTokenUrl = options.TokenEndPoint;

                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpContent content = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("UserName", options.ClientId),
                    new KeyValuePair<string, string>("Password", options.ClientSecret),
                    });

                httpClient.BaseAddress = new Uri(options.BaseAddress);
                HttpResponseMessage result = httpClient.PostAsync(getTokenUrl, content).Result;
                if (result.IsSuccessStatusCode)
                {
                    string resultContent = result.Content.ReadAsStringAsync().Result;
                    options.TokenInfo = JsonConvert.DeserializeObject<TokenInfo>(resultContent);

                    if (!string.IsNullOrWhiteSpace(options.TokenInfo.Error))
                    {
                        throw new Exception(options.TokenInfo.Error);
                    }
                    else
                    {
                        options.TokenInfo.CreatedDate = DateTime.UtcNow;
                    }
                }
                else
                {
                    throw new Exception(options.BaseAddress + ": Exception - " + result.ReasonPhrase);
                }

            }

        }

    }
}
