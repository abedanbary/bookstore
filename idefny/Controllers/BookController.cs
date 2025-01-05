using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using idefny.Data;
using idefny.Models;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.IO;

public class BookController : Controller
{
    private readonly AppDbContext _context;
    private readonly IWebHostEnvironment _hostEnvironment;

    public BookController(AppDbContext context, IWebHostEnvironment hostEnvironment)
    {
        _context = context;
        _hostEnvironment = hostEnvironment;

    }

    // GET: Book
    public async Task<IActionResult> Index()
    {
        var books = await _context.Books.ToListAsync();

        if (books == null || !books.Any())
        {
            ViewBag.Message = "No books available.";
        }

        return View(books);
    }

    // GET: Book/Details/5
    public async Task<IActionResult> Details(int id)
    {
        var book = await _context.Books.FindAsync(id);
        if (book == null)
        {
            return NotFound();
        }
        return View(book);
    }

    // GET: Book/Create
 
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Name,Author,Age,Genre,NumberOfCopies,DatePublished,Price,IsAvailableForBorrow")] Book book, IFormFile ImageFile, IFormFile PdfFile)
    {
        if (ModelState.IsValid)
        {
            // Handle Image upload
            if (ImageFile != null && ImageFile.Length > 0)
            {
                var imageFileName = Path.GetFileName(ImageFile.FileName);
                var imageFilePath = Path.Combine(_hostEnvironment.WebRootPath, "images", imageFileName);

                // Save the image in wwwroot/images
                using (var stream = new FileStream(imageFilePath, FileMode.Create))
                {
                    await ImageFile.CopyToAsync(stream);
                }

                // Set the image URL for the book
                book.ImageUrl = "/images/" + imageFileName;
            }
            else
            {
                book.ImageUrl = "/images/default.png"; // Default image if none provided
            }

            // Handle PDF upload
            if (PdfFile != null && PdfFile.Length > 0)
            {
                var pdfFileName = Path.GetFileName(PdfFile.FileName);
                var pdfFilePath = Path.Combine(_hostEnvironment.WebRootPath, "pdfs", pdfFileName);

                // Save the PDF in wwwroot/pdfs
                using (var stream = new FileStream(pdfFilePath, FileMode.Create))
                {
                    await PdfFile.CopyToAsync(stream);
                }

                // Set the PDF URL for the book
                book.PdfUrl = "/pdfs/" + pdfFileName;
            }

            // Set the DatePublished to UTC
            book.DatePublished = book.DatePublished.ToUniversalTime();

            // Set AvailableForBorrow based on IsAvailableForBorrow
            book.AvailableForBorrow = book.IsAvailableForBorrow ? 3 : 0;

            // Add the book to the database
            _context.Add(book);
            await _context.SaveChangesAsync();

            // Redirect to the Index page or wherever you want after adding the book
            return RedirectToAction(nameof(Index));
        }

        // If model state is invalid, return the view with validation errors
        return View(book);
    }
    // GET: Book/Edit/5
    public async Task<IActionResult> Edit(int id)
    {
        var book = await _context.Books.FindAsync(id);
        if (book == null)
        {
            return NotFound();
        }
        return View(book);
    }



    // POST: Book/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Author,Age,Genre,NumberOfCopies,AvailableForBorrow,DatePublished,Price,IsAvailableForBorrow,ImageUrl,PdfUrl")] Book book, IFormFile ImageFile, IFormFile PdfFile)
    {
        if (id != book.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            // Ensure DatePublished is in UTC
            if (book.DatePublished.Kind == DateTimeKind.Unspecified)
            {
                book.DatePublished = DateTime.SpecifyKind(book.DatePublished, DateTimeKind.Utc);
            }

            try
            {
                // Handle Image Upload if a new image is uploaded
                if (ImageFile != null && ImageFile.Length > 0)
                {
                    var fileName = Path.GetFileName(ImageFile.FileName);
                    var filePath = Path.Combine(_hostEnvironment.WebRootPath, "images", fileName);

                    // Save the image in wwwroot/images
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await ImageFile.CopyToAsync(stream);
                    }

                    // Update the ImageUrl of the book
                    book.ImageUrl = "/images/" + fileName;
                }

                // Handle PDF Upload if a new PDF file is uploaded
                if (PdfFile != null && PdfFile.Length > 0)
                {
                    var pdfFileName = Path.GetFileName(PdfFile.FileName);
                    var pdfFilePath = Path.Combine(_hostEnvironment.WebRootPath, "pdfs", pdfFileName);

                    // Save the PDF in wwwroot/pdfs
                    using (var stream = new FileStream(pdfFilePath, FileMode.Create))
                    {
                        await PdfFile.CopyToAsync(stream);
                    }

                    // Update the PdfUrl of the book
                    book.PdfUrl = "/pdfs/" + pdfFileName;
                }

                // Update the book in the database
                _context.Update(book);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookExists(book.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToAction(nameof(Index));
        }

        return View(book);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        var book = await _context.Books.FindAsync(id);
        if (book == null)
        {
            return Json(new { success = false, message = "Book not found." });
        }

        _context.Books.Remove(book);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index)); // Redirect back to the list of books
    }



    // POST: Book/SetDiscount
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> SetDiscount(int id, decimal discount)
    {
        var book = await _context.Books.FindAsync(id);
        if (book == null)
        {
            return NotFound();
        }

        // تعيين قيمة الخصم
        book.DiscountPercentage2 = discount;
        book.DiscountStartDate = DateTime.UtcNow;
        book.DiscountEndDate = DateTime.UtcNow.AddDays(7);

        // حفظ التغييرات في قاعدة البيانات
        _context.Update(book);
        await _context.SaveChangesAsync();

        // إعادة التوجيه إلى قائمة الكتب
        return RedirectToAction(nameof(Index));
    }

    private bool BookExists(int id)
    {
        return _context.Books.Any(e => e.Id == id);
    }
   
}
