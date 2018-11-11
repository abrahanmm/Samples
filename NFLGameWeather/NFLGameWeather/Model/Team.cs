using System.Collections.Generic;

namespace NFLGameWeather.Model
{
    /// <summary>
    /// Represents a NFL team.
    /// </summary>
    internal class Team
    {
        //Enum of teams
        internal static Team Cardinals = new Team("ARI", "Cardinals", "Arizona", "Arizona Cardinals", Conference.NFC, Division.West, Stadium.U_of_Phoenix_Stadium);
        internal static Team Falcons = new Team("ATL", "Falcons", "Atlanta", "Atlanta Falcons", Conference.NFC, Division.South, Stadium.Mercedes_Benz_Stadium);
        internal static Team Ravens = new Team("BAL", "Ravens", "Baltimore", "Baltimore Ravens", Conference.AFC, Division.North, Stadium.MT_Bank_Stadium);
        internal static Team Bills = new Team("BUF", "Bills", "Buffalo", "Buffalo Bills", Conference.AFC, Division.East, Stadium.New_Era_Field);
        internal static Team Panthers = new Team("CAR", "Panthers", "Carolina", "Carolina Panthers", Conference.NFC, Division.South, Stadium.Bank_of_America_Stadium);
        internal static Team Bears = new Team("CHI", "Bears", "Chicago", "Chicago Bears", Conference.NFC, Division.North, Stadium.Soldier_Field);
        internal static Team Bengals = new Team("CIN", "Bengals", "Cincinnati", "Cincinnati Bengals", Conference.AFC, Division.North, Stadium.Paul_Brown_Stadium);
        internal static Team Browns = new Team("CLE", "Browns", "Cleveland", "Cleveland Browns", Conference.AFC, Division.North, Stadium.FirstEnergy_Stadium);
        internal static Team Cowboys = new Team("DAL", "Cowboys", "Dallas", "Dallas Cowboys", Conference.NFC, Division.East, Stadium.ATT_Stadium);
        internal static Team Broncos = new Team("DEN", "Broncos", "Denver", "Denver Broncos", Conference.AFC, Division.West, Stadium.Broncos_Stadium_at_Mile_High);
        internal static Team Lions = new Team("DET", "Lions", "Detroit", "Detroit Lions", Conference.NFC, Division.North, Stadium.Ford_Field);
        internal static Team Packers = new Team("GB", "Packers", "Green Bay", "Green Bay Packers", Conference.NFC, Division.North, Stadium.Lambeau_Field);
        internal static Team Texans = new Team("HOU", "Texans", "Houston", "Houston Texans", Conference.AFC, Division.South, Stadium.NRG_Stadium);
        internal static Team Colts = new Team("IND", "Colts", "Indianapolis", "Indianapolis Colts", Conference.AFC, Division.South, Stadium.Lucas_Oil_Stadium);
        internal static Team Jaguars = new Team("JAX", "Jaguars", "Jacksonville", "Jacksonville Jaguars", Conference.AFC, Division.South, Stadium.TIAA_Bank_Field);
        internal static Team Chiefs = new Team("KC", "Chiefs", "Kansas City", "Kansas City Chiefs", Conference.AFC, Division.West, Stadium.Arrowhead_Stadium);
        internal static Team Chargers = new Team("LAC", "Chargers", "Los Angeles", "Los Angeles Chargers", Conference.AFC, Division.West, Stadium.StubHub_Center);
        internal static Team Rams = new Team("LAR", "Rams", "Los Angeles", "Los Angeles Rams", Conference.NFC, Division.West, Stadium.Los_Angeles_Memorial_Coliseum);
        internal static Team Dolphins = new Team("MIA", "Dolphins", "Miami", "Miami Dolphins", Conference.AFC, Division.East, Stadium.Hard_Rock_Stadium);
        internal static Team Vikings = new Team("MIN", "Vikings", "Minnesota", "Minnesota Vikings", Conference.NFC, Division.North, Stadium.US_Bank_Stadium);
        internal static Team Patriots = new Team("NE", "Patriots", "New England", "New England Patriots", Conference.AFC, Division.East, Stadium.Gillette_Stadium);
        internal static Team Saints = new Team("NO", "Saints", "New Orleans", "New Orleans Saints", Conference.NFC, Division.South, Stadium.Mercedes_Benz_Superdome);
        internal static Team Giants = new Team("NYG", "Giants", "New York", "New York Giants", Conference.NFC, Division.East, Stadium.MetLife_Stadium);
        internal static Team Jets = new Team("NYJ", "Jets", "New York", "New York Jets", Conference.AFC, Division.East, Stadium.MetLife_Stadium);
        internal static Team Raiders = new Team("OAK", "Raiders", "Oakland", "Oakland Raiders", Conference.AFC, Division.West, Stadium.Oakland_Coliseum);
        internal static Team Eagles = new Team("PHI", "Eagles", "Philadelphia", "Philadelphia Eagles", Conference.NFC, Division.East, Stadium.Lincoln_Financial_Field);
        internal static Team Steelers = new Team("PIT", "Steelers", "Pittsburgh", "Pittsburgh Steelers", Conference.AFC, Division.North, Stadium.Heinz_Field);
        internal static Team Seahawks = new Team("SEA", "Seahawks", "Seattle", "Seattle Seahawks", Conference.NFC, Division.West, Stadium.CenturyLink_Field);
        internal static Team Fortininers = new Team("SF", "49ers", "San Francisco", "San Francisco 49ers", Conference.NFC, Division.West, Stadium.Levis_Stadium);
        internal static Team Buccaneers = new Team("TB", "Buccaneers", "Tampa Bay", "Tampa Bay Buccaneers", Conference.NFC, Division.South, Stadium.Raymond_James_Stadium);
        internal static Team Titans = new Team("TEN", "Titans", "Tennessee", "Tennessee Titans", Conference.AFC, Division.South, Stadium.Nissan_Stadium);
        internal static Team Redskins = new Team("WAS", "Redskins", "Washington", "Washington Redskins", Conference.NFC, Division.East, Stadium.FedEx_Field);

