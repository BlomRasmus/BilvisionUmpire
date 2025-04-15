using System.ComponentModel.DataAnnotations;

namespace BilvisionUmpire.Models
{
    public class Player
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        public int Score { get; set; }
        public int GameScore { get; set; }
        public int SetScore { get; set; }
        public bool IsServing { get; set; }
    }
}
