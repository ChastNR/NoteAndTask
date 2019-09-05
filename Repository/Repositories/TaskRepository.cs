using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using ProjectModels;
using Repository.Interface;

namespace Repository.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private const string ConnectionString =
            "Server=82.209.241.82;Database=NoneAndTaskDb;User Id=Tihon;Password=4Ayssahar0m;";

        #region Get

        public IEnumerable<TaskEntity> Get(int? taskListId, bool archived, int userId)
        {
            if (taskListId != null)
            {
                return GetWithListId(taskListId, userId);
            }

            return archived ? GetArchived(userId) : GetDefault(userId);
        }

        public IEnumerable<TaskEntity> GetAll(int userId)
        {
            var tasks = new List<TaskEntity>();

            using (var connection = new SqlConnection(ConnectionString))
            {
                const string query = "SELECT * FROM Tasks WHERE UserId = @userId";
                var command = new SqlCommand(query, connection);
                command.Parameters.Add("@userId", SqlDbType.Int).Value = userId;

                connection.Open();
                using (var dataReader = command.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        tasks.Add(new TaskEntity
                        {
                            Id = Convert.ToInt32(dataReader["Id"]),
                            Name = Convert.ToString(dataReader["Name"]),
                            Description = Convert.ToString(dataReader["Description"]),
                            ExpiresOn = Convert.ToDateTime(dataReader["ExpiresOn"]),
                            IsDone = Convert.ToBoolean(dataReader["IsDone"]),
                            TaskListId = dataReader["TaskListId"] != DBNull.Value
                                ? Convert.ToInt32(dataReader["TaskListId"])
                                : (int?) null
                        });
                    }
                }

                connection.Close();
            }

            return tasks;
        }

        private static IEnumerable<TaskEntity> GetArchived(int userId)
        {
            var tasks = new List<TaskEntity>();

            using (var connection = new SqlConnection(ConnectionString))
            {
                const string query = "SELECT * FROM Tasks WHERE IsDone = 'true' AND UserId = @userId";
                var command = new SqlCommand(query, connection);
                command.Parameters.Add("@userId", SqlDbType.Int).Value = userId;

                connection.Open();
                using (var dataReader = command.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        tasks.Add(new TaskEntity
                        {
                            Id = Convert.ToInt32(dataReader["Id"]),
                            Name = Convert.ToString(dataReader["Name"]),
                            Description = Convert.ToString(dataReader["Description"]),
                            ExpiresOn = Convert.ToDateTime(dataReader["ExpiresOn"]),
                            IsDone = Convert.ToBoolean(dataReader["isDone"])
                        });
                    }
                }

                connection.Close();
            }

            return tasks;
        }

        private static IEnumerable<TaskEntity> GetWithListId(int? taskListId, int userId)
        {
            var tasks = new List<TaskEntity>();

            using (var connection = new SqlConnection(ConnectionString))
            {
                const string query =
                    "SELECT * FROM Tasks WHERE IsDone = 'false' AND TaskListId = @taskListId AND UserId = @userId";
                var command = new SqlCommand(query, connection);
                command.Parameters.Add("@taskListId", SqlDbType.Int).Value = taskListId;
                command.Parameters.Add("@userId", SqlDbType.Int).Value = userId;

                connection.Open();
                using (var dataReader = command.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        tasks.Add(new TaskEntity
                        {
                            Id = Convert.ToInt32(dataReader["Id"]),
                            Name = Convert.ToString(dataReader["Name"]),
                            Description = Convert.ToString(dataReader["Description"]),
                            ExpiresOn = Convert.ToDateTime(dataReader["ExpiresOn"])
                        });
                    }
                }

                connection.Close();
            }

            return tasks;
        }

        private static IEnumerable<TaskEntity> GetDefault(int userId)
        {
            var tasks = new List<TaskEntity>();

            using (var connection = new SqlConnection(ConnectionString))
            {
                const string query =
                    "SELECT * FROM Tasks WHERE IsDone = 'false' AND TaskListId IS NULL AND UserId = @userId";

                var command = new SqlCommand(query, connection);
                command.Parameters.Add("@userId", SqlDbType.Int).Value = userId;

                connection.Open();
                using (var dataReader = command.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        tasks.Add(new TaskEntity
                        {
                            Id = Convert.ToInt32(dataReader["Id"]),
                            Name = Convert.ToString(dataReader["Name"]),
                            Description = Convert.ToString(dataReader["Description"]),
                            ExpiresOn = Convert.ToDateTime(dataReader["ExpiresOn"])
                        });
                    }
                }

                connection.Close();
            }

            return tasks;
        }

        #endregion

        #region Create

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
            const string query =
                "INSERT INTO Tasks (Name, Description, ExpiresOn, IsDone, UserId, TaskListId) VALUES (@name, @description, @expiresOn, 'false' , @userId, @taskListId)";
            using (var connection = new SqlConnection(ConnectionString))
            {
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@name", SqlDbType.VarChar).Value = task.Name;
                    command.Parameters.Add("@description", SqlDbType.VarChar).Value = task.Description;
                    command.Parameters.Add("@userId", SqlDbType.Int).Value = task.UserId;
                    command.Parameters.Add("@taskListId", SqlDbType.Int).Value = task.TaskListId;
                    command.Parameters.Add("@expiresOn", SqlDbType.DateTime2).Value = task.ExpiresOn;

                    //command.CommandType = CommandType.Text;
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }

        private static void CreateWithoutListId(TaskEntity task)
        {
            const string query =
                "INSERT INTO Tasks (Name, Description, ExpiresOn, IsDone, UserId) VALUES (@name, @description, @expiresOn ,'false', @userId)";
            using (var connection = new SqlConnection(ConnectionString))
            {
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@name", SqlDbType.VarChar).Value = task.Name;
                    command.Parameters.Add("@description", SqlDbType.VarChar).Value = task.Description;
                    command.Parameters.Add("@userId", SqlDbType.Int).Value = Convert.ToInt32(task.UserId);
                    command.Parameters.Add("@expiresOn", SqlDbType.DateTime2).Value = task.ExpiresOn;

                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }

        #endregion

        #region TaskDone

        public bool TaskDone(int? id)
        {
            bool done;

            using (var connection = new SqlConnection(ConnectionString))
            {
                const string query = "UPDATE Tasks SET IsDone = 'true' WHERE Id = @id";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@id", SqlDbType.Int).Value = id;

                    connection.Open();
                    done = command.ExecuteNonQuery() > 0;
                    connection.Close();
                }
            }

            return done;
        }

        #endregion
    }
}