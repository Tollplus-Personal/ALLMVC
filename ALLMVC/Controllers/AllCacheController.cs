using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace ALLMVC.Controllers
{
    public class AllCacheController : Controller
    {
        private readonly IMemoryCache _cache;
        string key = "IDGKey";
        string obj;

        public AllCacheController(IMemoryCache Cache)
        {
            _cache = Cache;
        }


        [Route("AllCache/SetCacheData")]
        public IActionResult SetCacheData()
        {
            var Time = DateTime.Now.ToLocalTime().ToString();
            MemoryCacheEntryOptions options = new MemoryCacheEntryOptions()
            {
                AbsoluteExpirationRelativeToNow = (DateTime.Now.AddSeconds(30) - DateTime.Now)
            };

            _cache.Set("Time", Time, options);
            return View();
        }

        [Route("AllCache/GetCacheData")]
        public IActionResult GetCacheData()
        {
            string Time = string.Empty;

            if (!_cache.TryGetValue("Time", out Time))
            {
                Time = "Cache is Expired or not availabe";
            }

            ViewBag.date = Time;
            return View();
        }
    }
}
