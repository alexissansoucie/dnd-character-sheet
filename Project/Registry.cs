// Project/Registry.cs
using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore.Query;


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
        return _items.FirstOrDefault(item =>
            string.Equals(item.Name, name, StringComparison.OrdinalIgnoreCase));
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

    public void Save(ProjectContext db)
    {
        foreach (Character item in _items)
        {
            if (item.Id == 0)
            {
                db.Characters.Add(item);
            }
            else
            {
                db.Characters.Update(item);
            }
        }

        db.SaveChanges();
    }

    public void Load(ProjectContext db)
    {
        List<Character> loaded = db.Characters.OrderBy(item => item.Id).ToList();

        if (loaded.Count == 0)
        {
            return;
        }

        _items.Clear();


        _items.AddRange(loaded);
    }

    public List<Character> Matching(string term)
    {
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