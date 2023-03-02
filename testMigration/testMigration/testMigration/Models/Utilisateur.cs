using System;
using System.Collections.Generic;

namespace testMigration.Models;

public partial class Utilisateur
{
    public int IdUser { get; set; }
    public string Pseudo { get; set; } = null!;
    public string Mail { get; set; } = null!;
    public string Password { get; set; } = null!;
    public int Theme { get; set; }
    public virtual ICollection<Message> Messages { get; } = new List<Message>();
    public virtual ICollection<Salon> Salons { get; } = new List<Salon>();
}
