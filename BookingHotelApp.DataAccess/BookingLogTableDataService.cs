using BookingHotelApp.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingHotelApp.DataAccess
{
    public class BookingLogTableDataService
    {
        private static readonly string _connectionString;
        static BookingLogTableDataService()
        {
            _connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ADILET\Desktop\BookingHotelApp\BookingHotelApp.DataAccess\Database.mdf;Integrated Security=True";
        }

        #region Стоимость бронирования
        public static object GetRentPrice(int days)
        {

        }
        #endregion
        #region Добавление пользователя в базу данных
        public void AddNote(BookingLog note)
        {
            using (var connection = new SqlConnection(_connectionString))
            using (var command = connection.CreateCommand())
            {
                try
                {
                    connection.Open();
                    command.CommandText = $"INSERT into BookingLog values ('{note.RoomNumber}','{note.GuestName}'," +
                        $"'{note.HotelName}','{note.ArrivalDate}','{note.ArrivalDate}','{note.DepartureDate}','{note.Payment}')";
                    
                    var affectedRows = command.ExecuteNonQuery(); //число строк которые подвергнуты каким либо изменениям 

                    if (affectedRows < 1)
                    {
                        throw new Exception("Вставка не была произведена");
                    }
                }
                catch (SqlException exception)
                {
                    //TODO обработка ошибки
                    Console.WriteLine(exception.Message);
                    //throw;
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception.Message);
                    //TODO обработка ошибки
                    //throw;
                }
            }
        }
        #endregion
    }
}
