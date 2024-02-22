using System.ComponentModel.DataAnnotations;

namespace URLShortener.Data.Entities
{
    public class UserDTO
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Username { get; set; }

        public bool IsAdmin { get; set; }
    }
}
