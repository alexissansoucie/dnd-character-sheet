using Spectre.Console;
public static class CharacterTable
{
    public static void Display(Registry registry)
    {
        AnsiConsole.MarkupLine($"[bold teal]\n{Markup.Escape(Registry.Topic)}[/]");
        AnsiConsole.MarkupLine($"[blue]{registry.Count} character(s) on file.[/]");

        var table = new Table
        {
            Border = TableBorder.Rounded,
            BorderStyle = new Style(Color.Violet)
        };

        table.Title("[bold purple]D&D Characters[/]");
        table.AddColumn("[bold CornflowerBlue]Index[/]");
        table.AddColumn("[bold DarkMagenta]Name[/]");
        table.AddColumn("[bold aqua]Species[/]");
        table.AddColumn("[bold DarkViolet]Class[/]");
        table.AddColumn("[bold DeepSkyBlue4]Background[/]");
        table.AddColumn("[bold blue]Age[/]");
        table.AddColumn("[bold teal]Level[/]");

        int rowIndex = 0;

        foreach (Character character in registry.All())
        {
            string rowColor = rowIndex % 2 == 0 ? "blue" : "purple";

            table.AddRow(
                $"[{rowColor}]{rowIndex}[/]",
                $"[{rowColor}]{Markup.Escape(character.Name)}[/]",
                $"[{rowColor}]{Markup.Escape(character.Species)}[/]",
                $"[{rowColor}]{Markup.Escape(character.Class)}[/]",
                $"[{rowColor}]{Markup.Escape(character.Background)}[/]",
                $"[{rowColor}]{character.Age}[/]",
                $"[{rowColor}]Level {character.Level} {character.Class}[/]");

            rowIndex++;
        }

        AnsiConsole.Write(table);
    }

    public static void FindCharacter(Registry registry)
    {   
        AnsiConsole.Markup("[purple]Search by Full Name (f) or Partial Name (p)[/] ");
        string searchMethod;
        bool valid = false;
        do
        {
            searchMethod = Console.ReadLine()?.Trim() ?? string.Empty;
            if (string.IsNullOrEmpty(searchMethod) || !(searchMethod.ToLower() == "f" || !(searchMethod.ToLower() == "p")))
            {
                AnsiConsole.MarkupLine("[red]A valid response is required.[/]");
            }
            else
            {
                valid = true;
            }


        } while (valid);

        AnsiConsole.Markup("[aqua]What is the Character's name?[/] ");
        string name;
        do
        {
            name = Console.ReadLine()?.Trim() ?? string.Empty;
            if (string.IsNullOrEmpty(name))
            {
                AnsiConsole.MarkupLine("[red]A response is required.[/]");
                AnsiConsole.Markup("[aqua]What is the Character's name?[/] ");
            }


        } while (string.IsNullOrEmpty(name));

        switch (searchMethod)
        {
            case "f":
                Console.WriteLine(registry.Find(name));
                break;
            case "p":
                Console.WriteLine(registry.Matching(name));
                break;
            default:
                    AnsiConsole.MarkupLine("[fuchsia]Please enter a valid response (f,p)[/]");
                    break;
        }
        
    }

    public static void RemoveCharacter(Registry registry)
    {
        Display(registry);

        AnsiConsole.Markup("[aqua]Which number character do you want to remove?[/] ");

        while (true)
        {
            string input = Console.ReadLine()?.Trim() ?? string.Empty;

            if (!int.TryParse(input, out int index))
            {
                AnsiConsole.MarkupLine("[red]Enter a valid number.[/]");
                continue;
            }

            List<Character> characters = registry.All();

            if (index < 0 || index >= characters.Count)
            {
                AnsiConsole.MarkupLine("[red]That index does not exist.[/]");
                continue;
            }

            Character character = characters[index];

            AnsiConsole.Markup($"[red]Are you sure you want to remove {Markup.Escape(character.Name)}? (y/n)[/] ");
            string confirmation = Console.ReadLine()?.Trim().ToLower() ?? string.Empty;

            if (confirmation != "y" && confirmation != "yes")
            {
                AnsiConsole.MarkupLine("[yellow]Character was not removed.[/]");
                break;
            }

            registry.Remove(character.Name);
            AnsiConsole.MarkupLine($"[red]{character.Name} has been removed[/]");
            break;
        }
    }

    public static void LevelUpCharacters(Registry registry)
    {
        foreach (Character character in registry.All())
        {
            character.LevelUp();
        }
        AnsiConsole.MarkupLine("[DeepSkyBlue4]\nALL CHARACTERS LEVELED UP!!![/]");
    }

}
