using System;
using System.Collections.Generic;

namespace testMigration.Models;

public partial class Message
{
    public int IdMessage { get; set; }
    public string Message1 { get; set; } = null!;
    public DateTime Date { get; set; }
    public string? Image { get; set; }
    public int IdUser { get; set; }
    public int IdChannel { get; set; }
    public virtual Salon IdChannelNavigation { get; set; } = null!;
    public virtual Utilisateur IdUserNavigation { get; set; } = null!;
}
