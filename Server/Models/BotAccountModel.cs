using System.ComponentModel.DataAnnotations;

namespace Server.Models
{
    public class BotAccountModel
    {
        [Required(ErrorMessage = "Your bot account needs a name. What about 'Tim'?")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Your bot account needs an avatar URL, say cheese!")]
        [Url(ErrorMessage = "Your creativity is appreciated, however, we need a valid URL.")]
        public string AvatarUrl { get; set; }

        [Required(ErrorMessage = "Your bot account needs a token. You can input random stuff, but then you know... no Discord.")]
        public string Token { get; set; }
    }
}
