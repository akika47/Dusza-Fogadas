using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Dusza.Models
{
    public record class BetParticipant
    {
        public int betId {  get; set; }
        public int userId {  get; set; }
        public int Amount { get; set; }
    }
}
