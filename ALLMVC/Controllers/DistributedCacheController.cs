using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ALLMVC.Data;
using ALLMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace ALLMVC.Controllers
{
    public class DistributedCacheController : Controller
    {
        private readonly IDistributedCache _distributedCache;
        private readonly ApplicationDbContext _context;

        public DistributedCacheController(IDistributedCache distributedCache, ApplicationDbContext context)
        {
            _distributedCache = distributedCache;
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> RedisCache()
        {
            IList<Employee> data = new List<Employee>();

            var isCacheString = _distributedCache.GetString("Employee");
            if (string.IsNullOrEmpty(isCacheString))
            {
                data = await _context.Employee.ToListAsync();
                var dataString = JsonConvert.SerializeObject(data);

                var distributedCacheOptions = new DistributedCacheEntryOptions();
                distributedCacheOptions.SetSlidingExpiration(TimeSpan.FromSeconds(60));

                await _distributedCache.SetStringAsync("Employee", dataString, distributedCacheOptions);
            }
            else
            {

                data = JsonConvert.DeserializeObject<IList<Employee>>(isCacheString);
            }

            return View(data);
        }
    }
}

