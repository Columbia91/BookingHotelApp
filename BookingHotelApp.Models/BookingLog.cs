using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingHotelApp.Models
{
    public class BookingLog
    {
        public int Id { get; set; }
        public int RoomNumber { get; set; }
        public string GuestName { get; set; }
        public string HotelName { get; set; }
        public DateTime ArrivalDate { get; set; }
        public DateTime DepartureDate { get; set; }
        public object Payment { get; set; }
    }
}
