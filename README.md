# MiniMax on Adugo
![Demo](https://github.com/LuanRoger/JaguarGame/blob/main/images/DemoTimelapse.gif)
Implementação do algoritimo competitovo MiniMax com poda Alpha-Beta para jogar Adugo (Jogo da onça).
Este implementação foi feito como trabalho da disciplina de Inteligência Artificial (IA).

## Tech stack
- 🚀 .NET 7
- ✨ CLI criada com [Spectre.Console](https://spectreconsole.net)

# Como executar
## Pré-requisitos
- [.NET 7](https://dotnet.microsoft.com/pt-br/download/dotnet/7.0)

Depois de baixar os pré-requisitos, clone o repositório e entre na pasta do projeto.
```powershell
git clone https://github.com/LuanRoger/JaguarGame.git
cd JaguarGame
```

## Executando
```powershell
dotnet run --release
```
O algoritimo será executado com os parâmetros padrões, que são:
- Profundidade máxima da árvore: 5
- Nome do jogador 1: Jaguar
- Nome do jogador 2: Dogs
- Tabuleiro: [`Adugo`](https://github.com/LuanRoger/JaguarGame/blob/main/BoardDefinitions/Definitions/AdugoBoard.cs)
