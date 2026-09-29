// Project/Registry.cs
public class Registry : IListed
{
    private readonly List<Character> _items = new List<Character>();

    // TODO — Task 1. Say what your project is about, in words.
    public static string Topic => "D&D Character Sheet Manager";     // ← yours

    public Character NewItem(string name) => new Character(name);

    public void Add(Character item)
    {
        _items.Add(item);
    }

    public List<IListed> Everything()
    {
        List<IListed> listing = new List<IListed>();

        listing.Add(this);

        foreach (Character item in _items)
        {
            listing.Add(item);
        }

        return listing;
    }

    public int Count => _items.Count;

    public List<Character> All()
    {
        // TODO — Task 5. Hand back a COPY, never the list itself.
        return new List<Character>(_items);                                 // ← yours
    }

    public Character? Find(string name)
    {
        foreach (Character character in _items)
        {
            if (character.Name == name)
            {
                return character;
            }
        }
        return null;
    }

    public bool Remove(string name)
    {
        Character? found = Find(name);

        if (found == null)
        {
            return false;
        }

        _items.Remove(found);
        return true;
    }

    public string Kind => "REGISTRY";

    public string Line() => $"{Registry.Topic} with {_items.Count} on file";
}