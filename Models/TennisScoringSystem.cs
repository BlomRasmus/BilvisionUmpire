namespace BilvisionUmpire.Models
{
    public class TennisScoringSystem
    {
        public Dictionary<int, string> TennisScoresCallout { get; set; } = new Dictionary<int, string>()
        {
            { 0, "Love" },
            { 1, "Fifteen" },
            { 2, "Thirty" },
            { 3, "Forty" },
            { 4, "Game" },
            { 5, "Advantage" },
            { 6, "Deuce" }
        };


    }
}
