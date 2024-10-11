using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Dusza.Models
{
    public class GameRow
    {
        public string GameName { get; set; } = "";
        public string OrganizerName { get; set; } = "";
        public string Participants { get; set; } = "";
        public string Events { get; set; } = "";
        public bool IsDisplay {  get; set; }
    }
}
