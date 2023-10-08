namespace JaguarGame.GameInterface;

public interface ILegalMoves<TMove>
{
    /// <summary>
    /// Return a list of legal moves for the current gamestate.
    /// </summary>
    List<TMove> GetLegalMoves();
}