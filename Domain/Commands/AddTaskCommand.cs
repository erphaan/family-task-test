using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Commands
{
    public class AddTaskCommand
    {
        public Guid AssignedTold { get; set; }
        public string Subject { get; set; }
        public bool IsComplete { get; set; }
    }
}
