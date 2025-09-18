using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESMCommon.Database.Model
{
    public class WorkItem
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string Command { get; set; } = "";
        public string Arguments { get; set; } = "";
        public DateTime ScheduleTime { get; set; }
        public string RepeatType { get; set; } = "None"; // None, Daily, Weekly
        public bool Enabled { get; set; } = true;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
