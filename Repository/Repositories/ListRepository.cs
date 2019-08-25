using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using ProjectModels;
using Repository.Interface;

namespace Repository.Repositories
{
    public class ListRepository : IListRepository
    {
        private const string ConnectionString = "Server=82.209.241.82;Database=NoneAndTaskDb;User Id=Tihon;Password=4Ayssahar0m;";

        public IEnumerable<TaskList> Get(int userId)
        {
            var lists = new List<TaskList>();

            using (var connection = new SqlConnection(ConnectionString))
            {
                const string query = "SELECT * FROM TaskLists WHERE UserId = @userId ORDER BY CreationDate DESC";
                var command = new SqlCommand(query, connection);
                command.Parameters.Add("@userId", SqlDbType.Int).Value = userId;
                
                connection.Open();
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

            using (var connection = new SqlConnection(ConnectionString))
            {
                const string query = "INSERT INTO TaskLists (Name, UserId) VALUES (@name, @userId)";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@name", SqlDbType.VarChar).Value = name;
                    command.Parameters.Add("@userId", SqlDbType.Int).Value = userId;   
                        
                    connection.Open();
                    insertedData = command.ExecuteNonQuery() > 0;
                    connection.Close();
                }
            }
            return insertedData;
        }

        public bool Delete(int? id)
        {
            bool deletedData;

            using (var connection = new SqlConnection(ConnectionString))
            {
                const string query = "DELETE FROM TaskLists WHERE Id = @id";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@id", SqlDbType.Int).Value = id;   
                        
                    connection.Open();
                    deletedData = command.ExecuteNonQuery() > 0;
                    connection.Close();
                }
            }
            return deletedData;
        }
    }
}