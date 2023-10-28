using JaguarGame.Models.Interfaces;

namespace JaguarGame.Models;

public struct DogMove : IMove
{
    public PlaceRef newPoss { get; init; }
    public int dogIndex { get; init; }
}