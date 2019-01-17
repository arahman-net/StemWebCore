using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace StemHttp.Core
{
    public class TokenInfo
    {
        [JsonProperty("error")]
        public string Error { get; internal set; }

        [JsonProperty("user_id")]
        public string UserId { get; set; }

        [JsonProperty("access_token")]
        public string Token { get; set; }

        [JsonProperty("expires_in")]
        public int ExpiresInSeconds { get; set; }

        [JsonProperty("token_type")]
        public string TokenType { get; set; }

        [JsonProperty("default_company_id")]
        public string DefaultCompanyId { get; set; }
        
        [JsonProperty("permissions")]
        public List<RoleInfoModel> Roles { get; set; }

        public DateTime CreatedDate { get; set; }
    }

    public class RoleInfoModel
    {
        public string Type { get; set; }
        public string Value { get; set; }
    }
}
