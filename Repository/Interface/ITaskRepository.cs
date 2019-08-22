using System.Collections.Generic;
using Repository.Models;

namespace Repository.Interface
{
    public interface ITaskRepository
    {
        IEnumerable<TaskEntity> Get(int? id, bool archived, int userId);
        void Create(TaskEntity task);
    }
}