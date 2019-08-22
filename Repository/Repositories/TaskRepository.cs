using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Repository.Interface;
using Repository.Models;

namespace Repository.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private const string ConnectionString = "Server=82.209.241.82;Database=NoneAndTaskDb;User Id=Tihon;Password=4Ayssahar0m;";

        public IEnumerable<TaskEntity> Get(int? id, bool archived, int userId)
        {
            
            
            var query = id != null ? $"SELECT * FROM Tasks WHERE TaskListId = '{id}' AND UserId = '{userId}'" :
                 $"SELECT * FROM Tasks WHERE IsDone = '{false}' AND UserId = '{userId}' AND TaskListId = null";

            if (archived)
            {
                query = $"SELECT * FROM Tasks WHERE IsDone = '{archived}' AND UserId = '{userId}'";
            }

            var tasks = new List<TaskEntity>();
            
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                using (var dataReader = new SqlCommand(query, connection).ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        var task = new TaskEntity
                        {
                            Id = Convert.ToInt32(dataReader["Id"]),
                            Name = Convert.ToString(dataReader["Name"]),
                            Description = Convert.ToString(dataReader["Description"]),
                            ExpiresOn = Convert.ToDateTime("ExpiresOn")
                        };
                        tasks.Add(task);
                    }
                }
                connection.Close();
            }
            return tasks;
        }

        public void Create(TaskEntity task)
        {
            if (task.TaskListId != null)
            {
                CreateWithListId(task);
            }
            
            CreateWithoutListId(task);
        }

        private static void CreateWithListId(TaskEntity task)
        {
            const string query = "INSERT INTO Tasks (Name, Description, IsDone, UserId, TaskListId) VALUES ('name', 'description', 'isDone', 'userId', 'taskListId')";
            using (var connection = new SqlConnection(ConnectionString))
            {
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("name", task.Name);
                    command.Parameters.AddWithValue("description", task.Description);
                    command.Parameters.AddWithValue("isDone", false);
                    command.Parameters.AddWithValue("userId", task.UserId);
                    command.Parameters.AddWithValue("taskListId", task.TaskListId);
                    
                    //command.CommandType = CommandType.Text;
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }  
            }
        }
        
        private static void CreateWithoutListId(TaskEntity task)
        {
            const string query = "INSERT INTO Tasks (Name, Description, IsDone, UserId) VALUES ('name', 'description', 'isDone', 'userId')";
            using (var connection = new SqlConnection(ConnectionString))
            {
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("name", task.Name);
                    command.Parameters.AddWithValue("description", task.Description);
                    command.Parameters.AddWithValue("isDone", false);
                    command.Parameters.AddWithValue("userId", task.UserId);
                    
                    //command.CommandType = CommandType.Text;
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }  
            }
        }
    }
}