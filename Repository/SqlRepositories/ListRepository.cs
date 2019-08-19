using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Repository.Interface;
using Repository.Models;

namespace Repository.SqlRepositories
{
    public class ListRepository : IListRepository
    {
        private readonly SqlConnection _connection;
        
        public ListRepository()
        {
            _connection = new SqlConnection("Server=82.209.241.82;Database=NoneAndTaskDb;User Id=Tihon;Password=4Ayssahar0m;");
        }

        public IEnumerable<TaskList> Get(string userId, object orderBy)
        {
            var lists = new List<TaskList>();
            var query = $"SELECT * FROM TaskLists WHERE UserId = '{userId}' ORDER BY '{orderBy}' DESC";
            
            _connection.Open();
            
            using (var dataReader = new SqlCommand(query, _connection).ExecuteReader())
            {
                while (dataReader.Read())
                {
                    var list = new TaskList
                    {
                        Id = Convert.ToString(dataReader["Id"]),
                        Name = Convert.ToString(dataReader["Name"])
                    };
                    lists.Add(list);
                }
            }
            _connection.Close();
            
            return lists;
        }
    }
}