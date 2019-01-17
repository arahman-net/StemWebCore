using Microsoft.AspNetCore.Http;
using Stem.Core;
using StemHttp.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace StemWeb.Core.Services
{
    public class BaseService<TEntity> where TEntity : BaseEntity
    {
        protected HttpRequestSrvice _service;
        IHttpContextAccessor _context;
        protected string _resourceUrl = typeof(TEntity).Name + "s";
        protected string filterPrefix = "/?";
        protected string filter;
        public BaseService(IHttpContextAccessor context, HttpRequestSrvice service)
        {
            _context = context;
            _service = service;

            _service.BaseUri = "http://localhost:54000/api/";
            //CompanyFilter = true;
            //companyContextFilter = $"CompanyContextId == {CurrentCompanyId} && IsActive == true";
        }

        public string SysUserId
        {
            get
            {
                return _context != null && _context.HttpContext != null ?
                    ((ClaimsPrincipal)_context.HttpContext.User)
                    .FindFirst("SysUserId").Value : "";
            }
        }
        public string Username
        {
            get
            {
                return _context != null && _context.HttpContext != null ?
                    ((ClaimsPrincipal)_context.HttpContext.User)
                    .FindFirst(ClaimTypes.Name).Value : "";
            }
        }
        public string DefaultCompanyId
        {
            get
            {
                return _context != null && _context.HttpContext != null ?
                    ((ClaimsPrincipal)_context.HttpContext.User)
                    .FindFirst("DefaultCompanyId").Value : "";
            }
        }
        public string BearerToken
        {
            get
            {
                return _context != null && _context.HttpContext != null ?
                    ((ClaimsPrincipal)_context.HttpContext.User)
                    .FindFirst("AcessToken").Value : "";
            }
        }
        public List<string> Permissions
        {
            get
            {
                return _context != null && _context.HttpContext != null ?
                    ((ClaimsPrincipal)_context.HttpContext.User)
                    .FindAll(ClaimTypes.Role).Select(c => c.Value).ToList(): null;
            }
        }


    }
}
