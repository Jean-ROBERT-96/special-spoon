using APITodo.Models;

namespace APITodo.Data
{
    public class DataRepo : IDataRepo
    {
        DataContext context;

        public DataRepo(DataContext context)
        {
            this.context = context;
        }

        public List<Tasks> GetAllTasks()
        {
            return context.Tasks1.ToList();
        }
    }
}
