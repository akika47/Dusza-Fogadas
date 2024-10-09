using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Dusza.Models
{
    public record class Bet
    {
        public int EventId { get; set; }
        public int UserID { get; set; }
        public int ParticipantId { get; set; }
        public string Prediction {  get; set; }
        public int BetAmount { get; set; }
    }
}
