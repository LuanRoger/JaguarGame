using JaguarGame.BoardDefinitions;
using JaguarGame.Models;
using JaguarGame.Models.Enums;
using JaguarGame.Models.Interfaces;

namespace JaguarGame;

public class Board : ICloneable
{
    private BoardDefinition boardDefinition { get; }
    private List<Place> places { get; }
    public PlaceRef jagPoss { get; private set; }
    public List<PlaceRef> dogsPoss { get; }

    public Board(BoardDefinition boardDefinition)
    {
        jagPoss = boardDefinition.jaguarPossition;
        dogsPoss = boardDefinition.dogsPossitions;
        places = boardDefinition.boardPlaces;
        this.boardDefinition = boardDefinition;
    }
    
    private int GetDogIndexByPoss(PlaceRef poss) => dogsPoss.IndexOf(poss);
    public IEnumerable<JaguarMove> GetPossibleMovesToJaguar(bool includeFatals = true)
    {
        List<JaguarMove> possibleMoves = new();
        var freePoss = places.Find(place => place.@ref == jagPoss)!.connections
            .Where(place => IsValidToGoTo(place, jagPoss))
            .Select(freePlace => new JaguarMove
            {
                killDogOnBoardIndex = null,
                newPoss = freePlace
            })
            .ToList();
        possibleMoves.AddRange(freePoss);
        if(!includeFatals) return possibleMoves;
        
        var possibleEats = GetDogsAdjacentToJaguar();
        foreach (PlaceRef possibleEatDogPoss in possibleEats)
        {
            int possDifAbs = Math.Abs(jagPoss.id - possibleEatDogPoss.id);
            bool dogIsBeforeJag = jagPoss.id > possibleEatDogPoss.id;
            int futureJagPoss = dogIsBeforeJag ? possibleEatDogPoss.id - possDifAbs : possibleEatDogPoss.id + possDifAbs;
            
            int placeIndex = futureJagPoss - 1;
            if(placeIndex is < 0 or > 24) continue;
            PlaceRef futureJaguar = places[placeIndex];
            if(!IsValidToGoTo(futureJaguar, jagPoss, true)) continue;

            int deathDogIndex = GetDogIndexByPoss(possibleEatDogPoss);
            possibleMoves.Add(new()
            {
                killDogOnBoardIndex = deathDogIndex,
                newPoss = futureJaguar
            });
        }
        
        return possibleMoves;
    }
    public IEnumerable<DogMove> GetPossibleMovesToDogs()
    {
        int dogIndex = 0;
        List<DogMove> possibleMoves = new();
        foreach (PlaceRef dogPoss in dogsPoss)
        {
            int index = dogIndex;
            Place? dogPlaceMove = places.Find(place => place.@ref == dogPoss);
            if(dogPlaceMove is null) continue;
            var dogMoves = dogPlaceMove.connections
                .Where(place => IsValidToGoTo(place, dogPoss))
                .Select(place => new DogMove
                {
                    dogIndex = index,
                    newPoss = place
                })
                .ToList();
            possibleMoves.AddRange(dogMoves);
            dogIndex++;
        }
        
        return possibleMoves;
    }
    
    private bool IsValidToGoTo(PlaceRef place, PlaceRef currentPoss, bool isFatalMove = false)
    {
        bool isEmptyToGo = place != jagPoss && !dogsPoss.Contains(place);
        bool isAdjacent = places.Find(p => p.@ref == currentPoss)?.connections
            .Contains(place) ?? false;
        
        return isEmptyToGo && (isAdjacent || isFatalMove);
    }
    private IEnumerable<PlaceRef> GetDogsAdjacentToJaguar() =>
        places.Find(place => place.@ref == jagPoss)!.connections
            .Where(place => dogsPoss.Contains(place))
            .ToList();
    
    public void Move(IMove move)
    {
        switch (move)
        {
            case JaguarMove jaguarMove:
                MoveJaguar(jaguarMove);
                break;
            case DogMove dogMove:
                MoveDog(dogMove);
                break;
        }
    }
    private void MoveJaguar(JaguarMove move)
    {
        if(!IsValidToGoTo(move.newPoss, jagPoss, move.isFatal)) return;
        
        if(move is { isFatal: true, killDogOnBoardIndex: not null })
            dogsPoss.RemoveAt(move.killDogOnBoardIndex.Value);
        
        jagPoss = move.newPoss;
    }
    private void MoveDog(DogMove move)
    {
        PlaceRef dogCurrentPoss = dogsPoss[move.dogIndex];
        if(!IsValidToGoTo(move.newPoss, dogCurrentPoss)) return;
        
        dogsPoss[move.dogIndex] = move.newPoss;
    }
    
    public bool IsGameOver(out Winner? winner)
    {
        bool dogsWin = !GetPossibleMovesToJaguar(false).Any();
        bool jaguarWin = dogsPoss.Count <= 6;
        winner = dogsWin ? Winner.DogWinner : jaguarWin ? Winner.JaguarWinner : null;
        
        return dogsWin || jaguarWin;
    }
    public float GetStateScore()
    {
        float stateSocre;
        
        //Pontuação da onça
        float freeSpacesToGo = GetPossibleMovesToJaguar().Count() / 4f;
        float dogsLived = 14f * 3 / dogsPoss.Count;
        float jaguarScore = freeSpacesToGo * dogsLived;
        
        //Pontuação dos cachorros
        float possibleMoves = GetPossibleMovesToDogs().Count() / 3f;
        float possiblePeacefulMoves = 4f / GetPossibleMovesToJaguar(false).Count();
        float dogsAdjacentToJaguar = GetDogsAdjacentToJaguar().Count();
        float dogsScore = -(possibleMoves + dogsAdjacentToJaguar + possiblePeacefulMoves);
        stateSocre = jaguarScore + dogsScore;
        
        Random rng = new();
        bool hasSignal = rng.Next(0, 2) == 1;
        float rngFactor = rng.Next(0, 10) / 10f;
        
        stateSocre += hasSignal ? -rngFactor : rngFactor;
        
        return stateSocre;
    }
    
    public object Clone()
    {
        var boardPlacesClone = places
            .Select(place => (Place)place.Clone())
            .ToList();
        PlaceRef jagPossClone = (PlaceRef)jagPoss.Clone();
        var dogsPossClone = dogsPoss
            .Select(dogPoss => (PlaceRef)dogPoss.Clone())
            .ToList();
        BoardDefinition boardDefinitionClone = new()
        {
            boardPlaces = boardPlacesClone,
            jaguarPossition = jagPossClone,
            dogsPossitions = dogsPossClone
        };
        
        return new Board(boardDefinitionClone);
    }
}