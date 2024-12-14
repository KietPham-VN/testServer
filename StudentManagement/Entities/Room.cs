using System;
using System.Collections.Generic;

namespace StudentManagement.Entities;

public partial class Room
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();
}
