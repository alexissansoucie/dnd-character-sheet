using Spectre.Console;
public static class CharacterTable
{
    public static void Display(Registry registry)
    {
        AnsiConsole.MarkupLine($"[bold teal]\n{Markup.Escape(Registry.Topic)}[/]");
        AnsiConsole.MarkupLine($"[darkblue]{registry.Count} character(s) on file.[/]");

        var table = new Table
        {
            Border = TableBorder.Rounded,
            BorderStyle = new Style(Color.Black)
        };

        table.Title("[bold purple]D&D Characters[/]");
        table.AddColumn("[bold CornflowerBlue]Index[/]");
        table.AddColumn("[bold DarkMagenta]Name[/]");
        table.AddColumn("[bold aqua]Species[/]");
        table.AddColumn("[bold DarkViolet]Class[/]");
        table.AddColumn("[bold DeepSkyBlue4]Background[/]");
        table.AddColumn("[bold darkblue]Age[/]");

        int rowIndex = 0;

        foreach (Character character in registry.All())
        {
            string rowColor = rowIndex % 2 == 0 ? "darkred" : "darkgreen";

            table.AddRow(
                $"[{rowColor}]{rowIndex}[/]",
                $"[{rowColor}]{Markup.Escape(character.Name)}[/]",
                $"[{rowColor}]{Markup.Escape(character.Species)}[/]",
                $"[{rowColor}]{Markup.Escape(character.Class)}[/]",
                $"[{rowColor}]{Markup.Escape(character.Background)}[/]",
                $"[{rowColor}]{character.Age}[/]");

            rowIndex++;
        }

        AnsiConsole.Write(table);
    }

    public static void FindCharacter(Registry registry)
    {
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

        registry.Find(name);
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
            registry.Remove(character.Name);
            break;
        }
    }

}
