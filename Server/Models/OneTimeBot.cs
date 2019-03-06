using System.ComponentModel.DataAnnotations;

namespace Server.Models
{
    public class OneTimeBot
    {
        [Required(ErrorMessage = "A bot token is required.")]
        public string Token { get; set; }
    }
}
