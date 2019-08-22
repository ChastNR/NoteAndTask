using System.Collections.Generic;
using Repository.Models;

namespace Repository.Interface
{
    public interface IListRepository
    {
        IEnumerable<TaskList> Get(int userId, object orderBy);

        bool Add(string name, int userId);
    }
}