using LDST.Domain.Common.Models;

namespace LDST.Domain.Playground.ValueObjects;

public sealed class Location : ValueObject
{
    public string Name { get; }
    public string Address { get; }
    public string Country { get; }
    public string City { get; }
    public string State { get; }
    public string ZipCode { get; }
    public double Latitude { get; }
    public double Longitude { get; }

    private Location(string name, string address, string country, string city, string state, string zipCode, double latitude, double longitude)
    {
        Name = name;
        Address = address;
        Country = country;
        City = city;
        State = state;
        ZipCode = zipCode;
        Latitude = latitude;
        Longitude = longitude;
    }

    public static Location Create(string name, string address, string country, string city, string state, string zipCode, double latitude, double longitude)
    {
        // TODO: Enforce invariants
        return new Location(name, address, country, city, state, zipCode, latitude, longitude);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Name;
        yield return Address;
        yield return Country;
        yield return City;
        yield return State;
        yield return ZipCode;
        yield return Latitude;
        yield return Longitude;
    }
}