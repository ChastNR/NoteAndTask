using System.Collections.Generic;
using ProjectModels;

namespace Repository.Interface
{
    public interface ITaskRepository
    {
        IEnumerable<TaskEntity> Get(int? taskListId, bool archived, int userId);
        IEnumerable<TaskEntity> GetAll(int userId);
        void Create(TaskEntity task);
        bool TaskDone(int? id);
    }
}