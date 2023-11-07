namespace JaguarGame.Models;

public record GameState
{
    public int round { get; set; }
    public int movementsCount { get; set; }
}