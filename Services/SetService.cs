using BilvisionUmpire.Models;

namespace BilvisionUmpire.Services
{
    public class SetService
    {
        public bool IsSetWon(int gameWinnerScore, int gameLoserScore)
        {
            return gameWinnerScore >= 6 && Math.Abs(gameWinnerScore - gameLoserScore) >= 2;
        }

        public void SetWon(Player winner, Player loser)
        {
            winner.SetScore++;

            winner.GameScore = 0;
            winner.GameScore = 0;
        }
    }
}
