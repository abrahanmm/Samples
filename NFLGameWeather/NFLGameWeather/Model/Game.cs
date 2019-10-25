using System;
using System.Collections.Generic;

namespace NFLGameWeather.Model
{
    public class Game
    {
        public static IReadOnlyCollection<Game> Schedule { get; } = GenerateRandomGames();

        static Game[] GenerateRandomGames()
        {
            List<Team> teams = new List<Team>(Team.GetAll());
            Random random = new Random();
            List<Game> games = new List<Game>();
            List<Stadium> stadiums = new List<Stadium>(Stadium.GetAll());

            while (teams.Count > 0)
            {
                int randomNumber = random.Next(0, teams.Count - 1);
                Team awayTeam = teams[randomNumber];
                teams.RemoveAt(randomNumber);

                randomNumber = random.Next(0, teams.Count - 1);
                Team homeTeam = teams[randomNumber];
                teams.RemoveAt(randomNumber);

                randomNumber = random.Next(0, stadiums.Count - 1);
                Stadium stadium = stadiums[randomNumber];
                stadiums.RemoveAt(randomNumber);

                randomNumber = random.Next(1, 4);
                games.Add(new Game(1, DateTime.Now.AddDays(randomNumber), awayTeam, homeTeam, stadium));
            }

            return games.ToArray();
        }

        private Game(int week, DateTime date, Team awayTeam, Team homeTeam, Stadium stadium)
        {
            this.Week = week;
            this.Date = date;
            this.AwayTeam = awayTeam;
            this.HomeTeam = homeTeam;
            this.Stadium = stadium;
        }

        public int Week { get; }

        public DateTime Date { get; }

        public Team AwayTeam { get; }

        public Team HomeTeam { get; }

        public Stadium Stadium { get; }
    }
}
