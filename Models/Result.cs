using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Dusza.Models
{
    public record class Result
    {
        public string EventName { get; set; }
        public string Prediction { get; set; }
        public string EventResult { get; set; }

    }
}
