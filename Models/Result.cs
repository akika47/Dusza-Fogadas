using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Dusza.Models
{
    public record class Result
    {
        public int Id { get; set; }
        public int userId { get; set; }
        public int eventId { get; set; }
        public int EndResult {  get; set; }
        public float Multiplier { get; set; }
    }
}
