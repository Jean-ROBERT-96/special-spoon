using System;
using System.Collections.Generic;

namespace testMigration.Models;

public partial class Salon
{
    public int IdChannel { get; set; }
    public string Name { get; set; } = null!;
    public DateTime Date { get; set; }
    public int IdUser { get; set; }
    public virtual Utilisateur IdUserNavigation { get; set; } = null!;
    public virtual ICollection<Message> Messages { get; } = new List<Message>();
}
