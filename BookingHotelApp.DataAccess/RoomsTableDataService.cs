using BookingHotelApp.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingHotelApp.DataAccess
{
    public class RoomsTableDataService
    {
        private static readonly string _connectionString;
        static RoomsTableDataService()
        {
            _connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ADILET\Desktop\BookingHotelApp\BookingHotelApp.DataAccess\Database.mdf;Integrated Security=True";
        }

        #region Получить коллекцию свободных комнат
        public static List<Room> GetAvailableRooms(int hotelId)
        {
            var data = new List<Room>(); //буферный список пользователей

            using (var connection = new SqlConnection(_connectionString))
            using (var command = connection.CreateCommand())
            {
                try
                {
                    connection.Open();
                    command.CommandText = $"SELECT r.id, r.number, r.category, h.name, r.price, r.status FROM Rooms r " +
                        $"join Hotels h on r.hotel_id = h.id " +
                        $"where r.hotel_id = {hotelId} and r.status = 'available'";

                    var sqlDataReader = command.ExecuteReader();

                    while (sqlDataReader.Read())
                    {
                        int id = (int)sqlDataReader["Id"];
                        int number = (int)sqlDataReader["Number"];
                        string category = sqlDataReader["Category"].ToString();
                        string name = sqlDataReader["Name"].ToString();
                        double price = (double)sqlDataReader["Price"];
                        string status = sqlDataReader["Status"].ToString();

                        data.Add(new Room
                        {
                            Id = id,
                            Number = number,
                            Category = category,
                            HotelName = name,
                            Price = price,
                            Status = status
                        });
                    }
                    sqlDataReader.Close();
                }
                catch (SqlException exception)
                {
                    //TODO обработка ошибки
                    throw;
                }
                catch (Exception exception)
                {
                    //TODO обработка ошибки
                    throw;
                }
            }
            return data;
        }
        #endregion

        #region Узнать стоимость комнаты
        public static double GetRoomPrice(int id)
        {
            double data = 0;

            using (var connection = new SqlConnection(_connectionString))
            using (var command = connection.CreateCommand())
            {
                try
                {
                    connection.Open();
                    command.CommandText = $"SELECT price from Rooms where Id = {id}";

                    if (command.ExecuteScalar() == null)
                        return data;
                    else
                        data = (double)command.ExecuteScalar();
                }
                catch (SqlException exception)
                {
                    Console.WriteLine(exception.Message);
                    //TODO обработка ошибки
                    //throw;
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception.Message);
                    //TODO обработка ошибки
                    //throw;
                    Console.ReadLine();
                }
            }
            return data;
        }
        #endregion
    }
}
