using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ALLMVC.Data;
using ALLMVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ALLMVC.Controllers
{
    [Authorize]

    public class ResponseCachingController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ResponseCachingController(ApplicationDbContext context)
        {
            _context = context;
        }
        //public async Task<IActionResult> ResponseCachingDemo()
        //{
        //    return View("ResponseCachingDemo", await _context.Employee.ToListAsync());
        //}
        [ResponseCache(Duration = 20)]

        public IActionResult Index()
        {
            return View(new CacheDemo
            {
                LastUpdated = DateTime.Now
            });
        }

    }
}
