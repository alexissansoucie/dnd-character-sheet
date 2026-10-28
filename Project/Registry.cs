// Project/Registry.cs
using System.Runtime.CompilerServices;
using System.Text.Json;


public class Registry : IListed
{
    private readonly List<Character> _items = new List<Character>();

    // TODO — Task 1. Say what your project is about, in words.
    public static string Topic => "D&D Character Sheet Manager";     // ← yours

    public Character NewItem(string name) => new Character(name);

    public void Add(Character item)
    {
        if (Find(item.Name) != null)
        {
            return;
        }
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
        return _items.FirstOrDefault(item => item.Name == name);
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

    public void Save(string path)
    {
         string json = JsonSerializer.Serialize(_items,
            new JsonSerializerOptions { WriteIndented = true });

        File.WriteAllText(path, json);
    }

    public void Load(string path)
    {
        if (!File.Exists(path))
        {
            return;
        }

        List<Character>? loaded =
            JsonSerializer.Deserialize<List<Character>>(File.ReadAllText(path));

        if (loaded == null)
        {
            return;
        }

        _items.Clear();


        _items.AddRange(loaded);
    }

    public List<Character> Matching(string term){
        return _items.Where(item => item.Name.Contains(term)).ToList();
    }

    public List<string> Names()
    {
        return _items.Select(item => item.Name).ToList();
    }

    public List<Character> Sorted()
    {
        return _items.OrderBy(item => item.Name).ToList();
    }
}