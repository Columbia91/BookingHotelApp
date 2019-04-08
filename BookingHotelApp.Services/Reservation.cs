using BookingHotelApp.DataAccess;
using BookingHotelApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingHotelApp.Services
{
    public class Reservation
    {
        public static void BookingNumber(Room room)
        {
            BookingLog note = new BookingLog();

            Console.Write("Please ender data:\n" +
                "ArrivalDate (dd.mm.yyyy): ");
            note.ArrivalDate = DateTime.Parse(Console.ReadLine());

            Console.Write("DepartureDate (dd.mm.yyyy): ");
            note.DepartureDate = DateTime.Parse(Console.ReadLine());

            note.Payment = (note.DepartureDate - note.ArrivalDate).TotalDays * (int)RoomsTableDataService.GetRoomPrice(room.Id);
        }
    }
}
