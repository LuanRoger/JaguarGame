using JaguarGame.Models.Interfaces;

namespace JaguarGame.Models.EventsArgs;

public class MoveEventArgs : EventArgs
{
    public required IMove move { get; init; }
    public required Player lastPlayerToMove { get; init; }
    public required float moveScore { get; init; }
    public required Board boardAfterMove { get; init; }
    public required BoardMoveSet currentBoardMoveSet { get; init; }
    
    public class BoardMoveSet
    {
        public required PlaceRef jaguarCurrentPoss { get; init; }
        public required IEnumerable<JaguarMove> jaguarMoves { get; init; }
        public required IEnumerable<DogMove> dogsMoves { get; init; }
    }
}