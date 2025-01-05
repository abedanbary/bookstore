using System.Threading;
using System.Threading.Tasks;
using idefny.Data;
using idefny.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

public class AutoReturnService : IHostedService
{
    private readonly IServiceProvider _serviceProvider;
    private Timer _timer;
    private CancellationTokenSource _cancellationTokenSource;

    public AutoReturnService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
        _cancellationTokenSource = new CancellationTokenSource();
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        // بدء التايمر مع التأكد من أنه يقوم بتشغيل `ExecuteAsync` كل دقيقة
        _timer = new Timer(async state => await ExecuteAsync(state, _cancellationTokenSource.Token), null, 0,10000); // كل دقيقة
        return Task.CompletedTask;
    }

      public async Task ExecuteAsync(object state, CancellationToken cancellationToken)
      {
          using (var scope = _serviceProvider.CreateScope())
          {
              var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

              // جلب جميع الكتب المستعارة التي يجب إرجاعها
              var booksToReturn = await dbContext.BorrowedBooks
                  .Where(b => b.ReturnDate2 <= DateTime.UtcNow)
                  .ToListAsync(cancellationToken);
          

                foreach (var borrowedBook in booksToReturn)
              {
                  // تحديث تاريخ الإرجاع
                  var book = await dbContext.Books
                     .FirstOrDefaultAsync(b => b.Id == borrowedBook.BookId, cancellationToken);

                  if (book != null)
                  {
                      // إضافة 1 إلى الحالة المتاحة للإعارة
                      book.AvailableForBorrow++;

                    var waitlist = await dbContext.Waitlists
                       .Where(w => w.BookId == borrowedBook.BookId)
                       .OrderBy(w => w.DateJoined)  // Order by the date the user joined the waitlist
                       .FirstOrDefaultAsync(cancellationToken);
                    if (waitlist != null)
                    {
                        var borrowedBookForWaitlistUser = new BorrowedBook
                        {
                            UserId = waitlist.UserId,
                            BookId = borrowedBook.BookId,
                            BorrowDate = DateTime.UtcNow,
                            DaysToBorrow = 1, // or however many days you want
                            ReturnDate2 = DateTime.UtcNow.AddDays(4) // Example return date
                        };
                        dbContext.BorrowedBooks.Add(borrowedBookForWaitlistUser);

                        // Remove the user from the waitlist
                        dbContext.Waitlists.Remove(waitlist);


                    } 

                   

              


                }


                dbContext.BorrowedBooks.Remove(borrowedBook);
              }

              await dbContext.SaveChangesAsync(cancellationToken);
          }
      }
    


    /*
    public async Task ExecuteAsync(object state, CancellationToken cancellationToken)
    {
        using (var scope = _serviceProvider.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            // Get all borrowed books that should be returned
            var booksToReturn = await dbContext.BorrowedBooks
                .Where(b => b.ReturnDate2 <= DateTime.UtcNow)  // Check for books past their return date
                .ToListAsync(cancellationToken);

            foreach (var borrowedBook in booksToReturn)
            {
                // Find the book in the Books table based on the BookId from BorrowedBooks
                var book = await dbContext.Books
                   .FirstOrDefaultAsync(b => b.Id == borrowedBook.BookId, cancellationToken);

                if (book != null)
                {
                    // Add 1 to the AvailableForBorrow field when the book is returned
                    book.AvailableForBorrow++;

                    // Check if there are any users in the waitlist for this book
                    var waitlist = await dbContext.Waitlists
                        .Where(w => w.BookId == borrowedBook.BookId)
                        .OrderBy(w => w.DateJoined)  // Order by the date the user joined the waitlist
                        .FirstOrDefaultAsync(cancellationToken);

                    if (waitlist != null)
                    {
                        // If there is a user in the waitlist, borrow the book to that user
                        var borrowedBookForWaitlistUser = new BorrowedBook
                        {
                            UserId = waitlist.UserId,
                            BookId = borrowedBook.BookId,
                            BorrowDate = DateTime.UtcNow,
                            DaysToBorrow = 1, // or however many days you want
                            ReturnDate2 = DateTime.UtcNow.AddDays(1) // Example return date
                        };

                        // Add the borrowed book record for the user on the waitlist
                        dbContext.BorrowedBooks.Add(borrowedBookForWaitlistUser);

                        // Remove the user from the waitlist
                        dbContext.Waitlists.Remove(waitlist);
                    }
                }

                // Remove the returned book from the BorrowedBooks table
                dbContext.BorrowedBooks.Remove(borrowedBook);
            }

            // Save changes to the database
            await dbContext.SaveChangesAsync(cancellationToken);
        }
    }
    */

    public Task StopAsync(CancellationToken cancellationToken)
    {
        // إيقاف الـ Timer
        _timer?.Dispose();
        _cancellationTokenSource.Cancel();
        return Task.CompletedTask;
    }
}




