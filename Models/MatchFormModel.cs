using System.ComponentModel.DataAnnotations;

namespace BilvisionUmpire.Models
{
    public class MatchFormModel
    {
        [Required]
        public Player Player1 { get; set; } = new();

        [Required]
        public Player Player2 { get; set; } = new();

        [Required]
        public int SetsInMatch { get; set; } = 3;
    }
}
