using System.Text.Json.Serialization;

namespace DomainModelsCountry
{
    public class Country
    {
        public string Id { get; set; }
        public Name Name { get; set; }
        public string[] Tld { get; set; }
        public string Cca2 { get; set; }
        public string Ccn3 { get; set; }
        public string Cca3 { get; set; }
        public string Cioc { get; set; }
        public string[] Borders { get; set; }
        public bool Independent { get; set; }
        public string Status { get; set; }
        public bool UnMember { get; set; }
        public Dictionary<string, Currency> Currencies { get; set; }
        public Idd Idd { get; set; }
        public string[] Capital { get; set; }
        public string[] AltSpellings { get; set; }
        public string Region { get; set; }
        public string Subregion { get; set; }
        public Dictionary<string, string> Languages { get; set; }
        public object Translations { get; set; }
        public double[] Latlng { get; set; }
        public bool Landlocked { get; set; }
        public double Area { get; set; }
        public object Demonyms { get; set; }
        public string Flag { get; set; }
        public Maps Maps { get; set; }
        public int Population { get; set; }
        public string Fifa { get; set; }
        public Car Car { get; set; }
        public Gini Gini { get; set; }
        public string[] Timezones { get; set; }
        public string[] Continents { get; set; }
        public Flags Flags { get; set; }
        public CoatOfArms CoatOfArms { get; set; }
        public string StartOfWeek { get; set; }
        public CapitalInfo CapitalInfo { get; set; }
    }

    public class Name
    {
        public string Common { get; set; }
        public string Official { get; set; }
        public Dictionary<string, NativeName> NativeName { get; set; }
    }

    public class NativeName
    {
        public string Official { get; set; }
        public string Common { get; set; }
    }

    public class Currency
    {
        public string Name { get; set; }
        public string Symbol { get; set; }
    }

    public class Idd
    {
        public string Root { get; set; }
        public string[] Suffixes { get; set; }
    }

    public class Maps
    {
        public string GoogleMaps { get; set; }
        public string OpenStreetMaps { get; set; }
    }

    public class Car
    {
        public string[] Signs { get; set; }
        public string Side { get; set; }
    }

    public class Gini
    {
        public Dictionary<string, double> YearValues { get; set; }
    }

    public class Flags
    {
        public string Png { get; set; }
        public string Svg { get; set; }
        public string Alt { get; set; }
    }

    public class CoatOfArms
    {
        public string Png { get; set; }
        public string Svg { get; set; }
    }

    public class CapitalInfo
    {
        public double[] Latlng { get; set; }
    }
}