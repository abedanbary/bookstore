using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace idefny.Models
{
    
        public class UserBook
        {
            public int Id { get; set; }
            public string UserId { get; set; }
            public int BookId { get; set; }
            public int Quantity { get; set; }
            public DateTime PurchaseDate { get; set; }

            // خصائص التنقل
            public virtual Users User { get; set; }
            public virtual Book Book { get; set; }
        }

    }


