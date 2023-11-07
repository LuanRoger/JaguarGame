using JaguarGame.BoardRenderer;
using JaguarGame.Models.EventsArgs;
using Spectre.Console;
using Spectre.Console.Rendering;

namespace JaguarGame.Delegates;

public class GameEventsHandler
{
    private readonly LiveDisplayContext _displayContext;
    private readonly Layout _layout;

    public GameEventsHandler(LiveDisplayContext displayContext, Layout layout)
    {
        _displayContext = displayContext;
        _layout = layout;
    }

    public void UpdateEventsOnMove(object sender, MoveEventArgs eventArgs)
    {
        const string moveTextFormat = "{0} foi para {1} ({2})";
        AdugoBoardRenderer boardRenderer = new(5, 5);
        _layout["Eventos"].Update(new Panel(
                new Markup(string.Format(moveTextFormat, 
                    eventArgs.player.name, eventArgs.move.newPoss.id, eventArgs.moveScore)))
            .Header("Eventos"));
        _layout["CurrentGame"].Update(
            new Panel(
                new Align(
                    boardRenderer.Render(eventArgs.boardAfterMove),
                    HorizontalAlignment.Center, VerticalAlignment.Middle))
                .Expand());
        _displayContext.Refresh();
    }
    
    public void UpdateEventsOnStartRound(object sender, StartRoundEventArgs eventArgs)
    {
        IEnumerable<IRenderable> jaguarMovesRender = eventArgs.jaguarMoves
            .Select(move => new Markup($"Jaguar [bold]-> {move.newPoss.id}[/]"));
        _layout["PossibleMoves"].Update(new Panel(
                new Rows(jaguarMovesRender))
            .Header("Movimentos possiveis")
            .Expand()
        );
        
        _displayContext.Refresh();
    }
    
    public void UpdateRoundInfos(object sender, RoundEndedEventArgs eventArgs)
    {
        Style ruleStyle = new Style()
            .Foreground(Color.Gold1);
            
        _layout["Data"].Update(
            new Panel(
                    new Rows(
                        new BarChart()
                            .AddItem("Round", eventArgs.round, Color.CadetBlue)
                            .AddItem("Quantidade moviemtnos", eventArgs.movesCount, Color.DarkSlateGray1)
                            .AddItem("Cachorro vivos", eventArgs.dogsAliveCount, Color.LightSalmon3_1)
                            .WithMaxValue(30)
                            .RightAlignLabel(),
                        new Markup($"Posição da onça: {eventArgs.jagPoss.id}"),
                        new Rule("[bold yellow]Relação de movimento[/]")
                            .RuleStyle(ruleStyle)
                            .Centered(),
                        new BreakdownChart()
                            .AddItem("Jaguar", eventArgs.boardEvaluation.jaguarChangeToWin, Color.Silver)
                            .AddItem("Dogs", eventArgs.boardEvaluation.dogsChangeToWin, Color.Gold1)
                            .ShowPercentage()
                            .UseValueFormatter(d =>
                            {
                                double percentage = d * 100;
                                return $"{percentage:0.00}%";
                            })
                    ))
                .Header("Informações do ultimo round")
                .Expand()
        );
    }
}