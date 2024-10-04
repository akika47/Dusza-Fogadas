using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Dusza.Models
{
    public record class Game
    {
        public int Id { get; set; }
        public string OrganizerName { get ; set; }
        public int eventId { get; set; }
    }
}
