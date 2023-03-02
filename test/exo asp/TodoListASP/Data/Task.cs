using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class Task
    {
        public int id { get; init; }
        public string user { get; set; }
        public string task { get; set; }
        public string date { get; set; }
        public bool isChecked { get; set; }

        public Task(int id, string user, string task, string date, bool isChecked)
        {
            this.id = id;
            this.user = user;
            this.task = task;
            this.date = date;
            this.isChecked = isChecked;
        }

        public void Check(bool check)
        {
            isChecked = check;
        }

        public void Update(string u, string t)
        {
            user = u;
            task = t;
            date = $"{DateTime.Now.Day.ToString()}/{DateTime.Now.Month.ToString()}/{DateTime.Now.Year.ToString()}";
        }
    }
}
