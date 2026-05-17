using System;
using System.Collections.Generic;

namespace Todo.Infrastructure;

public partial class Todo
{
    public int Id { get; set; }

    public string? Title { get; set; }

    public bool? IsCompleted { get; set; }
}
