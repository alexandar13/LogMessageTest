using System;
using System.Collections.Generic;

namespace LogMessageTest;

public partial class Application
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Logmessage> Logmessages { get; set; } = new List<Logmessage>();
}
