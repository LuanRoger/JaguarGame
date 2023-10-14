namespace JaguarGame.Models;

public class Game
{
    public Player jaguar { get; }
    public Player dogs { get; }
    private TranspositionTable transpositionTable;

    public Game()
    {
        jaguar = new("Jaguar");
        dogs = new("Dogs");
        transpositionTable = new();
    }
    
    public void Start()
    {
        transpositionTable = new();
        Board board = new();
        float d = MinMax(board, int.MinValue, int.MaxValue, true, 100);
        Console.WriteLine(d);
    }
    private float MinMax(Board board, float alpha, float beta, bool maxPlayer, int depth)
    {
        if(depth == 0 || board.IsGameOver())
            return board.GetStateScore();
        
        if(maxPlayer)
        {
            float score = float.MinValue;
            var possibleMoves = board.GetPossibleMovesToJaguar();
            foreach (JaguarMove jaguarMove in possibleMoves)
            {
                board.Move(jaguarMove);
                Console.WriteLine("Analizando movimiento: ");
                board.PrintBoard();
                score = Math.Max(score, MinMax(board, alpha, beta, false, depth - 1));
                if(score > beta)
                    break;
                alpha = alpha > score ? alpha : score;
            }
            return score;
        }
        //Minimize the dogs' score
        else
        {
            float score = float.MaxValue;
            var possibleMoves = board.GetPossibleMovesToDogs();
            foreach (DogMove dogMove in possibleMoves)
            {
                board.Move(dogMove);
                Console.WriteLine("Analizando movimiento: ");
                board.PrintBoard();
                Console.WriteLine("Depth: " + depth);
                score = Math.Min(score, MinMax(board, alpha, beta, true, depth - 1));
                if(score < alpha)
                    break;
                beta = beta < score ? beta : score;
            }
            return score;
        }
    }
}