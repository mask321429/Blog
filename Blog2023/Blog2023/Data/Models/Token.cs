using System.ComponentModel.DataAnnotations;

namespace Blog.Data.Models
{
    public class Token
    {

        [Required]
        public string? InvalidToken { get; set; }
        [Required]
        public DateTime ExpiredDate { get; set; }

    }
}
