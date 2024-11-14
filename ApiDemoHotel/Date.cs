using System;
using System.Collections.Generic;

namespace ApiDemoHotel;

public partial class Date
{
    public int Id { get; set; }

    public DateTime? DateCheckIn { get; set; }

    public DateTime? DateCheckOut { get; set; }

    public int IdClient { get; set; }

    public virtual User IdClientNavigation { get; set; } = null!;
}
