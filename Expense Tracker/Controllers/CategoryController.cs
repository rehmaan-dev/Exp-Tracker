using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Expense_Tracker.Models;

namespace Expense_Tracker.Controllers
{
    /// <summary>
    /// This controller handles CRUD operations for the Category entity.
    /// It provides endpoints for listing categories, adding or editing a category,
    /// and deleting a category.
    /// </summary>
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CategoryController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Category
        // Retrieves and displays a list of all categories.
        public async Task<IActionResult> Index()
        {
            // If the Categories entity set is not null, return the view with the list of categories.
            // Otherwise, return a problem message indicating the Categories entity set is null.
            return _context.Categories != null ?
                        View(await _context.Categories.ToListAsync()) :
                        Problem("Entity set 'ApplicationDbContext.Categories' is null.");
        }

        // GET: Category/AddOrEdit
        // Displays the Add or Edit form for a category.
        // If the id is 0, it means a new category is being created.
        // Otherwise, it means an existing category is being edited.
        public IActionResult AddOrEdit(int id = 0)
        {
            if (id == 0)
                return View(new Category()); // Return a view with a new Category object for creation.
            else
                return View(_context.Categories.Find(id)); // Return a view with the existing Category object for editing.
        }

        // POST: Category/AddOrEdit
        // Handles the form submission for adding or editing a category.
        // Validates the model and saves the changes to the database.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit([Bind("CategoryId,Title,Icon,Type")] Category category)
        {
            if (ModelState.IsValid)
            {
                if (category.CategoryId == 0)
                    _context.Add(category); // If the CategoryId is 0, add a new category.
                else
                    _context.Update(category); // Otherwise, update the existing category.
                await _context.SaveChangesAsync(); // Save the changes to the database.
                return RedirectToAction(nameof(Index)); // Redirect to the Index action.
            }
            return View(category); // If the model is not valid, return the view with the model to display validation errors.
        }

        // POST: Category/Delete/5
        // Handles the deletion of a category.
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Categories == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Categories' is null.");
            }
            var category = await _context.Categories.FindAsync(id);
            if (category != null)
            {
                _context.Categories.Remove(category); // Remove the category from the context.
            }

            await _context.SaveChangesAsync(); // Save the changes to the database.
            return RedirectToAction(nameof(Index)); // Redirect to the Index action.
        }
    }
}
