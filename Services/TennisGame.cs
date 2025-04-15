using BilvisionUmpire.Models;

namespace BilvisionUmpire.Services
{
    public class TennisGame
    {
        private readonly TennisScoringSystem tennisScoringSystem = new();
        public required Player Player1 { get; set; }
        public required Player Player2 { get; set; }
        public int SetsToPlay { get; set; }


        public void PlayerScores(Player player) => player.Score++;

        public string GetScoresBasedOnServer()
        {
            var (server, receiver) = Player1.IsServing
                ? (Player1, Player2)
                : (Player2, Player1);

            return GetScores(server, receiver);
        }

        public string GetScores(Player servingPlayer, Player recievingPlayer)
        {
            if(servingPlayer.Score >= 3 && recievingPlayer.Score >= 3)
            {
                if(servingPlayer.Score == recievingPlayer.Score)
                    return tennisScoringSystem.TennisScoresCallout[6];

                if (servingPlayer.Score == recievingPlayer.Score + 1)
                    return $"{tennisScoringSystem.TennisScoresCallout[5]} {servingPlayer.Name}";

                if (recievingPlayer.Score == servingPlayer.Score + 1)
                    return $"{tennisScoringSystem.TennisScoresCallout[5]} {servingPlayer.Name}";
            }

            if (servingPlayer.Score == recievingPlayer.Score)
                return $"{tennisScoringSystem.TennisScoresCallout[servingPlayer.Score]} - all";

            if(Player1.Score > 4 || Player2.Score > 4)
            {
                if (Math.Abs(servingPlayer.Score - recievingPlayer.Score) >= 2)
                {
                    ResetGame();
                    return servingPlayer.Score > recievingPlayer.Score ? $"{tennisScoringSystem.TennisScoresCallout[4]} {servingPlayer.Name}" : $"{tennisScoringSystem.TennisScoresCallout[4]} {recievingPlayer.Name}";
                }
            }


            return $"{tennisScoringSystem.TennisScoresCallout[servingPlayer.Score]} - {tennisScoringSystem.TennisScoresCallout[recievingPlayer.Score]}";
        }

        public void RuleOut(Player player)
        {
            player.Score--;
        }

        public void ResetGame()
        {
            Player1.Score = 0;
            Player2.Score = 0;
        }
    }
}
