using idefny.Data;
using idefny.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class ReviewController : Controller
{
    private readonly AppDbContext _context;
    private readonly UserManager<Users> _userManager;

    public ReviewController(AppDbContext context, UserManager<Users> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    [HttpPost]
    public async Task<IActionResult> SubmitReview(int BookId, int Rating, string Comment)
    {
        var currentUserId = _userManager.GetUserId(User);
        if (ModelState.IsValid)
        {
          

            if (string.IsNullOrEmpty(currentUserId))
            {
                // User is not logged in
                TempData["ErrorMessage"] = "You must be logged in to review a book.";
                return RedirectToAction("Details", "Store", new { id = BookId });
            }

            // Check if the user has purchased this book
            var hasPurchasedBook = await _context.UserBooks
                .AnyAsync(ub => ub.UserId == currentUserId && ub.BookId == BookId);
            var hasBorrowedBook = await _context.BorrowedBooks
           .AnyAsync(ub => ub.UserId == currentUserId && ub.BookId == BookId);

            if (!hasPurchasedBook && !hasBorrowedBook)
            {
                // User has not purchased the book
                TempData["ErrorMessage"] = "You must purchase  or Borrow the book before reviewing it.";
                return RedirectToAction("Details", "Store", new { id = BookId });
            }
            var userId = User.Identity.Name;
            // Create and save the review
            var review = new Review
            {
                BookId = BookId,
                UserId = userId,
                Rating = Rating,
                Comment = Comment,
                CreatedAt = DateTime.UtcNow
            };

            _context.Reviews.Add(review);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Review submitted successfully!";
            return RedirectToAction("Details", "Store", new { id = BookId });
        }

        // If ModelState is not valid, redirect to the error page
        TempData["ErrorMessage"] = "Invalid form submission.";
        return RedirectToAction("Error");
    }


}