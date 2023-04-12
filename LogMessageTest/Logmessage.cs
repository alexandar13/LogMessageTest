using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace LogMessageTest;

public partial class Logmessage
{
    public int Id { get; set; }

    public int ApplicationId { get; set; }

    public DateTime? Date { get; set; }

    [Column("log_level")]
    public loglevel log_level { get; set; }

    public string? Message { get; set; }

    public virtual Application Application { get; set; } = null!;
}

public enum loglevel
{
    trace = 1,
    debug = 2,
    info = 3,
    warn = 4,
    error = 5
}
