// Project/Thing.cs — Thing is a PLACEHOLDER. Rename the file and the class.
using Spectre.Console;

public class Character
{
    // TODO — Task 2. These three are holes: anything, anywhere, can write
    // anything into them. Close every one into a property.
    // Task 3 gives one of them a rule; Task 4 seals one shut.

    private string _name = "Unknown";
    private string _firstName = "Unknown";
    private string _lastName = "Unknown";
    private string _species = "Unknown";
    private string _class = "Unknown";
    private string _background = "Unknown";
    private int _age;
    private int _level = 1;

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

    public string FirstName
    {
        get { return _firstName; }
        set
        {
            if (!string.IsNullOrWhiteSpace(value))
            {
                _firstName = value.Trim();
            }
        }
    }

    public string LastName
    {
        get { return _lastName; }
        set
        {
            if (!string.IsNullOrWhiteSpace(value))
            {
                _lastName = value.Trim();
            }
        }
    }

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

    public int Age { get; private set; }

    public int Level { get; private set; }

    public void LevelUp()
    {
        Level++;
    }

    public Character(string name)
    {
        Name = name;
    }

    public Character(string name, string firstName, string lastName, string species, string characterClass, string background, int age)
    {
        Name = name;
        FirstName = firstName;
        LastName = lastName;
        Species = species;
        Class = characterClass;
        Background = background;
        Age = age;
    }
    public static Character AddCharacter()
    {
        AnsiConsole.Markup("[aqua]What is your Character's first name?[/] ");
        string firstName;
        do
        {
            firstName = Console.ReadLine()?.Trim() ?? string.Empty;
            if (string.IsNullOrEmpty(firstName))
            {
                AnsiConsole.MarkupLine("[red]A response is required.[/]");
                AnsiConsole.Markup("[aqua]What is your Character's first name?[/] ");
            }
        } while (string.IsNullOrEmpty(firstName));

        AnsiConsole.Markup("[aqua]What is your Character's last name?[/] ");
        string lastName;
        do
        {
            lastName = Console.ReadLine()?.Trim() ?? string.Empty;
            if (string.IsNullOrEmpty(lastName))
            {
                AnsiConsole.MarkupLine("[red]A response is required.[/]");
                AnsiConsole.Markup("[aqua]What is your Character's last name?[/] ");
            }
        } while (string.IsNullOrEmpty(lastName));

        string name = $"{firstName} {lastName}";


        AnsiConsole.Markup("[blue]What is your Character's species?[/] ");
        string species;
        do
        {
            species = Console.ReadLine()?.Trim() ?? string.Empty;
            if (string.IsNullOrEmpty(species))
            {
                AnsiConsole.MarkupLine("[red]A response is required.[/]");
                AnsiConsole.Markup("[blue]What is your Character's species?[/] ");
            }
        } while (string.IsNullOrEmpty(species));

        AnsiConsole.Markup("[purple]What is your Character's class?[/] ");
        string characterClass;
        do
        {
            characterClass = Console.ReadLine()?.Trim() ?? string.Empty;
            if (string.IsNullOrEmpty(characterClass))
            {
                AnsiConsole.MarkupLine("[red]A response is required.[/]");
                AnsiConsole.Markup("[purple]What is your Character's class?[/] ");
            }
        } while (string.IsNullOrEmpty(characterClass));

        AnsiConsole.Markup("[darkblue]What is your Character's background?[/] ");
        string background;
        do
        {
            background = Console.ReadLine()?.Trim() ?? string.Empty;
            if (string.IsNullOrEmpty(background))
            {
                AnsiConsole.MarkupLine("[red]A response is required.[/]");
                AnsiConsole.Markup("[darkblue]What is your Character's background?[/] ");
            }
        } while (string.IsNullOrEmpty(background));

        AnsiConsole.Markup("[aqua]What is your Character's age?[/] ");
        int age;
        string ageInput;
        do
        {
            ageInput = Console.ReadLine()?.Trim() ?? string.Empty;
            if (!int.TryParse(ageInput, out age) || age < 0)
            {
                AnsiConsole.MarkupLine("[red]Please enter a valid age.[/]");
                AnsiConsole.Markup("[aqua]What is your Character's age?[/] ");
            }
        } while (!int.TryParse(ageInput, out age) || age < 0);

        return new Character(name, firstName, lastName, species, characterClass, background, age);
    }
}


