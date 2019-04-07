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
            _connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Nurdaulet\Source\Repos\BookingHotelApp\BookingHotelApp.DataAccess\Database.mdf;Integrated Security=True";
        }

        #region Получить коллекцию всех пользователей
        public static List<Room> GetAvailableRooms(int hotelId)
        {
            var data = new List<Room>(); //буферный список пользователей

            using (var connection = new SqlConnection(_connectionString))
            using (var command = connection.CreateCommand())
            {
                try
                {
                    connection.Open();
                    command.CommandText = $"SELECT id, number, category, price, status " +
                        $"FROM Rooms r " +
                        $"where status = available and hotel_id = {hotelId}";

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
    }
}
