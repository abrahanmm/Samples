namespace NFLGameWeather.Model
{
    /// <summary>
    /// Represents a stadium where play a NFL's team.
    /// </summary>
    internal class Stadium
    {
        // List of NFL's stdiums
        internal static Stadium U_of_Phoenix_Stadium = new Stadium("U of Phoenix Stadium", "Glendale", 63400, 33.527558f, -112.263036f, StadiumType.RetractableDome);
        internal static Stadium Mercedes_Benz_Stadium = new Stadium("Mercedes-Benz Stadium", "Atlanta", 71000, 33.755f, -84.401f, StadiumType.RetractableDome);
        internal static Stadium MT_Bank_Stadium = new Stadium("M&T Bank Stadium", "Baltimore", 71008, 39.277985f, -76.622788f, StadiumType.Outdoor);
        internal static Stadium New_Era_Field = new Stadium("New Era Field", "Orchard Park", 73079, 42.773758f, -78.786837f, StadiumType.Outdoor);
        internal static Stadium Bank_of_America_Stadium = new Stadium("Bank of America Stadium", "Charlotte", 73778, 35.225808f, -80.852861f, StadiumType.Outdoor);
        internal static Stadium Soldier_Field = new Stadium("Soldier Field", "Chicago", 61500, 41.86232f, -87.616699f, StadiumType.Outdoor);
        internal static Stadium Paul_Brown_Stadium = new Stadium("Paul Brown Stadium", "Cincinnati", 65535, 39.095309f, -84.516003f, StadiumType.Outdoor);
        internal static Stadium FirstEnergy_Stadium = new Stadium("FirstEnergy Stadium", "Cleveland", 71516, 41.505885f, -81.699458f, StadiumType.Outdoor);
        internal static Stadium ATT_Stadium = new Stadium("AT&T Stadium", "Arlington", 80000, 32.747778f, -97.092778f, StadiumType.RetractableDome);
        internal static Stadium Broncos_Stadium_at_Mile_High = new Stadium("Broncos Stadium at Mile High", "Denver", 76125, 39.743936f, -105.020097f, StadiumType.Outdoor);
        internal static Stadium Ford_Field = new Stadium("Ford Field", "Detroit", 65000, 42.340021f, -83.045777f, StadiumType.Dome);
        internal static Stadium Lambeau_Field = new Stadium("Lambeau Field", "Green Bay", 80750, 44.501389f, -88.061944f, StadiumType.Outdoor);
        internal static Stadium NRG_Stadium = new Stadium("NRG Stadium", "Houston", 71054, 29.684722f, -95.410707f, StadiumType.RetractableDome);
        internal static Stadium Lucas_Oil_Stadium = new Stadium("Lucas Oil Stadium", "Indianapolis", 67000, 39.760056f, -86.163806f, StadiumType.RetractableDome);
        internal static Stadium TIAA_Bank_Field = new Stadium("TIAA Bank Field", "Jacksonville", 67246, 30.323813f, -81.637199f, StadiumType.Outdoor);
        internal static Stadium Arrowhead_Stadium = new Stadium("Arrowhead Stadium", "Kansas City", 76416, 39.049002f, -94.483864f, StadiumType.Outdoor);
        internal static Stadium StubHub_Center = new Stadium("StubHub Center", "Carson", 27000, 33.858663f, -118.256666f, StadiumType.Outdoor);
        internal static Stadium Los_Angeles_Memorial_Coliseum = new Stadium("Los Angeles Memorial Coliseum", "Los Angeles", 93607, 34.014167f, -118.287778f, StadiumType.Outdoor);
        internal static Stadium Hard_Rock_Stadium = new Stadium("Hard Rock Stadium", "Miami Gardens", 76100, 25.95774f, -80.238781f, StadiumType.Outdoor);
        internal static Stadium US_Bank_Stadium = new Stadium("U.S. Bank Stadium", "Minneapolis", 66655, 44.974288f, -93.329728f, StadiumType.Dome);
        internal static Stadium Gillette_Stadium = new Stadium("Gillette Stadium", "Foxborough", 68756, 42.090866f, -71.264244f, StadiumType.Outdoor);
        internal static Stadium Mercedes_Benz_Superdome = new Stadium("Mercedes-Benz Superdome", "New Orleans", 73208, 29.950931f, -90.081364f, StadiumType.Dome);
        internal static Stadium MetLife_Stadium = new Stadium("MetLife Stadium", "East Rutherford", 82500, 40.813611f, -74.074444f, StadiumType.Outdoor);
        internal static Stadium Oakland_Coliseum = new Stadium("Oakland Coliseum", "Oakland", 53200, 37.751613f, -122.200509f, StadiumType.Outdoor);
        internal static Stadium Lincoln_Financial_Field = new Stadium("Lincoln Financial Field", "Philadelphia", 68532, 39.900771f, -75.167469f, StadiumType.Outdoor);
        internal static Stadium Heinz_Field = new Stadium("Heinz Field", "Pittsburgh", 65050, 40.446751f, -80.015707f, StadiumType.Outdoor);
        internal static Stadium CenturyLink_Field = new Stadium("CenturyLink Field", "Seattle", 67000, 47.595153f, -122.331625f, StadiumType.Outdoor);
        internal static Stadium Levis_Stadium = new Stadium("Levi's Stadium", "Santa Clara", 68500, 37.404108f, -121.970274f, StadiumType.Outdoor);
        internal static Stadium Raymond_James_Stadium = new Stadium("Raymond James Stadium", "Tampa Bay", 65890, 27.975967f, -82.50335f, StadiumType.Outdoor);
        internal static Stadium Nissan_Stadium = new Stadium("Nissan Stadium", "Nashville", 6914, 36.166441f, -86.771253f, StadiumType.Outdoor);
        internal static Stadium FedEx_Field = new Stadium("FedEx Field", "Landover", 85000, 38.907652f, -76.864479f, StadiumType.Outdoor);

        /// <summary>
        /// Initializes a new instance of a <see cref="Stadium"/> class.
        /// </summary>
        /// <param name="name">Name of the stadium.</param>
        /// <param name="city">City of the stadium.</param>
        /// <param name="capacity">Capacity of the stadium.</param>
        /// <param name="geoLatitude">Geographical latitude.</param>
        /// <param name="geoLongitude">Geographical longitude.</param>
        /// <param name="type">Type of the stadium (open or covered).</param>
        private Stadium(string name, string city, int capacity, float geoLatitude, float geoLongitude, StadiumType type)
        {
            this.Name = name;
            this.City = city;
            this.Capacity = capacity;
            this.GeoLatitude = geoLatitude;
            this.GeoLongitude = geoLongitude;
            this.Type = type;
        }

        /// <summary>
        /// Name of the stadium.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// City of the stadium.
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// Capacity of the stadium.
        /// </summary>
        public int Capacity { get; set; }

        /// <summary>
        /// Geographical latitude.
        /// </summary>
        public float GeoLatitude { get; set; }

        /// <summary>
        /// Geographical longitude.
        /// </summary>
        public float GeoLongitude { get; set; }

        /// <summary>
        /// Type of the stadium (open or covered).
        /// </summary>
        public StadiumType Type { get; set; }
    }
}
