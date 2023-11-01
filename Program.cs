using JaguarGame;
using JaguarGame.BoardDefinitions.Definitions;
using JaguarGame.Delegates;
using Spectre.Console;

const string DEFAULT_JAGUAR_PLAYER_NAME = "Jaguar";
const string DEFAULT_DOGS_PLAYER_NAME = "Dogs";

string jaguarPlauyerName = AnsiConsole.Ask<string>("Digite o nome do jogador que será o jaguar", 
    DEFAULT_JAGUAR_PLAYER_NAME);
string dogsPlayerName = AnsiConsole.Ask<string>("Digite o nome do jogador que serão os cachorros",
    DEFAULT_DOGS_PLAYER_NAME);

Game game = new(new(jaguarPlauyerName), new(dogsPlayerName), new AdugoBoard());

Layout mainLayout = new Layout("Root")
    .SplitColumns(
        new Layout("Previews")
            .SplitRows(
                new Layout("CurrentGame"),
                new Layout("PossibleMoves")),
        new Layout("Infos")
            .SplitRows(
                new Layout("Data"),
                new Layout("Eventos"))
        );

AnsiConsole.Live(mainLayout)
    .Start(ctx =>
    {
        GameEventsHandler eventsHandler = new(ctx, mainLayout);
        game.OnMove += eventsHandler.UpdateEventsOnMove;
        game.OnRoundEnded += eventsHandler.UpdateRoundInfos;
        game.OnStartRound += eventsHandler.UpdateEventsOnStartRound;
        var winner = game.Start();
        mainLayout["Eventos"].Update(new Panel(
                new Markup($"O vencedor é {winner?.ToString() ?? "Ninguém"}"))
            .Header("Eventos"));
    });

AnsiConsole.Write(mainLayout);
