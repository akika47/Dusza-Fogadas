using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Dusza.Models
{
    public record class EventDetails
    {
        public int Id { get; set; }
        public int EventAmount { get; set; }
        public int ParticipantAmount { get; set; }

    }
}
