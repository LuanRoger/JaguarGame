using JaguarGame.Models;

namespace JaguarGame.BoardDefinitions;

public class BoardDefinition : ICloneable
{
    public required List<Place> boardPlaces { get; init; }
    public required PlaceRef jaguarPossition { get; init; }
    public required List<PlaceRef> dogsPossitions { get; init; }
    
    public object Clone()
    {
        var boardPlacesClone = boardPlaces
            .Select(place => (Place)place.Clone())
            .ToList();
        PlaceRef jagPossClone = (PlaceRef)jaguarPossition.Clone();
        var dogsPossClone = dogsPossitions
            .Select(dogPoss => (PlaceRef)dogPoss.Clone())
            .ToList();
        
        return new BoardDefinition
        {
            boardPlaces = boardPlacesClone, 
            jaguarPossition = jagPossClone, 
            dogsPossitions = dogsPossClone
        };
    }
}