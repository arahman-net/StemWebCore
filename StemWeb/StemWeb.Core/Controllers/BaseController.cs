using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using SharedStem.Core;
using Stem.Core;
using StemHttp.Core;

namespace StemWeb.Core.Controllers
{
    public abstract class BaseController<TEntity> : Controller where TEntity : BaseEntity
    {
        private readonly IConfiguration _configuration;
        IHttpContextAccessor _context;
        protected HttpRequestSrvice _service;
        protected string _resourceUrl = typeof(TEntity).Name + "s";
        protected string filterPrefix = "/?";
        public string filter;
        public BaseController(IHttpContextAccessor context ,
            HttpRequestSrvice service, IConfiguration configuration)
        {
            _context = context;
            _service = service;
            _configuration = configuration;
            _service.BaseUri = _configuration.GetValue<string>("AuthServerURL");
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
                    .FindAll(ClaimTypes.Role).Select(c => c.Value).ToList() : null;
            }
        }

        public async Task<IEnumerable<TEntity>> Get()
        {
            //var query = $"{ _resourceUrl}{filterPrefix}{CompanyContextFilter}";
            var query = $"{ _resourceUrl}{filterPrefix}";
            query += string.IsNullOrWhiteSpace(filter) ? "" : filter;

            var response =  await _service.GetAsync<TEntity[]>(
                            query, BearerToken);
            return response;
        }

        public async Task<TEntity> Get(int Id)
        {
            var query = $"{ _resourceUrl}/{Id}";
            var response = await _service.GetAsync<TEntity>(query, Id, BearerToken);
            return response;
        }

        public virtual async Task<TEntity> Post(TEntity entity)
        {
            if (ModelState.IsValid)
            {
                var response = await _service.PostAsJsonAsync<TEntity>(
                    _resourceUrl, entity, BearerToken);
                return response;
            }
            else
            {
                return null;
            }
        }

        public virtual async Task<TEntity> Put(int Id, TEntity entity)
        {
            if (ModelState.IsValid)
            {
                var response = await _service.PutJsonAsync<TEntity>(
                    _resourceUrl + "/" + Id, entity, BearerToken);

                return response;
            }
            else
            {
                return null;
            }
        }

        [Authorize(Roles = "SystemAdmin, Developer, Owner, Read")]
        // GET: TEntity
        public virtual async Task<IActionResult> Index()
        {
            //var query = $"{ _resourceUrl}{filterPrefix}{CompanyContextFilter}";
            var query = $"{ _resourceUrl}";
            query += string.IsNullOrWhiteSpace(filter) ? "" : $"{filterPrefix} && " + filter;

            var response = await _service.GetAsync<TEntity[]>(
                            query, BearerToken);

            //var response = await Get();
            return View(model: response);
        }

        [Authorize(Roles = "SystemAdmin, Developer, Owner, Read")]
        // GET: TEntity/Details/5
        public virtual async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var query = $"{ _resourceUrl}/{id}";
            //    $"{filterPrefix}Id == {id.Value} ";
            //query += CompanyFilter ? " && " : "" + CompanyContextFilter;
            //query += string.IsNullOrWhiteSpace(filter) ? "" : " && " + filter;

            var response = await _service.GetAsync<TEntity>(query, id.Value, BearerToken);


            if (response == null)
            {
                return NotFound();
            }

            return View(model: response);
        }

        [Authorize(Roles = "SystemAdmin, Developer, Owner, Create")]
        // GET: TEntity/Create
        public virtual async Task<IActionResult> Create()
        {
            var model = (TEntity)Activator.CreateInstance(typeof(TEntity));
            if(model.GetType().IsSubclassOf(typeof(CompanyEntity)))
            {
                model.GetType().GetProperty("CompanyContextId").SetValue(model, Convert.ToInt32(DefaultCompanyId));
            }
            return View(model);
        }

       

        // POST: TEntity/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "SystemAdmin, Developer, Owner, Create")]
        public virtual async Task<IActionResult> Create(TEntity entity)
        {
            if (ModelState.IsValid)
            {   
                var response = await Post(entity);

                string returnUrl = Request.Form["returnUrl"];
                if (string.IsNullOrWhiteSpace(returnUrl)) returnUrl = "Index";
                else
                {
                    if (returnUrl.Contains("{}")) returnUrl = returnUrl.Replace("{}", "");
                    return RedirectToAction(returnUrl, new { id = response.Id });
                }

                return RedirectToAction(returnUrl);

            }

            return View(entity);
        }


        // GET: TEntity/Edit/5
        [Authorize(Roles = "SystemAdmin, Developer, Owner, Update")]
        public virtual async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var query = $"{ _resourceUrl}/{id}";
            //    $"{filterPrefix}Id == {id.Value} ";
            //query += CompanyFilter ? " && " : "" + CompanyContextFilter;
            //query += string.IsNullOrWhiteSpace(filter) ? "" : " && " + filter;

            var response = await _service.GetAsync<TEntity>(query, id.Value, BearerToken);


            if (response == null)
            {
                return NotFound();
            }

            return View(model: response);
        }


        
        // PUT: TEntity/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "SystemAdmin, Developer, Owner, Update")]
        public virtual async Task<IActionResult> Edit(int id, TEntity entity)
        {
            if (ModelState.IsValid)
            {
                var response = await Put(id, entity);

                return RedirectToAction("Index");
            }

            return View(entity);
        }

        // GET: TEntity/Delete/5
        [Authorize(Roles = "SystemAdmin, Developer, Owner, Delete")]
        public virtual async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var query = $"{ _resourceUrl}/{id}";
            //    $"{filterPrefix}Id == {id.Value} ";
            //query += CompanyFilter ? " && " : "" + CompanyContextFilter;
            //query += string.IsNullOrWhiteSpace(filter) ? "" : " && " + filter;

            var response = await _service.GetAsync<TEntity>(query, id.Value, BearerToken);


            if (response == null)
            {
                return NotFound();
            }

            return View(model: response);
        }

        // POST: TEntity/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "SystemAdmin, Developer, Owner, Delete")]
        public virtual async Task<IActionResult> DeleteConfirmed(int id)
        {
            var response = await _service.DeleteJsonAsync<TEntity>(
                _resourceUrl + "/" + id, BearerToken);
            return RedirectToAction("Index");
        }

    }
}