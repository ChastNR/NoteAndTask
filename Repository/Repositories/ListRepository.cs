using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Repository.Interface;
using Repository.Models;

namespace Repository.Repositories
{
    public class ListRepository : IListRepository
    {
        private const string connectionString = "Server=82.209.241.82;Database=NoneAndTaskDb;User Id=Tihon;Password=4Ayssahar0m;";

        public IEnumerable<TaskList> Get(int userId, object orderBy)
        {
            var lists = new List<TaskList>();

            using (var connection = new SqlConnection(connectionString))
            {
                const string query = "SELECT * FROM TaskLists WHERE UserId = 'userId' ORDER BY 'orderBy' DESC";
                connection.Open();

                var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("userId", userId);
                command.Parameters.AddWithValue("orderBy", orderBy);
                
                using (var dataReader = command.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        var list = new TaskList
                        {
                            Id = Convert.ToInt32(dataReader["Id"]),
                            Name = Convert.ToString(dataReader["Name"])
                        };
                        lists.Add(list);
                    }
                }
                connection.Close();
            }
            return lists;
        }

        public bool Add(string name, int userId)
        {
            bool insertedData;

            using (var connection = new SqlConnection(connectionString))
            {
                const string query = "INSERT INTO TaskLists (Name, UserId) VALUES ('name', 'userId')";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("name", name);
                    command.Parameters.AddWithValue("userId", userId);   
                        
                    connection.Open();
                    insertedData = command.ExecuteNonQuery() > 0;
                    connection.Close();
                }
            }
            return insertedData;
        }
    }
}