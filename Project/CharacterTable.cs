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
        if (registry.Count == 0)
        {
            AnsiConsole.MarkupLine("[yellow]There are no characters to select.[/]");
            return;
        }

        AnsiConsole.MarkupLine("[DeepSkyBlue4]Would you like to select a character (y/n)?[/]");
        while (true)
        {
            string? selection = Console.ReadLine()?.Trim();

            if (selection != null &&
                (selection.Equals("n", StringComparison.OrdinalIgnoreCase) ||
                 selection.Equals("no", StringComparison.OrdinalIgnoreCase)))
            {
                return;
            }

            if (selection != null &&
                (selection.Equals("y", StringComparison.OrdinalIgnoreCase) ||
                 selection.Equals("yes", StringComparison.OrdinalIgnoreCase)))
            {
                CharacterFunctions.SelectCharacter(registry);
                return;
            }

            AnsiConsole.MarkupLine("[red]Please enter yes or no.[/]");
            AnsiConsole.Markup("[DeepSkyBlue4]Would you like to select a character (y/n)?[/] ");
        }
    }
}
