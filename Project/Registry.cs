// Project/Registry.cs
public class Registry
{
    private readonly List<Thing> _items = new List<Thing>();

    // TODO — Task 1. Say what your project is about, in words.
    public static string Topic => "D&D Character Sheet Manager";     // ← yours

    public Thing NewItem(string name) => new Thing(name);

    public void Add(Thing item)
    {
        _items.Add(item);
    }

    public int Count => _items.Count;

    public List<Thing> All()
    {
        // TODO — Task 5. Hand back a COPY, never the list itself.
        return _items;                                   // ← yours
    }
}