using JaguarGame;
using JaguarGame.BoardDefinitions;
using JaguarGame.BoardDefinitions.Definitions;
using JaguarGame.BoardRenderer;
using JaguarGame.Models;
using JaguarGame.Models.EventsArgs;
using Spectre.Console;
using Spectre.Console.Rendering;

const string DEFAULT_JAGUAR_PLAYER_NAME = "Jaguar";
const string DEFAULT_DOGS_PLAYER_NAME = "Dogs";

string jaguarPlauyerName = AnsiConsole.Ask<string>("Digite o nome do jogador que será o jaguar", 
    DEFAULT_JAGUAR_PLAYER_NAME);
string dogsPlayerName = AnsiConsole.Ask<string>("Digite o nome do jogador que serão os cachorros",
    DEFAULT_DOGS_PLAYER_NAME);

Game game = new(new(jaguarPlauyerName), new(dogsPlayerName), new AdugoBoard());

Layout mainLayout = new Layout("Root")
    .SplitColumns(
        new Layout("Preview"),
        new Layout("Infos")
            .SplitRows(new Layout("Data"),
                new Layout("Eventos"))
        );

AnsiConsole.Live(mainLayout)
    .Start(ctx =>
    {
        game.OnMove += UpdateEventsOnMove;
        game.OnRoundEnded += UpdateRoundInfos;
        var winner = game.Start();
        mainLayout["Eventos"].Update(new Panel(
                new Markup($"O vencedor é {winner?.ToString() ?? "Ninguém"}"))
            .Header("Eventos"));
        return;

        void UpdateEventsOnMove(object sender, MoveEventArgs eventArgs)
        {
            const string moveTextFormat = "{0} foi para {1} ({2})";
            AdugoBoardRenderer boardRenderer = new(5, 5);
            mainLayout["Eventos"].Update(new Panel(
                new Markup(string.Format(moveTextFormat, 
                    eventArgs.player.name, eventArgs.move.newPoss.id, eventArgs.moveScore)))
                .Header("Eventos"));
            mainLayout["Preview"].Update(
                new Panel(
                    new Align(
                        boardRenderer.Render(eventArgs.boardAfterMove),
                        HorizontalAlignment.Center, VerticalAlignment.Middle)));
            ctx.Refresh();
        }
        void UpdateRoundInfos(object sender, RoundEndedEventArgs eventArgs)
        {
            Style ruleStyle = new Style()
                .Foreground(Color.Gold1);
            
            mainLayout["Data"].Update(
                new Panel(
                    new Rows(
                        new Rule("[bold blue]Informações do ultimo round[/]")
                            .RuleStyle(ruleStyle)
                            .Centered(),
                        new Markup($"Round: {eventArgs.round}"),
                        new Markup($"Posição da onça: {eventArgs.jagPoss.id}"),
                        new Markup($"Cachorros vivos: {eventArgs.dogsAliveCount}"),
                        new Rule("[bold yellow]Chance de vitória[/]")
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
                    .Header("Informações")
                    .Expand()
                );
        }
    });

AnsiConsole.Write(mainLayout);
