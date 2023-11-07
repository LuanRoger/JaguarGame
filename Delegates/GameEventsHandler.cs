using JaguarGame.BoardRenderer;
using JaguarGame.Models.EventsArgs;
using Spectre.Console;
using Spectre.Console.Rendering;

namespace JaguarGame.Delegates;

public class GameEventsHandler
{
    private readonly LiveDisplayContext _displayContext;
    private readonly Layout _layout;
    
    private const string MOVE_TEXT_FORMAT = "{0} foi para {1} (Pontuação: {2})";
    private const string POSSIBLE_MOVE_TEXT_FORMAT = "{0} [bold {1}]{2} --move-> {3}[/]";

    public GameEventsHandler(LiveDisplayContext displayContext, Layout layout)
    {
        _displayContext = displayContext;
        _layout = layout;
    }

    public void UpdateEventsOnMove(object sender, MoveEventArgs eventArgs)
    {
        AdugoBoardRenderer boardRenderer = new(5, 5);
        _layout["Eventos"].Update(new Panel(
                Align.Right(new Markup(string.Format(MOVE_TEXT_FORMAT, 
                    eventArgs.lastPlayerToMove.name, eventArgs.move.newPoss.id, eventArgs.moveScore)
                )))
            .Header("Eventos"));
        
        _layout["CurrentGame"].Update(
            new Panel(
                new Align(
                    boardRenderer.Render(eventArgs.boardAfterMove),
                    HorizontalAlignment.Center, VerticalAlignment.Middle))
                .Header("Visualização")
                .Expand()
            );
        
        IEnumerable<IRenderable> jaguarMovesRender = eventArgs.currentBoardMoveSet
            .jaguarMoves.Select(move =>
            {
                string movementMessage = string.Format(POSSIBLE_MOVE_TEXT_FORMAT, 
                    "Jaguar", move.isFatal ? "red" : "green",
                    eventArgs.currentBoardMoveSet.jaguarCurrentPoss.id, move.newPoss.id);
                return new Markup(movementMessage);
            });
        IEnumerable<IRenderable> dogsMovesRender = eventArgs.currentBoardMoveSet
            .dogsMoves.Select(move =>
            {
                string movementMessage = string.Format(POSSIBLE_MOVE_TEXT_FORMAT, 
                    "Cachorro", "blue", move.dogIndex, move.newPoss.id);
                return new Markup(movementMessage);
            });
        _layout["PossibleMoves"].Update(new Panel(
                new Columns(
                    new Rows(jaguarMovesRender),
                    new Rows(dogsMovesRender)
                ))
            .Header("Movimentos possiveis")
            .Expand()
        );
        
        _displayContext.Refresh();
    }
    
    public void UpdateRoundInfos(object sender, RoundEndedEventArgs eventArgs)
    {
        _layout["Data"].Update(
            new Panel(
                    new Rows(
                        new BarChart()
                            .AddItem("Round", eventArgs.round, Color.CadetBlue)
                            .AddItem("Quantidade moviemtnos", eventArgs.movesCount, Color.DarkSlateGray1)
                            .AddItem("Cachorro vivos", eventArgs.dogsAliveCount, Color.LightSalmon3_1)
                            .WithMaxValue(30)
                            .RightAlignLabel(),
                        new Markup($"Posição da onça: {eventArgs.jagPoss.id}")
                    ))
                .Header("Informações do ultimo round")
                .Expand()
        );
        _layout["MovementRelation"].Update(new Panel(
            new BreakdownChart()
                .AddItem("Jaguar", eventArgs.boardEvaluation.jaguarChangeToWin, Color.Silver)
                .AddItem("Dogs", eventArgs.boardEvaluation.dogsChangeToWin, Color.Gold1)
                .ShowPercentage()
                .UseValueFormatter(d =>
                {
                    double percentage = d * 100;
                    return $"{percentage:0.00}%";
                })
            )
            .Header("Relação de movimento")
        );
    }
}