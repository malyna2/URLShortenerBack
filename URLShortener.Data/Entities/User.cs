using System.ComponentModel.DataAnnotations;

namespace URLShortener.Data.Entities
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        public bool IsAdmin { get; set; }
        
        public ICollection<Link> Links { get; set; }
    }
}
