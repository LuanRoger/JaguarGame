using JaguarGame;
using JaguarGame.BoardDefinitions.Definitions;
using JaguarGame.Delegates;
using Spectre.Console;

const string defaultJaguarPlayerName = "Jaguar";
const string defaultDogsPlayerName = "Dogs";

Game game = new(new(defaultJaguarPlayerName), new(defaultDogsPlayerName), new AdugoBoard());

Layout mainLayout = new Layout("Root")
    .SplitColumns(
        new Layout("Infos")
            .SplitRows(
                new Layout("MovementRelation"),
                new Layout("PossibleMoves")
                    .Ratio(3),
                new Layout("Eventos")
                    .Ratio(5)),
        new Layout("Previews")
            .SplitRows(
                new Layout("CurrentGame")
                    .Ratio(2),
                new Layout("Data"))
        );

AnsiConsole.Live(mainLayout)
    .Start(ctx =>
    {
        GameEventsHandler eventsHandler = new(ctx, mainLayout);
        game.OnMove += eventsHandler.UpdateEventsOnMove;
        game.OnRoundEnded += eventsHandler.UpdateRoundInfos;
        var winner = game.Start();
        mainLayout["Eventos"].Update(new Panel(
                new Markup($"O vencedor é {winner?.ToString() ?? "Ninguém"}"))
            .Header("Eventos"));
    });

AnsiConsole.Write(mainLayout);
