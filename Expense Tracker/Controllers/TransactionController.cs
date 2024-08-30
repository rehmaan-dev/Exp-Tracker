using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Expense_Tracker.Models;

namespace Expense_Tracker.Controllers
{
    public class TransactionController : Controller
    {
        private readonly ApplicationDbContext _context;

        // Constructor to inject the ApplicationDbContext dependency
        public TransactionController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Transaction
        // This action retrieves all transactions, including their related categories, and returns them to the view.
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Transactions.Include(t => t.Category);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Transaction/AddOrEdit
        // This action returns the AddOrEdit view. If the id is 0, it creates a new Transaction object.
        // Otherwise, it retrieves the existing Transaction object from the database.
        public IActionResult AddOrEdit(int id = 0)
        {
            PopulateCategories(); // Populate the categories for the dropdown list
            if (id == 0)
                return View(new Transaction());
            else
                return View(_context.Transactions.Find(id));
        }

        // POST: Transaction/AddOrEdit
        // This action handles the form submission for adding or editing a transaction.
        // It validates the model and either adds a new transaction or updates an existing one.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit([Bind("TransactionId,CategoryId,Amount,Note,Date")] Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                if (transaction.TransactionId == 0)
                    _context.Add(transaction); // Add a new transaction
                else
                    _context.Update(transaction); // Update an existing transaction
                await _context.SaveChangesAsync(); // Save changes to the database
                return RedirectToAction(nameof(Index));
            }
            PopulateCategories(); // Repopulate the categories if the model state is invalid
            return View(transaction);
        }

        // POST: Transaction/Delete/5
        // This action handles the form submission for deleting a transaction.
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Transactions == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Transactions'  is null.");
            }
            var transaction = await _context.Transactions.FindAsync(id);
            if (transaction != null)
            {
                _context.Transactions.Remove(transaction); // Remove the transaction from the database
            }

            await _context.SaveChangesAsync(); // Save changes to the database
            return RedirectToAction(nameof(Index));
        }

        // This method populates the ViewBag with the list of categories for use in the dropdown list.
        [NonAction]
        public void PopulateCategories()
        {
            var CategoryCollection = _context.Categories.ToList();
            Category DefaultCategory = new Category() { CategoryId = 0, Title = "Choose a Category" };
            CategoryCollection.Insert(0, DefaultCategory); // Insert a default category at the beginning of the list
            ViewBag.Categories = CategoryCollection; // Set the ViewBag property
        }
    }
}
