namespace JaguarGame.Models;

public class Place : ICloneable, IEquatable<Place>
{
    public PlaceRef @ref { get; }
    public List<PlaceRef> connections { get; }

    public Place(int id)
    {
        @ref = new()
        {
            id = id
        };
        connections = new();
    }
    
    public void Connect(PlaceRef newConnection)
    {
        connections.Add(newConnection);
    } 
    public void Connect(IEnumerable<PlaceRef> newConnections)
    {
        connections.AddRange(newConnections);
    }
    
    public object Clone()
    {
        Place clone = new(@ref.id);
        clone.Connect(connections);
        return clone;
    }
    public override int GetHashCode()
    {
        return HashCode.Combine(@ref, connections);
    }
    
    public static bool operator ==(Place place1, Place place2) => place1.@ref == place2.@ref;
    public static bool operator !=(Place place1, Place place2) => place1.@ref != place2.@ref;
    
    public static implicit operator PlaceRef(Place place) => place.@ref;
    
    public bool Equals(Place? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return @ref.Equals(other.@ref) && connections.Equals(other.connections);
    }
    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((Place)obj);
    }
}