using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Dusza.Models
{
    public record class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public int Points { get; set; }
        // 0 - admin, 1 - szervező, 2 - játékos
        public byte Role { get; set; }
    }
}
