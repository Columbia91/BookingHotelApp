using BookingHotelApp.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingHotelApp.DataAccess
{
    public class HotelsTableDataService
    {
        private static readonly string _connectionString;
        static HotelsTableDataService()
        {
            _connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Nurdaulet\Source\Repos\BookingHotelApp\BookingHotelApp.DataAccess\Database.mdf;Integrated Security=True";
        }

        #region Получить коллекцию всех отелей
        public static List<Hotel> GetAllHotels()
        {
            var data = new List<Hotel>(); //буферный список пользователей

            using (var connection = new SqlConnection(_connectionString))
            using (var command = connection.CreateCommand())
            {
                try
                {
                    connection.Open();
                    command.CommandText = "SELECT * FROM Hotels";

                    var sqlDataReader = command.ExecuteReader();

                    while (sqlDataReader.Read())
                    {
                        int id = (int)sqlDataReader["Id"];
                        string name = sqlDataReader["Name"].ToString();
                        
                        data.Add(new Hotel
                        {
                            Id = id,
                            Name = name
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
