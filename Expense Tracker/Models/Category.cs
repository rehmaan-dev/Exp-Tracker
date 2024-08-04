using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Expense_Tracker.Models
{
    // Represents a category of transactions
    public class Category
    {
        // Primary key for the Category table
        [Key]
        public int CategoryId { get; set; }

        // Title of the category with a maximum length of 50 characters
        [Column(TypeName = "nvarchar(50)")]
        [Required(ErrorMessage = "Title is required.")]
        public string Title { get; set; }

        // Icon representing the category with a maximum length of 5 characters
        [Column(TypeName = "nvarchar(5)")]
        public string Icon { get; set; } = "";

        // Type of the category (e.g., Expense or Income) with a maximum length of 10 characters
        [Column(TypeName = "nvarchar(10)")]
        public string Type { get; set; } = "Expense";

        // Not mapped to the database, combines Icon and Title properties for display
        [NotMapped]
        public string? TitleWithIcon
        {
            get
            {
                return this.Icon + " " + this.Title;
            }
        }
    }
}
