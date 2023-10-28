using JaguarGame.Models.Interfaces;

namespace JaguarGame.Models;

public struct JaguarMove : IMove
{
    public PlaceRef newPoss { get; init; }
    public bool isFatal => killDogOnBoardIndex is not null;
    public int? killDogOnBoardIndex { get; init; }
}