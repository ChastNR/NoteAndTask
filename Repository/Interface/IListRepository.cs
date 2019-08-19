using System.Collections.Generic;
using Repository.Models;

namespace Repository.Interface
{
    public interface IListRepository
    {
        IEnumerable<TaskList> Get(string userId, object orderBy);
    }
}