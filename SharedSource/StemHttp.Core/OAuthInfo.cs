using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using static StemHttp.Core.HttpModuleEnums;

namespace StemHttp.Core
{
    public class OAuthInfo
    {
        //Authenticaton information for site
        public ChannelTypes ChannelType { get; set; }
        public AuthTypes AuthType { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string AuthorizationEndPoint { get; set; }
        public string TokenEndPoint { get; set; }

        public List<string> Scope = new List<string>();
        public string GrantType { get; set; }
        public string RedirectURI { get; set; }
        public TokenInfo TokenInfo { get; set; }
        public bool Sandbox { get; internal set; }
        public HttpMethod AuthEndPointMethod { get; internal set; }
        public SecretsSendMethods SecretsSendMethod { get; internal set; }
        public int TokenExpiryLimitSec { get; internal set; }
        public string BaseAddress { get; internal set; }
    }
}
