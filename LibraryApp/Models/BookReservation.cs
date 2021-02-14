using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApp.Models
{
    public class BookReservation
    {
        [Key]
        public int BookReservationId { get; set; }
        public DateTime ReservationDate { get; set; }

        public int BookId { get; set; }
        public virtual Book Book { get; set; }

        public string UserId { get; set; }
        public string UserName { get; set; }

    }
}
