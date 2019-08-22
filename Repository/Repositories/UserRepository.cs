using System;
using System.Data;
using System.Data.SqlClient;
using Repository.Interface;
using Repository.Models;

namespace Repository.Repositories
{
    public class UserRepository : IUserRepository
    {
        private const string connectionString = "Server=82.209.241.82;Database=NoneAndTaskDb;User Id=Tihon;Password=4Ayssahar0m;";

        public User GetById(int id)
        {
            var user = new User();

            using (var connection = new SqlConnection(connectionString))
            {
                const string query = "SELECT * FROM Users WHERE Id = id";

                var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("id", id);
                
                connection.Open();

                using (var dataReader = command.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        user.Name = Convert.ToString(dataReader["Name"]);
                        user.Email = Convert.ToString(dataReader["Email"]);
                        user.PhoneNumber = Convert.ToString(dataReader["PhoneNumber"]);
                    }
                }
                connection.Close();
            }
            return user;
        }

        public User AuthUser(string login)
        {
            var user = new User();

            using (var connection = new SqlConnection(connectionString))
            {
                const string query = "SELECT * FROM Users WHERE Email='login' OR PhoneNumber='login'";
                var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("login", login);
                
                connection.Open();
                using (var dataReader = command.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        user.Id = Convert.ToInt32(dataReader["Id"]);
                    }
                }
                connection.Close();
            }
            return user;
        }

        public User UserExist(string email, string phoneNumber)
        {
            var user = new User();

            using (var connection = new SqlConnection(connectionString))
            {
                const string query = "SELECT * FROM Users WHERE Email='email' OR PhoneNumber='phoneNumber'";
                var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("email", email);
                command.Parameters.AddWithValue("phoneNumber", phoneNumber);
                
                connection.Open();
                using (var dataReader = command.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        user.Id = Convert.ToInt32(dataReader["Id"]);
                    }
                }
                connection.Close();
            }
            return user;
        }

        public void Add(User user)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                const string query = "INSERT INTO Users (Name, Email, PasswordHash, PhoneNumber) values ('name', 'email', 'passwordHash', 'phoneNumber')";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("name", user.Name);
                    command.Parameters.AddWithValue("email", user.Email);
                    command.Parameters.AddWithValue("passwordHash", user.PasswordHash);
                    command.Parameters.AddWithValue("phoneNumber", user.PhoneNumber);
                    
                    //command.CommandType = CommandType.Text;
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }
    }
}