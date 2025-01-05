
    using System;
    using System.ComponentModel.DataAnnotations;

    namespace idefny.Models
    {
        public class Book
        {
            [Key]
            public int Id { get; set; } // Primary Key

            [Required]
            [StringLength(100)]
            public string Name { get; set; } // Book Name

            [Required]
            [StringLength(100)]
            public string Author { get; set; } // Author Name

            [Required]
            [Range(0, 150)]
            public int Age { get; set; } // Target Audience Age

            [Required]
            [StringLength(50)]
            public string Genre { get; set; } // Book Genre

            [Required]
            [Range(0, int.MaxValue)]
            public int NumberOfCopies { get; set; } // Total Copies Available

            // New property for Available Copies for Borrowing
            [Required]
            [Range(0, 3)] // You can only borrow up to 3 copies by default
            public int AvailableForBorrow { get; set; } = 3; // Default to 3 copies available for borrowing

            // New property to mark whether a book is available for borrowing
            [Required]
            public bool IsAvailableForBorrow { get; set; } = true; // Default to true (book available for borrowing)

            [Required]
            public DateTime DatePublished { get; set; } // Publication Date

            [Required]
            [Range(0.01, double.MaxValue)]
            public decimal Price { get; set; } // Price of the Book

            public string? ImageUrl { get; set; }
            public string? PdfUrl { get; set; }

        [Range(0, 100)]
        public decimal? DiscountPercentage2 { get; set; } // نسبة الخصم (على سبيل المثال، 10% تعني 10)

        public DateTime? DiscountStartDate { get; set; } // تاريخ بدء الخصم
        public DateTime? DiscountEndDate { get; set; } // تاريخ انتهاء الخصم

        public decimal DiscountedPrice
        {
            get
            {
                // إذا كان هناك خصم نشط ضمن الفترة الزمنية المحددة
                if (DiscountPercentage2.HasValue && DiscountStartDate.HasValue && DiscountEndDate.HasValue)
                {
                    if (DateTime.Now >= DiscountStartDate.Value && DateTime.Now <= DiscountEndDate.Value)
                    {
                        return Price - (Price * DiscountPercentage2.Value / 100);
                    }
                }

                // إذا لم يكن هناك خصم، يعاد السعر الأصلي
                return Price;
            }
        }



        public void BorrowBook()
        {
            if (AvailableForBorrow > 0)
            {
                AvailableForBorrow--;
            }
        }

        // Method to increase available copies when returning
        public void ReturnBook()
        {
            if (AvailableForBorrow < NumberOfCopies)
            {
                AvailableForBorrow++;
            }
        }
    }


    }

