using JaguarGame.Models.Enums;

namespace JaguarGame.Models;

public class Game
{
    public Player jaguar { get; }
    public Player dogs { get; }

    public Game()
    {
        jaguar = new("Jaguar");
        dogs = new("Dogs");
    }
    
    public void Start()
    {
        const int maxDepth = 5;
        Board board = new();
        Winner? winner;
        int round = 0;
        while (!board.IsGameOver(out winner))
        {
            Board boardClone = (Board)board.Clone();
            float maxPlayerScore = MinMax(boardClone, int.MinValue, int.MaxValue, out IMove? maxPlayerMove, true, maxDepth);
            Console.WriteLine($"Max player get a branch with score: {maxPlayerScore}");
            boardClone.Move(maxPlayerMove!);
            
            float minPlayerScore = MinMax(boardClone, int.MinValue, int.MaxValue, out IMove? minPlayerMove, false, maxDepth);
            Console.WriteLine($"Min player get a branch with score: {minPlayerScore}");
            boardClone.Move(minPlayerMove!);
            
            round++;
            board = boardClone;
        }
        
        Console.WriteLine("The winner is: " + winner);
    }
    private float MinMax(Board board, float alpha, float beta, out IMove? move, bool maxPlayer, int depth, bool getFromActualMoveSet = true)
    {
        move = null;
        if(depth == 0 || board.IsGameOver(out _))
            return board.GetStateScore();
        
        if(maxPlayer)
        {
            float score = float.MinValue;
            var possibleMoves = board.GetPossibleMovesToJaguar();
            foreach (JaguarMove jaguarMove in possibleMoves)
            {
                Board boardClone = (Board)board.Clone();
                boardClone.Move(jaguarMove);
                Console.WriteLine("Analizando movimiento: ");
                boardClone.PrintBoard();
                float moveScore = Math.Max(score, MinMax(boardClone, alpha, beta, out _,
                    false, depth - 1, false));
                if(getFromActualMoveSet && moveScore > score)
                    move = jaguarMove;
                score = moveScore;
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
                Board boardClone = (Board)board.Clone();
                boardClone.Move(dogMove);
                Console.WriteLine("Analizando movimiento: ");
                boardClone.PrintBoard();
                Console.WriteLine("Depth: " + depth);
                float moveScore = Math.Min(score, MinMax(boardClone, alpha, beta, out _,
                    true, depth - 1, false));
                if(getFromActualMoveSet && moveScore < score)
                    move = dogMove;
                score = moveScore;
                if(score < alpha)
                    break;
                beta = beta < score ? beta : score;
            }
            return score;
        }
    }
}