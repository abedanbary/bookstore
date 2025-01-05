using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using idefny.Models;
using idefny.Data;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity;

namespace idefny.Controllers
{
    public class CartController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ILogger<CartController> _logger;
        private readonly UserManager<Users> _userManager;

        public CartController(AppDbContext context, ILogger<CartController> logger, UserManager<Users> userManager)
        {
            _userManager = userManager;
            _context = context;
            _logger = logger;
        }

        // GET: View Cart
        public async Task<IActionResult> ViewCart()
        {
            // Get the current authenticated user's ID
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Login", "Account");  // Redirect to login if user is not authenticated
            }

            var userId = user.Id;  // UserId to fetch cart

            // Check if cart exists for this user
            var cart = await _context.Carts.Include(c => c.CartItems).ThenInclude(ci => ci.Book)
                                            .FirstOrDefaultAsync(c => c.UserId == userId);

            if (cart == null)
            {
                // Optionally, handle this case if cart doesn't exist yet
                cart = new Cart { UserId = userId, CartItems = new List<CartItem>() };
                _context.Carts.Add(cart);
                await _context.SaveChangesAsync();  // Save new cart if not found
            }

            return View(cart);  // Pass the cart model to the view
        }

        [HttpPost]
        [HttpPost]
        public async Task<IActionResult> AddToCart(int bookId, bool IsBuying2)
        {
            // Fetch the book from the database
            var book = await _context.Books.FindAsync(bookId);
            if (book == null)
            {
                return Json(new { success = false, message = "Book not found." });
            }

            // Ensure the user is authenticated
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Json(new { success = false, message = "You must be logged in to add items to the cart." });
            }

            var userId = user.Id;

            // Check if the user has already borrowed the book
            var hasBorrowedBook = await _context.BorrowedBooks
                                                .AnyAsync(b => b.BookId == bookId && b.UserId == userId);
            var hasBook = await _context.UserBooks
                                               .AnyAsync(b => b.BookId == bookId && b.UserId == userId);
            if (hasBorrowedBook || hasBook)
            {
                return Json(new { success = false, message = "You already have this book borrowed." });
            }

            // Check if the cart exists for the user
            var cart = await _context.Carts.Include(c => c.CartItems)
                                           .FirstOrDefaultAsync(c => c.UserId == userId);

            if (cart == null)
            {
                // Create a new cart if one doesn't exist
                cart = new Cart
                {
                    UserId = userId,
                    CartItems = new List<CartItem>()
                };
                _context.Carts.Add(cart);
                await _context.SaveChangesAsync();
            }

            // Check if the book is already in the cart
            var cartItem = cart.CartItems.FirstOrDefault(ci => ci.BookId == bookId);
            if (cartItem == null)
            {
                // Add the book to the cart
                cartItem = new CartItem
                {
                    BookId = bookId,
                    Quantity = 1,  // Default quantity is 1
                    Price = book.DiscountedPrice,
                    CartId = cart.Id,
                    IsBuying2 = IsBuying2  // True for Buying, False for Borrowing
                };
                cart.CartItems.Add(cartItem);
            }
            else
            {
                // If the book is already in the cart, increase the quantity
                cartItem.Quantity++;
            }

            // Save changes to the database
            await _context.SaveChangesAsync();

            // Redirect to the ViewCart action
            return RedirectToAction("ViewCart", "Cart");
        }





        // POST: Update Item Quantity in Cart
        [HttpPost]
        public async Task<IActionResult> UpdateQuantity(int cartItemId, int quantity)
        {
            var cartItem = await _context.CartItems.FindAsync(cartItemId);

            if (cartItem == null)
            {
                return Json(new { success = false, message = "Item not found in cart." });
            }

            // Update quantity
            cartItem.Quantity = quantity;
            await _context.SaveChangesAsync();

            return Json(new { success = true, message = "Cart updated." });
        }

        // POST: Remove Item from Cart
        [HttpPost]
        public async Task<IActionResult> RemoveFromCart(int cartItemId)
        {
            var cartItem = await _context.CartItems.FindAsync(cartItemId);

            if (cartItem == null)
            {
                return Json(new { success = false, message = "Item not found in cart." });
            }

            _context.CartItems.Remove(cartItem);
            await _context.SaveChangesAsync();

            return Json(new { success = true, message = "Item removed from cart." });
        }

     
        [HttpPost]
        public async Task<IActionResult> CheckoutCart()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                // If the user is not authenticated, redirect to the login page
                return RedirectToAction("Login", "Account");
            }

            var userId = user.Id;

            // Get the user's cart, including related books and cart items
            var cart = await _context.Carts.Include(c => c.CartItems).ThenInclude(ci => ci.Book)
                                           .FirstOrDefaultAsync(c => c.UserId == userId);

            if (cart == null || !cart.CartItems.Any())
            {
                // If the cart is empty, redirect back to the cart view
                return RedirectToAction("ViewCart", "Cart");
            }

            // Separate the items into buying and borrowing categories
            var itemsToBuy = cart.CartItems.Where(ci => ci.IsBuying2 == true).ToList();
            var itemsToBorrow = cart.CartItems.Where(ci => ci.IsBuying2 == false).ToList();

            // Process buying items
            foreach (var cartItem in itemsToBuy)
            {
                // Add each book to the UserBooks table (for buying)
                var userBook = new UserBook
                {
                    UserId = userId,
                    BookId = cartItem.BookId,
                    PurchaseDate = DateTime.UtcNow,
                    // Additional properties like price can be added here if needed
                };

                _context.UserBooks.Add(userBook);

                // Optionally, decrement the stock of the purchased books
                var book = await _context.Books.FindAsync(cartItem.BookId);
                if (book != null)
                {
                    book.NumberOfCopies -= cartItem.Quantity; // Decrease stock based on quantity in cart
                }
            }

            // Process borrowing items
            foreach (var cartItem in itemsToBorrow)
            {
                if (cartItem.Book.AvailableForBorrow == 1)
                {
                    var borrowedBook1 = new BorrowedBook
                    {
                        UserId = userId,
                        BookId = cartItem.BookId,
                        BorrowDate = DateTime.UtcNow,
                        DaysToBorrow = 14, // Example: Borrowing period of 14 days
                        ReturnDate2 = DateTime.UtcNow.AddDays(30)
                    };
                    var book1 = await _context.Books.FindAsync(cartItem.BookId);
                    if (book1 != null)
                    {
                        book1.AvailableForBorrow--;
                    }

                }
                else
                {
                    // Create a new BorrowedBook entry for each borrowing item
                    var borrowedBook = new BorrowedBook
                    {
                        UserId = userId,
                        BookId = cartItem.BookId,
                        BorrowDate = DateTime.UtcNow,
                        DaysToBorrow = 14, // Example: Borrowing period of 14 days
                        ReturnDate2 = DateTime.UtcNow.AddMinutes(20)
                    };

                    // Add the borrowed book to the database
                    _context.BorrowedBooks.Add(borrowedBook);

                    // Update the book's availability (decrease the number of available copies)
                    var book = await _context.Books.FindAsync(cartItem.BookId);
                    if (book != null)
                    {
                        book.AvailableForBorrow--;
                    }
                }
            }

            // Remove the processed items (both bought and borrowed) from the cart
            _context.CartItems.RemoveRange(itemsToBuy);
            _context.CartItems.RemoveRange(itemsToBorrow);

            // Save all changes to the database
            await _context.SaveChangesAsync();

            // Redirect to a confirmation page or the cart view
            return RedirectToAction("ViewCart", "Cart");
        }




    }
}

