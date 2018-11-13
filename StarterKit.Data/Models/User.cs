using System;

namespace StarterKit.Data.Models
{
    public class User : BaseEntity
    {
       
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Pass { get; set; }
        public string Token { get; set; }
        public DateTime Datecreated { get; set; }
        public int isactive { get; set; }
        public int user_type_id { get; set; }
    }
}