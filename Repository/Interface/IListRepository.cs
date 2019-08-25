using System.Collections.Generic;
using ProjectModels;

namespace Repository.Interface
{
    public interface IListRepository
    {
        IEnumerable<TaskList> Get(int userId);

        bool Add(string name, int userId);

        bool Delete(int? id);
    }
}