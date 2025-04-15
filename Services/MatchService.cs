using BilvisionUmpire.Models;

namespace BilvisionUmpire.Services
{
    public class MatchService
    {
        public bool IsMatchWon(Player setWinner, int setsInMatch)
        {
            return setWinner.SetScore > setsInMatch / 2;
        }
    }
}
