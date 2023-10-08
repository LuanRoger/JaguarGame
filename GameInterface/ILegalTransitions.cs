namespace JaguarGame.GameInterface;

public interface ILegalTransitions<TTransition>
{
    List<TTransition> GetLegalTransitions();
}