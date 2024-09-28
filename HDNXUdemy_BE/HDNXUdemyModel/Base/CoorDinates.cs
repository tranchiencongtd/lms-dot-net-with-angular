namespace HDNXUdemyModel.Base
{
    public class CoorDinates
    {
        public double Latitude { get; private set; }
        public double Longitude { get; private set; }

        public CoorDinates(double latitude, double longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }
    }

    public class UnitOfLength
    {
        public static UnitOfLength Kilometers = new(1.609344);
        public static UnitOfLength NauticalMiles = new(0.8684);
        public static UnitOfLength Miles = new(1);

        private readonly double _fromMilesFactor;

        private UnitOfLength(double fromMilesFactor)
        {
            _fromMilesFactor = fromMilesFactor;
        }

        public double ConvertFromMiles(double input)
        {
            return input * _fromMilesFactor;
        }
    }
}