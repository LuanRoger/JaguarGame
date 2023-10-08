﻿namespace JaguarGame.Models;

public class Board : ICloneable
{
    public List<Place> places { get; set; }
    public PlaceRef jagPoss { get; private set; }
    public PlaceRef[] dogsPoss { get; private set; }

    public Board()
    {
        Place place1 = new(1);
        Place place2 = new(2);
        Place place3 = new(3);
        Place place4 = new(4);
        Place place5 = new(5);
        Place place6 = new(6);
        Place place7 = new(7);
        Place place8 = new(8);
        Place place9 = new(9);
        Place place10 = new(10);
        Place place11 = new(11);
        Place place12 = new(12);
        Place place13 = new(13);
        Place place14 = new(14);
        Place place15 = new(15);
        Place place16 = new(16);
        Place place17 = new(17);
        Place place18 = new(18);
        Place place19 = new(19);
        Place place20 = new(20);
        Place place21 = new(21);
        Place place22 = new(22);
        Place place23 = new(23);
        Place place24 = new(24);
        Place place25 = new(25);
        
        place1.Connect(new PlaceRef[]{place2, place6, place7});
        place2.Connect(new PlaceRef[]{place1, place3, place7});
        place3.Connect(new PlaceRef[]{place2, place4, place7, place8, place9});
        place4.Connect(new PlaceRef[]{place3, place5, place9});
        place5.Connect(new PlaceRef[]{place4, place9, place10});
        place6.Connect(new PlaceRef[]{place1, place7, place11});
        place7.Connect(new PlaceRef[]{place1, place2, place3, place6, place8, place11, place12, place13});
        place8.Connect(new PlaceRef[]{place3, place7, place9, place13});
        place9.Connect(new PlaceRef[]{place3, place4, place5, place8, place10, place13, place14, place15});
        place10.Connect(new PlaceRef[]{place5, place8, place15});
        place11.Connect(new PlaceRef[]{place6, place7, place12, place16, place17});
        place12.Connect(new PlaceRef[]{place7, place11, place13, place17});
        place13.Connect(new PlaceRef[]{place7, place8, place9, place12, place14, place17, place18, place19});
        place14.Connect(new PlaceRef[]{place9, place13, place15, place19});
        place15.Connect(new PlaceRef[]{place9, place10, place14, place19, place20});
        place16.Connect(new PlaceRef[]{place11, place17, place21});
        place17.Connect(new PlaceRef[]{place11, place12, place13, place16, place18, place21, place22, place23});
        place18.Connect(new PlaceRef[]{place13, place17, place19, place23});
        place19.Connect(new PlaceRef[]{place13, place14, place15, place18, place20, place23, place24, place25});
        place20.Connect(new PlaceRef[]{place15, place19, place25});
        place21.Connect(new PlaceRef[]{place16, place17, place22});
        place22.Connect(new PlaceRef[]{place17, place21, place23});
        place23.Connect(new PlaceRef[]{place17, place18, place19, place22, place24});
        place24.Connect(new PlaceRef[]{place19, place23, place25});
        place25.Connect(new PlaceRef[]{place19, place18, place24});
        
        places = new()
        {
            place1,
            place2,
            place3,
            place4,
            place5,
            place6,
            place7,
            place8,
            place9,
            place10,
            place11,
            place12,
            place13,
            place14,
            place15,
            place16,
            place17,
            place18,
            place19,
            place20,
            place21,
            place22,
            place23,
            place24,
            place25
        };
        jagPoss = place13;
        dogsPoss = new PlaceRef[]{place1, place2, place3, place4, place5, place6, place7,
            place8, place9, place10, place11, place12, place14, place5};
    }
    public Board(PlaceRef jagPoss, PlaceRef[] dogsPoss)
    {
        Place place1 = new(1);
        Place place2 = new(2);
        Place place3 = new(3);
        Place place4 = new(4);
        Place place5 = new(5);
        Place place6 = new(6);
        Place place7 = new(7);
        Place place8 = new(8);
        Place place9 = new(9);
        Place place10 = new(10);
        Place place11 = new(11);
        Place place12 = new(12);
        Place place13 = new(13);
        Place place14 = new(14);
        Place place15 = new(15);
        Place place16 = new(16);
        Place place17 = new(17);
        Place place18 = new(18);
        Place place19 = new(19);
        Place place20 = new(20);
        Place place21 = new(21);
        Place place22 = new(22);
        Place place23 = new(23);
        Place place24 = new(24);
        Place place25 = new(25);
        
        place1.Connect(new PlaceRef[]{place2, place6, place7});
        place2.Connect(new PlaceRef[]{place1, place3, place7});
        place3.Connect(new PlaceRef[]{place2, place4, place7, place8, place9});
        place4.Connect(new PlaceRef[]{place3, place5, place9});
        place5.Connect(new PlaceRef[]{place4, place9, place10});
        place6.Connect(new PlaceRef[]{place1, place7, place11});
        place7.Connect(new PlaceRef[]{place1, place2, place3, place6, place8, place11, place12, place13});
        place8.Connect(new PlaceRef[]{place3, place7, place9, place13});
        place9.Connect(new PlaceRef[]{place3, place4, place5, place8, place10, place13, place14, place15});
        place10.Connect(new PlaceRef[]{place5, place8, place15});
        place11.Connect(new PlaceRef[]{place6, place7, place12, place16, place17});
        place12.Connect(new PlaceRef[]{place7, place11, place13, place17});
        place13.Connect(new PlaceRef[]{place7, place8, place9, place12, place14, place17, place18, place19});
        place14.Connect(new PlaceRef[]{place9, place13, place15, place19});
        place15.Connect(new PlaceRef[]{place9, place10, place14, place19, place20});
        place16.Connect(new PlaceRef[]{place11, place17, place21});
        place17.Connect(new PlaceRef[]{place11, place12, place13, place16, place18, place21, place22, place23});
        place18.Connect(new PlaceRef[]{place13, place17, place19, place23});
        place19.Connect(new PlaceRef[]{place13, place14, place15, place18, place20, place23, place24, place25});
        place20.Connect(new PlaceRef[]{place15, place19, place25});
        place21.Connect(new PlaceRef[]{place16, place17, place22});
        place22.Connect(new PlaceRef[]{place17, place21, place23});
        place23.Connect(new PlaceRef[]{place17, place18, place19, place22, place24});
        place24.Connect(new PlaceRef[]{place19, place23, place25});
        place25.Connect(new PlaceRef[]{place19, place18, place24});
        
        places = new()
        {
            place1,
            place2,
            place3,
            place4,
            place5,
            place6,
            place7,
            place8,
            place9,
            place10,
            place11,
            place12,
            place13,
            place14,
            place15,
            place16,
            place17,
            place18,
            place19,
            place20,
            place21,
            place22,
            place23,
            place24,
            place25
        };
        this.jagPoss = jagPoss;
        this.dogsPoss = dogsPoss;
    }
    
