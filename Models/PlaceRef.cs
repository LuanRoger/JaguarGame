﻿namespace JaguarGame.Models;

public readonly struct PlaceRef : IEquatable<PlaceRef>
{
    public override int GetHashCode() => id;

    public int id { get; init; }
    
    public static bool operator ==(PlaceRef a, PlaceRef b) => a.id == b.id;
    public static bool operator !=(PlaceRef a, PlaceRef b) => a.id != b.id;
    public bool Equals(PlaceRef other) => id == other.id;
    public override bool Equals(object? obj) => obj is PlaceRef other && Equals(other);
}