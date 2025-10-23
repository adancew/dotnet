using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using lista10.Data;
using lista10.Models;
using lista10.ViewModels;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System.Drawing.Printing;

namespace lista10.Controllers
{
    public class ArticlesController : Controller
    {
        private readonly ShopDbContext _context;
        private IWebHostEnvironment _hostEnvironment;

        public ArticlesController(ShopDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _hostEnvironment = webHostEnvironment;  
            
        }

        // GET: Articles
        public async Task<IActionResult> Index()
        {
            var shopDbContext = _context.Articles.Include(a => a.Category);
            return View(await shopDbContext.ToListAsync());
        }

        // GET: Articles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var article = await _context.Articles
                .Include(a => a.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (article == null)
            {
                return NotFound();
            }

            return View(article);
        }

        // GET: Articles/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName");
            return View();
        }

        // POST: Students/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("Id,Name,Price,CategoryId,FormFile")] ArticleCreateViewModel articleView)
        {

            if (ModelState.IsValid)
            {
                string upload_folder = Path.Combine(_hostEnvironment.WebRootPath, "upload");
                string filename = Path.Combine(upload_folder, "no_image.jpg");

                if (!Directory.Exists(upload_folder))
                {
                    Directory.CreateDirectory(upload_folder);
                }

                if (articleView.FormFile != null)
                {
                    // save image to disc
                    filename = Path.Combine(
                       upload_folder,
                       Guid.NewGuid().ToString() +
                       Path.GetExtension(articleView.FormFile.FileName));

                    
                    using (Stream fileStream = new FileStream(filename, FileMode.Create))
                    {
                        articleView.FormFile.CopyTo(fileStream);
                    }

                }

                // save article to database
                Article article = new Article()
                {
                    Id = articleView.Id,
                    Name = articleView.Name,
                    Price = articleView.Price,
                    CategoryId = articleView.CategoryId,
                    FormFile = articleView.FormFile,
                   
                };
                _context.Add(article);
                await _context.SaveChangesAsync();
                
                
                return RedirectToAction(nameof(Index));
            }
            return View(articleView);
        }


        // POST: Articles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        /*
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Price,CategoryId,Picture")] Article article)
        {
            if (ModelState.IsValid)
            {
                _context.Add(article);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName", article.CategoryId);
            return View(article);
        }
        */

        // GET: Articles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var article = await _context.Articles.FindAsync(id);
            if (article == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName", article.CategoryId);
            return View(article);
        }

        // POST: Articles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Price,CategoryId,Picture")] Article article)
        {
            if (id != article.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(article);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ArticleExists(article.Id))
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
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName", article.CategoryId);
            return View(article);
        }

        // GET: Articles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var article = await _context.Articles
                .Include(a => a.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (article == null)
            {
                return NotFound();
            }

            return View(article);
        }

        // POST: Articles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var article = await _context.Articles.FindAsync(id);
            if (article != null)
            {
                if (article.FormFile != null && System.IO.File.Exists(
                    Path.Combine(_hostEnvironment.WebRootPath, article.FormFile.FileName)))
                {

                    System.IO.File.Delete(
                       Path.Combine(_hostEnvironment.WebRootPath, article.FormFile.FileName)
                    );
                }
                
                _context.Articles.Remove(article);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ArticleExists(int id)
        {
            return _context.Articles.Any(e => e.Id == id);
        }
    }
}
