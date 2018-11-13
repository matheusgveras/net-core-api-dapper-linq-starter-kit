namespace StarterKit.Data.Models
{
    public class State : BaseEntity
    {
        public string Title { get; set; }
        public Country Country { get; set; }
    }
}