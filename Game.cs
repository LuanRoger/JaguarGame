using JaguarGame.BoardDefinitions;
using JaguarGame.Models;
using JaguarGame.Models.Enums;
using JaguarGame.Models.EventsArgs;
using JaguarGame.Models.Interfaces;

namespace JaguarGame;

public class Game
{
    private Player jaguar { get; }
    private Player dogs { get; }
    private Board board { get; set; }
    private GameState gameState { get; }
    
    private int maxDepth { get; }

    public Game(Player jaguar, Player dogs, IBoardDefinitionFactory boardFactory, int maxDepth = 5)
    {
        BoardDefinition boardDefinition = boardFactory.Create();
        
        this.jaguar = jaguar;
        this.dogs = dogs;
        this.maxDepth = maxDepth;
        
        board = new(boardDefinition);
        gameState = new();
    }
    
    public delegate void OnMoveEventHandler(object sender, MoveEventArgs e);
    public event OnMoveEventHandler? OnMove;
    public delegate void OnRoundEndedEventHandler(object sender, RoundEndedEventArgs e);
    public event OnRoundEndedEventHandler? OnRoundEnded;
    
    public Winner? Start()
    {
        Winner? winner;
        while (!board.IsGameOver(out winner))
        {
            Board boardClone = (Board)board.Clone();
            float maxPlayerScore = MaxMove(ref boardClone);
            
            if(boardClone.IsGameOver(out winner))
                break;
            float minPlayerScore = MinMove(ref boardClone);
            
            gameState.round++;
            board = boardClone;
            OnRoundEnded?.Invoke(this, new()
            {
                round = gameState.round,
                jagPoss = board.jagPoss,
                dogsAliveCount = board.dogsPoss.Count,
                movesCount = gameState.movementsCount,
                boardEvaluation = new()
                {
                    jaguarChangeToWin = Math.Abs(Math.Abs(maxPlayerScore) - Math.Abs(minPlayerScore)) / Math.Abs(minPlayerScore),
                    dogsChangeToWin = Math.Abs(Math.Abs(minPlayerScore) - Math.Abs(maxPlayerScore)) / Math.Abs(maxPlayerScore),
                },
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
        //Minimize the dogs's score
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
    
    private float MaxMove(ref Board boardRoundClone)
    {
        float maxPlayerScore = MinMax(boardRoundClone, int.MinValue, int.MaxValue,
            out IMove? maxPlayerMove, true, maxDepth);
        if(maxPlayerMove is null)
            return default;
        boardRoundClone.Move(maxPlayerMove);
        gameState.movementsCount++;
        OnMove?.Invoke(this, new()
        {
            move = maxPlayerMove,
            lastPlayerToMove = jaguar,
            moveScore = maxPlayerScore,
            boardAfterMove = (Board)boardRoundClone.Clone(),
            currentBoardMoveSet = new()
            {
                jaguarCurrentPoss = board.jagPoss,
                jaguarMoves = board.GetPossibleMovesToJaguar(),
                dogsMoves = board.GetPossibleMovesToDogs()
            }
        });
        
        return maxPlayerScore;
    }
    private float MinMove(ref Board boardRoundClone)
    {
        float minPlayerScore = MinMax(boardRoundClone, int.MinValue, int.MaxValue,
            out IMove? minPlayerMove, false, maxDepth);
        if(minPlayerMove is null)
            return default;
        boardRoundClone.Move(minPlayerMove);
        gameState.movementsCount++;
        OnMove?.Invoke(this, new()
        {
            move = minPlayerMove,
            lastPlayerToMove = dogs,
            moveScore = minPlayerScore,
            boardAfterMove = (Board)boardRoundClone.Clone(),
            currentBoardMoveSet = new()
            {
                jaguarCurrentPoss = board.jagPoss,
                jaguarMoves = board.GetPossibleMovesToJaguar(),
                dogsMoves = board.GetPossibleMovesToDogs()
            }
        });
        
        return minPlayerScore;
    }
}