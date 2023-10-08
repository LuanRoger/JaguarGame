namespace JaguarGame.Models;

public class Game
{
    public Board board { get; }
    public Player jaguar { get; }
    public Player dogs { get; }

    public Game()
    {
        board = new();
        jaguar = new("Jaguar");
        dogs = new("Dogs");
    }
    
    public void Start()
    {
        var initialJaguarMoves = board.GetPossibleMovesToJaguar();
        var initialDogMoves = board.GetPossibleMovesToDogs();
        var d = 0;
    }
}