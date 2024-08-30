using Expense_Tracker.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Expense_Tracker.Controllers
{
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DashboardController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Dashboard
        // This action retrieves and calculates data for the dashboard view.
        public async Task<ActionResult> Index()
        {
            // Define the start and end dates for the last 7 days.
            DateTime StartDate = DateTime.Today.AddDays(-6);
            DateTime EndDate = DateTime.Today;

            // Retrieve transactions from the last 7 days and include the associated categories.
            List<Transaction> SelectedTransactions = await _context.Transactions
                .Include(x => x.Category)
                .Where(y => y.Date >= StartDate && y.Date <= EndDate)
                .ToListAsync();

            // Calculate total income from the selected transactions.
            int TotalIncome = SelectedTransactions
                .Where(i => i.Category.Type == "Income")
                .Sum(j => j.Amount);
            ViewBag.TotalIncome = "₹" + TotalIncome.ToString("N0"); // Format with rupee sign

            // Calculate total expenses from the selected transactions.
            int TotalExpense = SelectedTransactions
                .Where(i => i.Category.Type == "Expense")
                .Sum(j => j.Amount);
            ViewBag.TotalExpense = "₹" + TotalExpense.ToString("N0"); // Format with rupee sign

            // Calculate the balance as total income minus total expenses.
            int Balance = TotalIncome - TotalExpense;
            ViewBag.Balance = "₹" + Balance.ToString("N0"); // Format with rupee sign

            // Prepare data for the doughnut chart showing expenses by category.
            ViewBag.DoughnutChartData = SelectedTransactions
                .Where(i => i.Category.Type == "Expense")
                .GroupBy(j => j.Category.CategoryId)
                .Select(k => new
                {
                    categoryTitleWithIcon = k.First().Category.Icon + " " + k.First().Category.Title,
                    amount = k.Sum(j => j.Amount),
                    formattedAmount = "₹" + k.Sum(j => j.Amount).ToString("N0") // Format with rupee sign
                })
                .OrderByDescending(l => l.amount)
                .ToList();

            // Prepare data for the spline chart showing income and expenses over the last 7 days.

            // Group transactions by date and calculate daily income.
            List<SplineChartData> IncomeSummary = SelectedTransactions
                .Where(i => i.Category.Type == "Income")
                .GroupBy(j => j.Date)
                .Select(k => new SplineChartData()
                {
                    day = k.First().Date.ToString("dd-MMM"),
                    income = k.Sum(l => l.Amount)
                })
                .ToList();

            // Group transactions by date and calculate daily expenses.
            List<SplineChartData> ExpenseSummary = SelectedTransactions
                .Where(i => i.Category.Type == "Expense")
                .GroupBy(j => j.Date)
                .Select(k => new SplineChartData()
                {
                    day = k.First().Date.ToString("dd-MMM"),
                    expense = k.Sum(l => l.Amount)
                })
                .ToList();

            // Generate a list of the last 7 days.
            string[] Last7Days = Enumerable.Range(0, 7)
                .Select(i => StartDate.AddDays(i).ToString("dd-MMM"))
                .ToArray();

            // Combine income and expense data for each day in the last 7 days.
            ViewBag.SplineChartData = from day in Last7Days
                                      join income in IncomeSummary on day equals income.day into dayIncomeJoined
                                      from income in dayIncomeJoined.DefaultIfEmpty()
                                      join expense in ExpenseSummary on day equals expense.day into expenseJoined
                                      from expense in expenseJoined.DefaultIfEmpty()
                                      select new
                                      {
                                          day = day,
                                          income = income == null ? 0 : income.income,
                                          expense = expense == null ? 0 : expense.expense,
                                      };

            // Retrieve the most recent 5 transactions for display.
            ViewBag.RecentTransactions = await _context.Transactions
                .Include(i => i.Category)
                .OrderByDescending(j => j.Date)
                .Take(5)
                .ToListAsync();

            return View();
        }
    }


    // Class for storing income and expense data for the spline chart.
    public class SplineChartData
    {
        public string day;
        public int income;
        public int expense;
    }
}
