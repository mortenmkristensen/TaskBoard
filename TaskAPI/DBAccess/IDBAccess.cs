using TaskAPI.Models;

namespace TaskAPI.DBAccess {
    public interface IDBAccess {
        Assignment Upsert(Assignment assignment);
        bool Delete(string id);
        IEnumerable<Assignment> Get();
        Assignment Get(string id);
    }
}
