using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;

namespace Expense_Tracker.Models
{
    // Represents a financial transaction
    public class Transaction
    {
        // Primary key for the Transaction table
        [Key]
        public int TransactionId { get; set; }

        // Foreign key to the Category table, with a validation to ensure a category is selected
        [Range(1, int.MaxValue, ErrorMessage = "Please select a category.")]
        public int CategoryId { get; set; }

        // Navigation property to the Category entity
        public Category? Category { get; set; }

        // Amount of the transaction, with a validation to ensure it is greater than 0
        [Range(1, int.MaxValue, ErrorMessage = "Amount should be greater than 0.")]
        public int Amount { get; set; }

        // Optional note for the transaction, with a maximum length of 75 characters
        [Column(TypeName = "nvarchar(75)")]
        public string? Note { get; set; }

        // Date of the transaction, defaulting to the current date and time
        public DateTime Date { get; set; } = DateTime.Now;

        // Not mapped to the database, combines the category's icon and title for display
        [NotMapped]
        public string? CategoryTitleWithIcon
        {
            get
            {
                return Category == null ? "" : Category.Icon + " " + Category.Title;
            }
        }

        // Not mapped to the database, formats the amount with the appropriate currency symbol and sign
        [NotMapped]
        public string? FormattedAmount
        {
            get
            {
                var cultureInfo = new CultureInfo("en-IN"); // Use Indian culture for currency formatting
                string currencySymbol = cultureInfo.NumberFormat.CurrencySymbol;

                return ((Category == null || Category.Type == "Expense") ? "- " : "+ ") + Amount.ToString("C0", cultureInfo);
            }
        }
    }
}
