namespace JaguarGame.Models.EventsArgs;

public class StartRoundEventArgs : EventArgs
{
    public IEnumerable<JaguarMove> jaguarMoves { get; init; }
    public IEnumerable<DogMove> dogsMoves { get; init; }
}