using System;
using System.Collections.Generic;

namespace ApiDemoHotel;

public partial class Room
{
    public int Id { get; set; }

    public int Number { get; set; }

    public int Floor { get; set; }

    public int? IdCategory { get; set; }

    public int? IdStatus { get; set; }

    public virtual Category? IdCategoryNavigation { get; set; }

    public virtual Status? IdStatusNavigation { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
