using Spectre.Console;
public static class CharacterTable
{
    public static void Display(Registry registry)
    {

        AnsiConsole.MarkupLine($"[bold teal]\n{Markup.Escape(Registry.Topic)}[/]");
        AnsiConsole.MarkupLine($"[darkblue]{registry.Count} character(s) on file.[/]");

        var table = new Table();
        table.Border = TableBorder.Rounded;
        table.BorderStyle = new Style(Color.Black);
        table.Title("[bold purple]D&D Characters[/]");
        table.AddColumn("[bold fuchsia]Name[/]");
        table.AddColumn("[bold aqua]Species[/]");
        table.AddColumn("[bold blue]Class[/]");
        table.AddColumn("[bold teal]Background[/]");
        table.AddColumn("[bold darkblue]Age[/]");

        int rowIndex = 0;
        foreach (Character character in registry.All())
        {
            string rowColor = rowIndex % 2 == 0 ? "darkred" : "darkgreen";
            table.AddRow(
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
}
