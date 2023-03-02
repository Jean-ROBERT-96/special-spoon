using APITodo.Models;

namespace APITodo.Data
{
    public interface IDataRepo
    {
        public List<Tasks> GetAllTasks();
    }
}
