namespace JaguarGame.GameInterface;

public interface IScore<TPlayer>
{
    int Score(TPlayer player);
}