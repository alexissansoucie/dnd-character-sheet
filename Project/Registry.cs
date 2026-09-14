// Project/Registry.cs
public class Registry
{
    private readonly List<Character> _items = new List<Character>();

    // TODO — Task 1. Say what your project is about, in words.
    public static string Topic => "D&D Character Sheet Manager";     // ← yours

    public Character NewItem(string name) => new Character(name);

    public void Add(Character item)
    {
        _items.Add(item);
    }

    public int Count => _items.Count;

    public List<Character> All()
    {
        // TODO — Task 5. Hand back a COPY, never the list itself.
        return _items;                                   // ← yours
    }
}