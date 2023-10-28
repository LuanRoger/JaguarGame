using JaguarGame.Models.Interfaces;

namespace JaguarGame.Models.EventsArgs;

public class MoveEventArgs : EventArgs
{
    public required IMove move { get; init; }
    public required Player player { get; init; }
    public required float moveScore { get; init; }
    public required Board boardAfterMove { get; init; }
}