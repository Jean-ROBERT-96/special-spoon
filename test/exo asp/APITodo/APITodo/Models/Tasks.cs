using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APITodo.Models
{
    [Table("Todolist")]
    public class Tasks
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("user")]
        public string User { get; set; }

        [Column("task")]
        public string Task { get; set; }

        [Column("is_checked")]
        public bool IsChecked { get; set; }

        [Column("date")]
        public string Date { get; set; }

        /*public Tasks(int id, string user, string task, string date, bool isChecked = false)
        {
            this.Id = id;
            this.User = user;
            this.Task = task;
            this.IsChecked = isChecked;
            this.Date = date;
        }*/
    }
}
