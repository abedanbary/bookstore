using System;
using idefny.Models;

namespace idefny.ViewModels
{
    public class BookDetailsViewModel
    {
        public Book Book { get; set; }
        public double raebook { get; set; }
        public List<Review> Reviews { get; set; }
        public Review NewReview { get; set; } // For submitting a new review
    }


}

