using JaguarGame.Models;
using Spectre.Console;
using Spectre.Console.Rendering;

namespace JaguarGame.BoardRenderer;

public class AdugoBoardRenderer : IBoardRenderer<Table>
{
    public int boardDimensionX { get; }
    public int boardDimensionY { get; }

    public AdugoBoardRenderer(int boardDimensionX, int boardDimensionY)
    {
        this.boardDimensionX = boardDimensionX;
        this.boardDimensionY = boardDimensionY;
    }

    public Table Render(Board board)
    {
        Table table = new Table()
            .Border(TableBorder.MinimalHeavyHead)
            .Centered();
        
        for (int column = 1; column < boardDimensionX + 1; column++)
            table.AddColumn(new(column.ToString()));
        
        PlaceRef jagPoss = board.jagPoss;
        var dogsPoss = board.dogsPoss;
        List<IRenderable> rowCells = new();
        for (int rowIndex = 1; rowIndex <= boardDimensionX * boardDimensionY + boardDimensionX; rowIndex++)
        {
            if(rowIndex % boardDimensionX == 1 && rowCells.Any())
            {
                table.AddRow(rowCells);
                rowCells.Clear();
                continue;
            }
    
            if(jagPoss.id == rowIndex)
                rowCells.Add(new Markup("J"));
            else if(dogsPoss.Any(dog => dog.id == rowIndex))
                rowCells.Add(new Markup("D"));
            else rowCells.Add(new Markup(" "));
        }
        DrawnBoardTail(ref table, board);
        
        return table;
    }
    
    private void DrawnBoardTail(ref Table table, Board board)
    {
        const int tailXSize = 5;
        const int tailYSize = 2;
        
        PlaceRef jagPoss = board.jagPoss;
        var dogsPoss = board.dogsPoss;
        List<IRenderable> rowCells = new();
        int possitionOffset = 25;
        bool jumpVerification = false;
        for (int rowIndex = 1; rowIndex <= tailXSize * tailYSize + tailXSize; rowIndex++)
        {
            if(rowIndex % boardDimensionX == 1 && rowCells.Any())
            {
                table.AddRow(rowCells);
                rowCells.Clear();
                possitionOffset--;
                continue;
            }
            
            int entityPoss = rowIndex + possitionOffset;
            if(jumpVerification)
            {
                jumpVerification = false;
                goto VerificationEnd;
            }
            switch (entityPoss)
            {
                case 26:
                    rowCells.Add(new Markup("->"));
                    jumpVerification = true;
                    possitionOffset--;
                    continue;
                case 29:
                    rowCells.Add(new Markup("<-"));
                    jumpVerification = true;
                    possitionOffset--;
                    continue;
                case 30:
                    rowCells.Add(new Markup("<->"));
                    jumpVerification = true;
                    possitionOffset--;
                    continue;
                case 31:
                    rowCells.Add(new Markup("<->"));
                    jumpVerification = true;
                    possitionOffset--;
                    continue;
            }
            VerificationEnd:
            
            if(jagPoss.id == entityPoss)
                rowCells.Add(new Markup("J"));
            else if(dogsPoss.Any(dog => dog.id == entityPoss))
                rowCells.Add(new Markup("D"));
            else rowCells.Add(new Markup(" "));
        }
    }
}