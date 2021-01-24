using IPTVSchedulerQuartz.API;
using IPTVSchedulerQuartz.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace IPTVSchedulerQuartz.Controllers
{
    public class HomeController : Controller
    {
        private IConfiguration configuration;
        private readonly RestApiService _service;
        public HomeController(IConfiguration configuration, RestApiService service)
        {
            this.configuration = configuration;
            this._service = service;
        }

        [HttpGet]
        public ActionResult Index()
        {
            ViewData["config"] = configuration["BgHostedConfig:Actived"];

            if(TempData["list"] != null) return View(TempData["list"] as ListIptv);

            return View();

        }
        [HttpGet]
        public ActionResult Disabled(bool? actived)
        {
            configuration["BgHostedConfig:Actived"] = actived.ToString();
            return RedirectToAction(nameof(Index));
        }

        // GET: HomeController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: HomeController/Create
        public ActionResult Create()
        {
            return View();
        }

        public async Task<ActionResult> GetList()
        {
            var list = await this._service.GetListIPTV();
            TempData["list"] = list;
            return RedirectToAction(nameof(Index));
        }

        // POST: HomeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: HomeController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: HomeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: HomeController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: HomeController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
