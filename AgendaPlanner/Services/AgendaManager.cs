using AgendaPlanner.Models;

namespace AgendaPlanner.Services;

public class AgendaManager
{
    private readonly List<AgendaItem> _items = new();

    public IReadOnlyList<AgendaItem> Items => _items
        .OrderBy(item => item.StartTime)
        .ToList();

    public void AddItem(AgendaItem item) => _items.Add(item);

    public bool RemoveItem(int index)
    {
        if (index < 0 || index >= _items.Count)
        {
            return false;
        }

        _items.RemoveAt(index);
        return true;
    }
}
