using BilvisionUmpire.Models;
using System.Reflection.Metadata.Ecma335;

namespace BilvisionUmpire.Services
{
    public class TennisGame
    {
        private readonly MatchService matchService = new();
        private readonly TennisScoringSystem tennisScoringSystem = new();
        private readonly SetService setService = new();
        public required Player Player1 { get; set; }
        public required Player Player2 { get; set; }
        public int SetsToPlay { get; set; }
        public bool HasWinner { get; set; }


        public void PlayerScores(Player player) 
        {
            player.Score++;
        }
        public void RuleOut(Player player)
        {
            player.Score--;
        }

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
                {
                    servingPlayer.DisplayedScore = tennisScoringSystem.PlayerDisplayedScores[6];
                    recievingPlayer.DisplayedScore = tennisScoringSystem.PlayerDisplayedScores[6];

                    return tennisScoringSystem.TennisScoresCallout[6];
                }

                if (servingPlayer.Score == recievingPlayer.Score + 1)
                {
                    servingPlayer.DisplayedScore = tennisScoringSystem.PlayerDisplayedScores[5];
                    return $"{tennisScoringSystem.TennisScoresCallout[5]} {servingPlayer.Name}";
                }

                if (recievingPlayer.Score == servingPlayer.Score + 1)
                {
                    recievingPlayer.DisplayedScore = tennisScoringSystem.PlayerDisplayedScores[5];
                    return $"{tennisScoringSystem.TennisScoresCallout[5]} {recievingPlayer.Name}";
                }
            }

            if (servingPlayer.Score == recievingPlayer.Score)
            {
                servingPlayer.DisplayedScore = tennisScoringSystem.PlayerDisplayedScores[servingPlayer.Score];
                recievingPlayer.DisplayedScore = tennisScoringSystem.PlayerDisplayedScores[recievingPlayer.Score];

                return $"{tennisScoringSystem.TennisScoresCallout[servingPlayer.Score]} - all";
            }

            if(servingPlayer.Score >= 4 || recievingPlayer.Score >= 4)
            {
                if (Math.Abs(servingPlayer.Score - recievingPlayer.Score) >= 2)
                {
                    var (gameWinner, gameLoser) = servingPlayer.Score > recievingPlayer.Score ? (servingPlayer, recievingPlayer) : (recievingPlayer, servingPlayer);
                    string response = GameWon(gameWinner, gameLoser);

                    ResetScore();
                    ChangeServer(servingPlayer, recievingPlayer);

                    return response;
                }
            }

            servingPlayer.DisplayedScore = tennisScoringSystem.PlayerDisplayedScores[servingPlayer.Score];
            recievingPlayer.DisplayedScore = tennisScoringSystem.PlayerDisplayedScores[recievingPlayer.Score];

            return $"{tennisScoringSystem.TennisScoresCallout[servingPlayer.Score]} - {tennisScoringSystem.TennisScoresCallout[recievingPlayer.Score]}";
        }


        public void ResetScore()
        {
            Player1.Score = 0;
            Player2.Score = 0;

            Player1.DisplayedScore = "Love";
            Player2.DisplayedScore = "Love";
        }
        public void ChangeServer(Player servingPlayer, Player recievingPlayer)
        {
            servingPlayer.IsServing = false;
            recievingPlayer.IsServing = true;
        }
        public string GameWon(Player gameWinner, Player gameLoser)
        {
            gameWinner.GameScore++;

            if (setService.IsSetWon(gameWinner.GameScore, gameLoser.GameScore))
            {
                setService.SetWon(gameWinner, gameLoser);

                HasWinner = matchService.IsMatchWon(gameWinner, SetsToPlay);

                return HasWinner == true ? $"Game, Set, Match {gameWinner.Name}" : $"Set {gameWinner.Name}";
            }

            return $"Game {gameWinner.Name}";
        }
    }
}
