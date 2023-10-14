namespace JaguarGame.Models;

public class TranspositionTable
{
    private Dictionary<int, TranspositionTableItem> _table;

    public TranspositionTable()
    {
        _table = new();
    }

    public void SaveEntry(int hashKey, TranspositionTableItem item)
    {
        _table[hashKey] = item;
    }

    public bool TryRetrieveEntry(int hashKey, int depth, out TranspositionTableItem? entry)
    {
        if (_table.TryGetValue(hashKey, out entry) && entry.depth >= depth)
        {
            return true;
        }
        entry = null;
        return false;
    }
}