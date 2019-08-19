using System;
using System.Data.SqlClient;
using Repository.Interface;
using Repository.Models;

namespace Repository.SqlRepositories
{
    public class UserRepository : IUserRepository
    {
        private readonly SqlConnection _connection;
        
        public UserRepository()
        {
            _connection = new SqlConnection("Server=82.209.241.82;Database=NoneAndTaskDb;User Id=Tihon;Password=4Ayssahar0m;");
        }

        public User AuthUser(string login)
        {
            var user = new User();
            var query = $"SELECT * FROM Users WHERE Email='{login}' OR PhoneNumber='{login}'";
            
            _connection.Open();
            
            using (var dataReader = new SqlCommand(query, _connection).ExecuteReader())
            {
                while (dataReader.Read())
                {
                    user.Id = Convert.ToString(dataReader["Id"]);
                }
            }
            _connection.Close();
            
            return user;
        }

        public User UserExist(string email, string phoneNumber)
        {
            var user = new User();
            var query = $"SELECT * FROM Users WHERE Email='{email}' OR PhoneNumber='{phoneNumber}'";
            
            _connection.Open();
            
            using (var dataReader = new SqlCommand(query, _connection).ExecuteReader())
            {
                while (dataReader.Read())
                {
                    user.Id = Convert.ToString(dataReader["Id"]);
                }
            }
            _connection.Close();
            
            return user;
        }
    }
}