    public int GetDogIndexByPoss(PlaceRef poss) => Array.IndexOf(dogsPoss, poss);
    public IEnumerable<JaguarMove> GetPossibleMovesToJaguar()
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
        var possibleEats = GetDogsAdjacentToJaguar();
        foreach (PlaceRef possibleEatDogPoss in possibleEats)
        {
            int possDifAbs = Math.Abs(jagPoss.id - possibleEatDogPoss.id);
            bool dogIsBeforeJag = jagPoss.id > possibleEatDogPoss.id;
            int futureJagPoss = dogIsBeforeJag ? jagPoss.id - possDifAbs : jagPoss.id + possDifAbs;
            
            PlaceRef futureJaguar = places[futureJagPoss];
            if(!IsValidToGoTo(futureJaguar, jagPoss)) continue;

            int deathDogIndex = GetDogIndexByPoss(possibleEatDogPoss);
            possibleMoves.Add(new()
            {
                killDogOnBoardIndex = deathDogIndex,
                newPoss = futureJaguar
            });
        }
        
        possibleMoves.AddRange(freePoss);
        return possibleMoves;
    }
    public IEnumerable<DogMove> GetPossibleMovesToDogs()
    {
        int dogIndex = 0;
        List<DogMove> possibleMoves = new();
        foreach (PlaceRef dogPoss in dogsPoss)
        {
            var dogMoves = places.Find(place => place.@ref == dogPoss)!.connections
                .Where(place => IsValidToGoTo(place, dogPoss))
                .Select(place => new DogMove
                {
                    dogIndex = dogIndex,
                    newPoss = place
                })
                .ToList();
            possibleMoves.AddRange(dogMoves);
            dogIndex++;
        }
        
        return possibleMoves;
    }
    
    private bool IsValidToGoTo(PlaceRef place, PlaceRef currentPoss)
    {
        bool isEmptyToGo = place != jagPoss && !dogsPoss.Contains(place);
        bool isAdjacent = places.Find(p => p.@ref == currentPoss)!.connections.Contains(place);
        
        return isEmptyToGo && isAdjacent;
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
        if(!IsValidToGoTo(move.newPoss, jagPoss)) return;
        
        if(move is { isFatal: true, killDogOnBoardIndex: not null })
            dogsPoss[move.killDogOnBoardIndex.Value] = default;
        
        jagPoss = move.newPoss;
    }
    private void MoveDog(DogMove move)
    {
        PlaceRef dogCurrentPoss = dogsPoss[move.dogIndex];
        if(!IsValidToGoTo(move.newPoss, dogCurrentPoss)) return;
        
        dogsPoss[move.dogIndex] = move.newPoss;
    }
    
    public bool IsGameOver(out int winner)
    {
        bool dogsWin = !GetPossibleMovesToJaguar().Any();
        bool jaguarWin = dogsPoss.Count(p => p == default) == 6;
        if(dogsWin)
            winner = -1;
        else if(jaguarWin)
            winner = 1;
        else
            winner = 0;
        
        return dogsWin || jaguarWin;
    }
    public int? GetScore()
    {
        if(!IsGameOver(out int winner)) return null;
        
        return winner;
    }
    
    public object Clone() => new Board(jagPoss, dogsPoss);
}