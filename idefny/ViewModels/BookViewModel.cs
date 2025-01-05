using System.ComponentModel.DataAnnotations;

namespace idefny.ViewModels
{
    public class BookViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public int Age { get; set; }
        public string Genre { get; set; }
        public int NumberOfCopies { get; set; }
        public DateTime DatePublished { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; } // Path or URL to the book's cover image
        public int AvailableForBorrow { get; set; }  // Default to 3 copies available for borrowing


      
    
        public bool IsAvailableForBorrow { get; set; }
        public decimal? DiscountPercentage2 { get; set; } // نسبة الخصم (على سبيل المثال، 10% تعني 10)

        public DateTime? DiscountStartDate { get; set; } // تاريخ بدء الخصم
        public DateTime? DiscountEndDate { get; set; }
        public decimal DiscountedPrice { set; get; }
        public double raebook { get; set; }

        // Formatted price to display as currency
        public string FormattedPrice => Price.ToString("C");

        // Formatted publication date (e.g., "January 01, 2023")
        public string FormattedDate => DatePublished.ToString("MMMM dd, yyyy");
    }
}
