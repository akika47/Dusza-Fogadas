using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Dusza.Models
{
    public record class Event
    {
        public int Id { get; set; }
        public string EventName { get; set; }
        // 0 - open, 1 - closed
        public int Status { get; set; }
    }
}
