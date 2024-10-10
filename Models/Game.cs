using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_Dusza.Utils;
using WPF_MVVM_Template.Models;

namespace WPF_Dusza.Models
{
    public record class Game
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string OrganizerName { get; set; }
       
        public List<Participant> Participants { get; set; }
        public bool IsGameOver { get; set; }
        public override string ToString()
        {
            return $"{Name} {OrganizerName} {Participants.DisplayList()} {IsGameOver}";
        }
    }
}
