using JaguarGame.Models.Enums;
using JaguarGame.Models.EventsArgs;

namespace JaguarGame.Models;

public class Game
{
    private Player jaguar { get; }
    private Player dogs { get; }
    private Board board { get; set; }
    private int round { get; set; }

    public Game(Player jaguar, Player dogs)
    {
        this.jaguar = jaguar;
        this.dogs = dogs;
        board = new();
    }
    
    public delegate void OnMoveEventHandler(object sender, MoveEventArgs e);
    public event OnMoveEventHandler? OnMove;
    public delegate void OnRoundEndedEventHandler(object sender, RoundEndedEventArgs e);
    public event OnRoundEndedEventHandler? OnRoundEnded;
    
    public Winner? Start()
    {
        const int maxDepth = 5;
        Winner? winner;
        while (true)
        {
            Board boardClone = (Board)board.Clone();
            float maxPlayerScore = MinMax(boardClone, int.MinValue, int.MaxValue, out IMove? maxPlayerMove, true, maxDepth);
            if(boardClone.IsGameOver(out winner) || maxPlayerMove is null)
                break;
            boardClone.Move(maxPlayerMove);
            OnMove?.Invoke(this, new()
            {
                move = maxPlayerMove,
                player = jaguar,
                moveScore = maxPlayerScore,
                boardAfterMove = (Board)boardClone.Clone()
            });
            
            float minPlayerScore = MinMax(boardClone, int.MinValue, int.MaxValue, out IMove? minPlayerMove, false, maxDepth);
            if(boardClone.IsGameOver(out winner) || minPlayerMove is null)
                break;
            boardClone.Move(minPlayerMove);
            OnMove?.Invoke(this, new()
            {
                move = minPlayerMove,
                player = dogs,
                moveScore = minPlayerScore,
                boardAfterMove = (Board)boardClone.Clone()
            });
            
            round++;
            board = boardClone;
            OnRoundEnded?.Invoke(this, new()
            {
                round = round,
                boardEvaluation = new()
                {
                    jaguarChangeToWin = Math.Abs(Math.Abs(maxPlayerScore) - Math.Abs(minPlayerScore)) / Math.Abs(minPlayerScore),
                    dogsChangeToWin = Math.Abs(Math.Abs(minPlayerScore) - Math.Abs(maxPlayerScore)) / Math.Abs(maxPlayerScore),
                },
                jagPoss = board.jagPoss,
                dogsAliveCount = board.dogsPoss.Count
            });
        }
        
        return winner;
    }
    private float MinMax(Board minMaxBoard, float alpha, float beta, out IMove? move, bool maxPlayer, int depth, bool getFromActualMoveSet = true)
    {
        move = null;
        if(depth == 0 || minMaxBoard.IsGameOver(out _))
            return minMaxBoard.GetStateScore();
        
        if(maxPlayer)
        {
            float score = float.MinValue;
            var possibleMoves = minMaxBoard.GetPossibleMovesToJaguar();
            foreach (JaguarMove jaguarMove in possibleMoves)
            {
                Board boardClone = (Board)minMaxBoard.Clone();
                boardClone.Move(jaguarMove);
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
            var possibleMoves = minMaxBoard.GetPossibleMovesToDogs();
            foreach (DogMove dogMove in possibleMoves)
            {
                Board boardClone = (Board)minMaxBoard.Clone();
                boardClone.Move(dogMove);
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