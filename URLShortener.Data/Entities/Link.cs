using System.ComponentModel.DataAnnotations;

namespace URLShortener.Data.Entities
{
    public class Link
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string OriginalURL { get; set; }

        [Required]
        public string ShortenedURL { get; set; }

        [Required]
        public int UserId { get; set; }

        public User User { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
