﻿namespace WPF_Dusza.Models
{
    public record class Event
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public override string ToString()
        {
            return Name;
        }
    }
}
