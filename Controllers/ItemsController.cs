using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AOWebApp.Data;
using AOWebApp.Models;
using AOWebApp.ViewModels;

namespace AOWebApp.Controllers
{
    public class ItemsController : Controller
    {
        private readonly AmazonOrders2025Context _context;

        public ItemsController(AmazonOrders2025Context context)
        {
            _context = context;
        }

        // GET: Items
        public async Task<IActionResult> Index(string searchTerm, int? categoryId)
        {
            ItemSearchViewModel searchViewModel = new ItemSearchViewModel();

            #region CategoriesQuery
            var categories = _context.ItemCategories
                .Where(c => c.ParentCategoryId != null)
                .OrderBy(c => c.CategoryName)
                .Select(c => new {c.CategoryId, c.CategoryName})
                .ToList();

            searchViewModel.CategoryList = new SelectList(categories, "CategoryId", "CategoryName");

            #endregion

            #region AvgRatings

            /*var itemRatings = _context.Items
                .Include(r => r.Reviews)
                .Select(r => new ItemRatingsViewModel 
                {
                    ItemId = r.ItemId, 
                    AvgRating = r.Reviews.Any() ? r.Reviews.Average(re => re.Rating) : 0.0
                })
                .ToList();

            searchViewModel.ItemRatings = itemRatings;*/

            #endregion

            #region ItemQuery
            var amazonOrdersContext = _context.Items
                .Include(i => i.Category)
                .Include(i => i.Reviews)
                .OrderBy(i => i.ItemName)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                amazonOrdersContext = amazonOrdersContext.Where(i =>
                    i.ItemName.Contains(searchTerm));
            }
            if (categoryId != null)
            {
                amazonOrdersContext = amazonOrdersContext.Where(c => c.Category.CategoryId == categoryId);
            }

            var itemRatings = amazonOrdersContext
                .Select(r => new ItemRatingsViewModel
                {
                    ItemObj = r,
                    RatingCount = r.Reviews.Any() ? r.Reviews.Count() : 0,
                    AvgRating = r.Reviews.Any() ? r.Reviews.Average(re => re.Rating) : 0
                })
                .ToListAsync();

            searchViewModel.ItemRatings = await itemRatings;

            #endregion

            return View(searchViewModel);
        }

        // GET: Items/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.Items
                .Include(i => i.Category)
                .FirstOrDefaultAsync(m => m.ItemId == id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // GET: Items/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.ItemCategories, "CategoryId", "CategoryId");
            return View();
        }

        // POST: Items/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ItemId,ItemName,ItemDescription,ItemCost,ItemImage,CategoryId")] Item item)
        {
            if (ModelState.IsValid)
            {
                _context.Add(item);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.ItemCategories, "CategoryId", "CategoryId", item.CategoryId);
            return View(item);
        }

        // GET: Items/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.Items.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.ItemCategories, "CategoryId", "CategoryId", item.CategoryId);
            return View(item);
        }

        // POST: Items/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ItemId,ItemName,ItemDescription,ItemCost,ItemImage,CategoryId")] Item item)
        {
            if (id != item.ItemId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(item);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ItemExists(item.ItemId))
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
            ViewData["CategoryId"] = new SelectList(_context.ItemCategories, "CategoryId", "CategoryId", item.CategoryId);
            return View(item);
        }

        // GET: Items/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.Items
                .Include(i => i.Category)
                .FirstOrDefaultAsync(m => m.ItemId == id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // POST: Items/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var item = await _context.Items.FindAsync(id);
            if (item != null)
            {
                _context.Items.Remove(item);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ItemExists(int id)
        {
            return _context.Items.Any(e => e.ItemId == id);
        }
    }
}
