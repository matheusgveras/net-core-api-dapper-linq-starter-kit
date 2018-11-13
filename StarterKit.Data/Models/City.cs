using System;
namespace StarterKit.Data.Models
{
    public class City : BaseEntity
    {
        public string Title { get; set; }
        public State State { get; set; }
    }
}