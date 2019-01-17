using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using StemHttp.Core;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using SharedStem.Core.Entities;
using System.Linq;
using Microsoft.Extensions.Configuration;

namespace StemWeb.Core.Pages.Account
{
    [AllowAnonymous]
    public class LoginModel : PageModel
    {
        private readonly ILogger<LoginModel> _logger;
        private readonly HttpRequestSrvice _apiService;
        private readonly IConfiguration _configuration;

        public LoginModel(

            ILogger<LoginModel> logger,
            HttpRequestSrvice apiService,
            IConfiguration configuration)
        {
            _logger = logger;
            _apiService = apiService;
            _configuration = configuration;
        }

        
        [BindProperty]
        public InputModel Input { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public string ReturnUrl { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

        public class InputModel
        {
            [Required]
            public string Email { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [Display(Name = "Remember me?")]
            public bool RememberMe { get; set; }
        }
        public class TokenModel
        {
            public int Id { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Username { get; set; }
            public string Token { get; set; }
            public string Email { get; internal set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            returnUrl = returnUrl ?? Url.Content("~/");

            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            //ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");

            if (ModelState.IsValid)
            {
                var authServerURL = _configuration.GetValue<string>("AuthServerURL");
                var accessTokenEndPoint = _configuration.GetValue<string>("AccessTokenEndPoint");
                using (HttpClient httpClient = new HttpClient())
                {
                    var baseAddress = authServerURL;
                    var getTokenUrl = authServerURL + accessTokenEndPoint;

                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    //httpClient.DefaultRequestHeaders.Authorization =
                    //    new AuthenticationHeaderValue("Basic", Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(string.Format("{0}:{1}", options.ClientId, options.ClientSecret))));


                    HttpContent content = new FormUrlEncodedContent(new[]
                    {
                    new KeyValuePair<string, string>("UserName", Input.Email),
                    new KeyValuePair<string, string>("Password", Input.Password),
                    });

                    httpClient.BaseAddress = new Uri(baseAddress);
                    HttpResponseMessage result = httpClient.PostAsync(getTokenUrl, content).Result;
                    if (result.IsSuccessStatusCode)
                    {
                        string resultContent = result.Content.ReadAsStringAsync().Result;
                        //resultContent = resultContent.Replace("\\", string.Empty).TrimStart('"').TrimEnd('"');
                        var tokenInfo = JsonConvert.DeserializeObject<TokenInfo>(resultContent.ToString());

                        var claims = new List<Claim>
                        {
                            new Claim("SysUserId", tokenInfo.UserId),
                            new Claim("AcessToken", tokenInfo.Token),
                            new Claim("DefaultCompanyId", tokenInfo.DefaultCompanyId)
                        };

                        foreach (var role in tokenInfo.Roles)
                        {
                            claims.Add(new Claim(ClaimTypes.Role, role.Value));
                        }

                        ClaimsIdentity userIdentity = new ClaimsIdentity(claims, "login");
                        ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);

                        await HttpContext.SignInAsync(principal);
                        return LocalRedirect(returnUrl);
                    }
                    else
                    {
                        string resultContent = result.Content.ReadAsStringAsync().Result;
                        throw new Exception("Exception - " + resultContent);
                    }

                }

            }
            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
