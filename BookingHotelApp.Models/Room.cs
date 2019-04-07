using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingHotelApp.Models
{
    public class Room
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public string Category { get; set; }
        public string HotelName { get; set; }
        public double Price { get; set; }
        public string Status { get; set; }

        public void Show(int number)
        {
            Console.WriteLine("{0,5} | 1,15} | {2,10}",
                number, Category, Price);
        }
    }
}
