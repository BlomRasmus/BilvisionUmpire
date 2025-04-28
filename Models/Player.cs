using System.ComponentModel.DataAnnotations;

namespace BilvisionUmpire.Models
{
    public class Player
    {
        [Required]
        public string? Name { get; set; }
        public string DisplayedScore { get; set; } = "Love";
        public int Score { get; set; }
        public int GameScore { get; set; }
        public int SetScore { get; set; }
        public bool IsServing { get; set; }
    }
}
