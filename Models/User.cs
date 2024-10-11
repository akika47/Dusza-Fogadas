namespace WPF_Dusza.Models
{
    public record class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public int Points { get; set; }
        // 0 - admin, 1 - szervező, 2 - játékos
        public int Role { get; set; }


    }
}