        /// <summary>
        /// Initializes a new instance of a <see cref="Team"/> class.
        /// </summary>
        /// <param name="key">Key.</param>
        /// <param name="name">Team's name.</param>
        /// <param name="city">Team's city.</param>
        /// <param name="fullName">Full name of the team (city + name).</param>
        /// <param name="conference">Team's conference.</param>
        /// <param name="division">Team's division.</param>
        /// <param name="stadium">Stadium where the team play as local.</param>
        private Team(string key, string name, string city, string fullName, Conference conference, Division division, Stadium stadium)
        {
            this.Key = key;
            this.Name = name;
            this.City = city;
            this.FullName = fullName;
            this.Conference = this.Conference;
            this.Division = this.Division;
            this.Stadium = stadium;
        }

        /// <summary>
        /// Key.
        /// </summary>
        internal string Key { get; set; }

        /// <summary>
        /// Team's name.
        /// </summary>
        internal string Name { get; set; }

        /// <summary>
        /// Team's city.
        /// </summary>
        internal string City { get; set; }

        /// <summary>
        /// Full name of the team (city + name).
        /// </summary>
        internal string FullName { get; set; }

        /// <summary>
        /// Team's conference.
        /// </summary>
        internal Conference Conference { get; set; }

        /// <summary>
        /// Team's division.
        /// </summary>
        internal Division Division { get; set; }

        /// <summary>
        /// Stadium where the team play as local.
        /// </summary>
        internal Stadium Stadium { get; set; }

        public static IReadOnlyCollection<Team> GetTeams()
        {
            return new[]
            {
                Cardinals,
                Falcons,
                Ravens,
                Bills,
                Panthers,
                Bears,
                Bengals,
                Browns,
                Cowboys,
                Broncos,
                Lions,
                Packers,
                Texans,
                Colts,
                Jaguars,
                Chiefs,
                Chargers,
                Rams,
                Dolphins,
                Vikings,
                Patriots,
                Saints,
                Giants,
                Jets,
                Raiders,
                Eagles,
                Steelers,
                Seahawks,
                Fortininers,
                Buccaneers,
                Titans,
                Redskins
            };
        }
    }
}
