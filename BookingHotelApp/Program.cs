﻿using BookingHotelApp.Models;
using BookingHotelApp.Services;
using BookingHotelApp.DataAccess;
using System;
using System.Collections.Generic;

namespace BookingHotelApp.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            User user = new User();
            var dataService = new AccountsTableDataService();

            while (true) {
                System.Console.Clear();
                System.Console.Write("1) Registration \n" +
                    "2) Authorization \n" +
                    "3) Exit \n" +
                    "Choice: ");
                int choice = int.Parse(System.Console.ReadLine());

                if (choice == 1)
                    Registration.SignUp(user, dataService);
                else if (choice == 2)
                {
                    Authorization.SignIn(user);
                    System.Console.ReadLine();
                    List<Hotel> hotels = HotelsTableDataService.GetAllHotels();
                    for (int i = 0; i < hotels.Count; i++)
                    {
                        System.Console.WriteLine($"{i+1}) {hotels[i].Name}");
                    }
                    System.Console.Write("Choice: ");
                    choice = int.Parse(System.Console.ReadLine());

                    List<Room> rooms = RoomsTableDataService.GetAvailableRooms(hotels[choice].Id);
                    System.Console.WriteLine("{0,5} | {1,15} | {2,10}", "№", "Category","Price");

                    for (int i = 0; i < rooms.Count; i++)
                    {
                        rooms[i].Show(i+1);
                    }
                }
                else
                    System.Environment.Exit(0);
            }
        }
    }
}
