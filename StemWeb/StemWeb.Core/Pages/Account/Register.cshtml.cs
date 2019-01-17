using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SharedStem.Core.Entities;
using StemHttp.Core;

namespace StemWeb.Core.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly ILogger<RegisterModel> _logger;
        //private readonly IEmailSender _emailSender;
        private readonly HttpRequestSrvice _apiService;
        private readonly IConfiguration _configuration;

        public RegisterModel(
            ILogger<RegisterModel> logger,
            HttpRequestSrvice apiService,
            IConfiguration configuration
            /*IEmailSender emailSender*/)
        {
            _apiService = apiService;
            _logger = logger;
            _configuration = configuration;
            //_emailSender = emailSender;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public class InputModel
        {
            [Required]
            [Display(Name = "User Name")]
            public string UserName { get; set; }

            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
        }

        public void OnGet(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            if (ModelState.IsValid)
            {
                var authServerURL = _configuration.GetValue<string>("AuthServerURL");
                var registerEndPoint = _configuration.GetValue<string>("RegisterUserEndPoint");

                using (HttpClient httpClient = new HttpClient())
                {
                    var baseAddress = authServerURL;
                    var getTokenUrl = authServerURL + registerEndPoint;

                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    //httpClient.DefaultRequestHeaders.Authorization =
                    //    new AuthenticationHeaderValue("Basic", Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(string.Format("{0}:{1}", options.ClientId, options.ClientSecret))));


                    HttpContent content = new FormUrlEncodedContent(new[]
                    {
                        new KeyValuePair<string, string>("UserName", Input.UserName),
                        new KeyValuePair<string, string>("Email", Input.Email),
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

                //var user = new IdentityUser { UserName = Input.Email, Email = Input.Email };
                //var result = await _userManager.CreateAsync(user, Input.Password);
                //if (result.Succeeded)
                //{
                //    _logger.LogInformation("User created a new account with password.");

                //    //TODO: Initialize Company Types in seed method
                //    CompanyType cType = _dbContext.CompanyTypes.SingleOrDefault(ct => ct.Name == "RealEstate");
                //    if (cType == null)
                //    {
                //        cType = new CompanyType
                //        {
                //            Name = "RealEstate",
                //            Description = "Real Estate"
                //        };

                //        _dbContext.Add(cType);
                //        _dbContext.SaveChanges();
                //    }

                //    //Create company using the register parameters
                //    Company c = _dbContext.Companies.SingleOrDefault(ct => ct.CompanyCode == "001");
                //    if (c == null)
                //    {
                //        c = new Company()
                //        {
                //            CompanyCode = "001",
                //            CompanyType = cType,
                //            Name = "AlHadi Estate",
                //            ShortName = "AHE"
                //        };

                //        _dbContext.Add(c);
                //        _dbContext.SaveChanges();
                //    }

                //    SysUser sUser = _dbContext.SysUsers.SingleOrDefault(u => u.AccessToken == user.Id);
                //    if (sUser == null)
                //    {
                //        sUser = new SysUser
                //        {
                //            UserName = Input.Email,
                //            EmailAddress = Input.Email,
                //            AccessToken = user.Id,
                //            TokenProvider = "Asp.Net",
                //            IsActive = true
                //        };

                //        _dbContext.Add(sUser);
                //        _dbContext.SaveChanges();
                //    }

                //    List<Company> asCs = new List<Company>();
                //    asCs.Add(c);

                //    UserAssignedCompany uaCompany = new UserAssignedCompany
                //    {
                //        Companies = asCs,
                //        IsActive = true,
                //        SysUser = sUser
                //    };

                //    _dbContext.Add(uaCompany);
                //    _dbContext.SaveChanges();

                //    SecurityGroup sGroup = _dbContext.SecurityGroups.FirstOrDefault(sg => sg.CompanyContextId == c.Id);
                //    if (sGroup == null)
                //    {
                //        sGroup = new SecurityGroup
                //        {
                //            CompanyContext = c,
                //            IsActive = true,
                //            IsOwner = true,
                //        };

                //        _dbContext.Add(sGroup);
                //        _dbContext.SaveChanges();
                //    }

                //    List<SecurityGroup> sGroups = new List<SecurityGroup>();
                //    sGroups.Add(sGroup);

                //    UserSecurityGroup uSGs = new UserSecurityGroup
                //    {
                //        CompanyContext = c,
                //        SysUser = sUser,
                //        IsActive = true,
                //        SecurityGroups = sGroups
                //    };

                //    _dbContext.Add(uSGs);
                //    _dbContext.SaveChanges();



                //    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                //    var callbackUrl = Url.Page(
                //        "/Account/ConfirmEmail",
                //        pageHandler: null,
                //        values: new { userId = user.Id, code = code },
                //        protocol: Request.Scheme);

                //    await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                //        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                //    await _signInManager.SignInAsync(user, isPersistent: false);
                //    return LocalRedirect(returnUrl);
                //}
                //foreach (var error in result.Errors)
                //{
                //    ModelState.AddModelError(string.Empty, error.Description);
                //}
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
