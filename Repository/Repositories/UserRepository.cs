using System;
using System.Data;
using System.Data.SqlClient;
using Repository.Interface;
using Repository.Models;

namespace Repository.Repositories
{
    public class UserRepository : IUserRepository
    {
        private const string ConnectionString = "Server=82.209.241.82;Database=NoneAndTaskDb;User Id=Tihon;Password=4Ayssahar0m;";

        public User GetById(int id)
        {
            var user = new User();

            using (var connection = new SqlConnection(ConnectionString))
            {
                const string query = "SELECT * FROM Users WHERE Id = @id";
                var command = new SqlCommand(query, connection);
                command.Parameters.Add("@id", SqlDbType.Int).Value = id;
                
                connection.Open();
                using (var dataReader = command.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        user.Id = Convert.ToInt32(dataReader["Id"]);
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

            using (var connection = new SqlConnection(ConnectionString))
            {
                const string query = "SELECT * FROM Users WHERE Email = @login OR PhoneNumber = @loginAlt";
                var command = new SqlCommand(query, connection);
                command.Parameters.Add("@login", SqlDbType.VarChar).Value = login;
                command.Parameters.Add("@loginAlt", SqlDbType.VarChar).Value = login;
                
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

            using (var connection = new SqlConnection(ConnectionString))
            {
                const string query = "SELECT * FROM Users WHERE Email=@email OR PhoneNumber=@phoneNumber";
                var command = new SqlCommand(query, connection);
                command.Parameters.Add("@email", SqlDbType.VarChar).Value = email;
                command.Parameters.Add("@phoneNumber", SqlDbType.VarChar).Value = phoneNumber;
                
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
            using (var connection = new SqlConnection(ConnectionString))
            {
                const string query = "INSERT INTO Users (Name, Email, PasswordHash, PhoneNumber) values (@name, @email, @passwordHash, @phoneNumber)";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@name", SqlDbType.VarChar).Value = user.Name;
                    command.Parameters.Add("@email", SqlDbType.VarChar).Value =  user.Email;
                    command.Parameters.Add("@passwordHash", SqlDbType.VarChar).Value = user.PasswordHash;
                    command.Parameters.Add("@phoneNumber", SqlDbType.VarChar).Value =  user.PhoneNumber;
                    
                    //command.CommandType = CommandType.Text;
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }
    }
}