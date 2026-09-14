// Project/Thing.cs — Thing is a PLACEHOLDER. Rename the file and the class.
public class Character
{
    // TODO — Task 2. These three are holes: anything, anywhere, can write
    // anything into them. Close every one into a property.
    // Task 3 gives one of them a rule; Task 4 seals one shut.
    private string _name;      // ← yours
    private string _species;
    private string _class;
    private string _background;
    private int _age;

    public Character(string name, string species, string characterClass, string background, int age)
    {
        _name = name;
        _species = species;
        _class = characterClass;
        _background = background;
        _age = age;        
    }

    public string GetName()
    {
        return _name;
    }

    public string GetSpecies()
    {
        return _species;
    }

    public string GetClass()
    {
        return _class;
    }

    public string GetBackground()
    {
        return _background;
    }

    public int GetAge()
    {
        return _age;
    }

    public void SetName(string name)
    {
        _name = name;
    }

    public void SetSpecies(string species)
    {
        _species = species;
    }

    public void SetClass(string characterClass)
    {
        _class = characterClass;
    }

    public void SetBackground(string background)
    {
        _background = background;
    }

    public void SetAge(int age)
    {
        _age = age;
    }
}