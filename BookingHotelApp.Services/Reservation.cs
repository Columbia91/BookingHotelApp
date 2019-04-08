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
        public static void BookingNumber(BookingLog note, Room room, User user, Hotel hotel)
        {
            //note.RoomNumber = room.Number;
            //note.GuestName = user.Login;
            //note.HotelName = hotel.Name;
            user.Id = AccountsTableDataService.GetAccountId(user.Login);

            Console.Write("Please ender data:\n" +
                "ArrivalDate (dd.mm.yyyy): ");
            note.ArrivalDate = DateTime.Parse(Console.ReadLine());

            Console.Write("DepartureDate (dd.mm.yyyy): ");
            note.DepartureDate = DateTime.Parse(Console.ReadLine());

            Console.WriteLine("\n1) To book \n2) To Pay");
            int choice = int.Parse(Console.ReadLine());

            if (choice == 1)
                note.Payment = 0;
            else if(choice == 2)
                note.Payment = (note.DepartureDate - note.ArrivalDate).TotalDays * RoomsTableDataService.GetRoomPrice(room.Id);

            BookingLogTableDataService.AddNote(note, room.Id, user.Id, hotel.Id);
        }
    }
}
