using JaguarGame.Models;

namespace JaguarGame.BoardDefinitions.Definitions;

public sealed class AdugoBoard : IBoardDefinitionFactory
{
    public BoardDefinition Create()
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
        Place place26 = new(26);
        Place place27 = new(27);
        Place place28 = new(28);
        Place place29 = new(29);
        Place place30 = new(30);
        Place place31 = new(31);
        
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
        place23.Connect(new PlaceRef[]
        {
            place17, place18, place19, place22, place24,
            place26, place27, place28
        });
        place24.Connect(new PlaceRef[]{place19, place23, place25});
        place25.Connect(new PlaceRef[]{place19, place18, place24});
        place26.Connect(new PlaceRef[]{place23, place27, place29});
        place27.Connect(new PlaceRef[]{place23, place26, place28, place30});
        place28.Connect(new PlaceRef[]{place23, place27, place31});
        place29.Connect(new PlaceRef[]{place26, place30});
        place30.Connect(new PlaceRef[]{place27, place29, place31});
        place31.Connect(new PlaceRef[]{place28, place30});
        
        List<Place> boardPlaces = new()
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
            place25,
            place26,
            place27,
            place28,
            place29,
            place30,
            place31
        };
        PlaceRef jaguarPossition = place13;
        List<PlaceRef> dogsPoss = new() {place1, place2, place3, place4, place5, place6, place7,
            place8, place9, place10, place11, place12, place14, place15};
        
        return new()
        {
            boardPlaces = boardPlaces,
            jaguarPossition = jaguarPossition,
            dogsPossitions = dogsPoss
        };
    }
}