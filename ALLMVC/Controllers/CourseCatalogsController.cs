using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ALLMVC.Data;
using ALLMVC.Models;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace ALLMVC.Controllers
{
    public class CourseCatalogsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CourseCatalogsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CourseCatalogs
        public async Task<IActionResult> Index()
        {
            return View(await _context.CourseCatalog.ToListAsync());
        }

        // GET: CourseCatalogs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var courseCatalog = await _context.CourseCatalog
                .FirstOrDefaultAsync(m => m.Id == id);
            if (courseCatalog == null)
            {
                return NotFound();
            }

            return View(courseCatalog);
        }

        // GET: CourseCatalogs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CourseCatalogs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CourseName,Cost,FileName")] CourseCatalog courseCatalog, IFormFile FileName)
        {
            string path = Environment.CurrentDirectory;
            string fullName = Path.Combine(path, "wwwroot", "Images", FileName.FileName);

            courseCatalog.FileName = fullName;
            _context.Add(courseCatalog);

            await _context.SaveChangesAsync();

            // upload file
            if (FileName.Length > 0)
            {
                using (var stream = System.IO.File.Create(fullName))
                {
                    await FileName.CopyToAsync(stream);
                }
                return RedirectToAction(nameof(Index));

            }

            return View(courseCatalog);
        }

        // GET: CourseCatalogs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var courseCatalog = await _context.CourseCatalog.FindAsync(id);
            if (courseCatalog == null)
            {
                return NotFound();
            }
            return View(courseCatalog);
        }

        // POST: CourseCatalogs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CourseName,Cost,FileName")] CourseCatalog courseCatalog)
        {
            if (id != courseCatalog.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(courseCatalog);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CourseCatalogExists(courseCatalog.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(courseCatalog);
        }

        // GET: CourseCatalogs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var courseCatalog = await _context.CourseCatalog
                .FirstOrDefaultAsync(m => m.Id == id);
            if (courseCatalog == null)
            {
                return NotFound();
            }

            return View(courseCatalog);
        }

        // POST: CourseCatalogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var courseCatalog = await _context.CourseCatalog.FindAsync(id);
            _context.CourseCatalog.Remove(courseCatalog);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CourseCatalogExists(int id)
        {
            return _context.CourseCatalog.Any(e => e.Id == id);
        }
    }
}
