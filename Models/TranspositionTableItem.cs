namespace JaguarGame.Models;

public class TranspositionTableItem
{
    public int score { get; init; }
    public int depth { get; init; }
    public int alpha { get; init; }
    public int beta { get; init; }
    public NodeType type { get; init; }

    public enum NodeType
    {
        Exact,
        LowerBound,
        UpperBound
    }
}