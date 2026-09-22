// Project/Thing.cs — Thing is a PLACEHOLDER. Rename the file and the class.
public class Character
{
    // TODO — Task 2. These three are holes: anything, anywhere, can write
    // anything into them. Close every one into a property.
    // Task 3 gives one of them a rule; Task 4 seals one shut.

    private string _name = "Unknown";
    private string _species = "Unknown";
    private string _class = "Unknown";
    private string _background = "Unknown";
    private int _age;
    private int _level = 1;
    // jeff test

    public string Name
    {
        get { return _name; }
        set
        {
            if (!string.IsNullOrWhiteSpace(value))
            {
                _name = value.Trim();
            }
        }
    }

    //comment to show merge

    public string Species
    {
        get { return _species; }
        set
        {
            if (!string.IsNullOrWhiteSpace(value))
            {
                _species = value.Trim();
            }
        }
    }

    public string Class
    {
        get { return _class; }
        set
        {
            if (!string.IsNullOrWhiteSpace(value))
            {
                _class = value.Trim();
            }
        }
    }

    public string Background
    {
        get { return _background; }
        set
        {
            if (!string.IsNullOrWhiteSpace(value))
            {
                _background = value.Trim();
            }
        }
    }

    public int Age {get; private set;}

    public int Level {get; private set;}

    public void LevelUp()
    {
        Level++;
    }

    public Character(string name)
    {
        Name = name;
    }

    public Character(string name, string species, string characterClass, string background, int age)
    {
        Name = name;
        Species = species;
        Class = characterClass;
        Background = background;
        Age = age;
    }

}
