using System;
using System.Collections.Generic;
using System.Text;

namespace StemHttp.Core
{
    public static class HttpModuleEnums
    {
        public enum ChannelTypes
        {
            None = 0,
            Amazon = 1,
            Walmart = 2,
            eBay = 3,
            MapProxyServer = 4
        }

        public enum AuthTypes
        {
            No_Auth = 0,
            Bearer = 1,
            Basic = 2,
            Digest = 3,
            OAuth1 = 4,
            OAuth2 = 5,
            AWS_Sig = 6
        }

        public enum SecretsSendMethods
        {
            Basic_Auth_Headers = 1,
            Credentials_Body = 2
        }
    }
}
