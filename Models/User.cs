using System.Runtime.CompilerServices;

namespace WPF_Dusza.Models
{
    public sealed record User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public int Points { get; set; }
        // 0 - admin, 1 - szervező, 2 - játékos
        public int Role { get; set; }

        public bool Equals(User? other)
        {
            if(other == null) return false;
            return other is User user && Name == user.Name
                && Password == user.Password;
        }
        public override int GetHashCode() => base.GetHashCode();
    }
}
