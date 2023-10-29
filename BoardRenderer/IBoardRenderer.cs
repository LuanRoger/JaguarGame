namespace JaguarGame.BoardRenderer;

public interface IBoardRenderer<out T>
{
    public int boardDimensionX { get; } 
    public int boardDimensionY { get; }
    
    public T Render(Board board);
}