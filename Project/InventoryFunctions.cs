using Spectre.Console;

public static class InventoryFunctions
{
    public static void Edit(Character character)
    {
        while (true)
        {
            AnsiConsole.MarkupLine($"\n[bold aqua]{Markup.Escape(character.Name)} Inventory[/]");
            AnsiConsole.MarkupLine("[fuchsia]1 - View inventory[/]");
            AnsiConsole.MarkupLine("[fuchsia]2 - Add item[/]");
            AnsiConsole.MarkupLine("[fuchsia]3 - Remove item[/]");
            AnsiConsole.MarkupLine("[fuchsia]0 - Back[/]");
            AnsiConsole.Markup("[bold blue]What do you want to do? [/] ");

            string choice = Console.ReadLine()?.Trim() ?? string.Empty;

            switch (choice)
            {
                case "1":
                    Display(character);
                    break;
                case "2":
                    AnsiConsole.Markup("[aqua]What is the item's name?[/] ");
                    string itemName = Console.ReadLine()?.Trim() ?? string.Empty;

                    if (string.IsNullOrEmpty(itemName))
                    {
                        AnsiConsole.MarkupLine("[red]An item name is required.[/]");
                        break;
                    }

                    Add(character, new CharacterInventory(itemName));
                    AnsiConsole.MarkupLine($"[green]{Markup.Escape(itemName)} added to the inventory.[/]");
                    break;
                case "3":
                    RemoveFromMenu(character);
                    break;
                case "0":
                    return;
                default:
                    AnsiConsole.MarkupLine("[red]Please enter a valid menu number.[/]");
                    break;
            }
        }
    }

    public static void Display(Character character)
    {
        var table = new Table
        {
            Border = TableBorder.Rounded,
            BorderStyle = new Style(Color.Violet)
        };

        table.Title($"[bold purple]{Markup.Escape(character.Name)} Inventory[/]");
        table.AddColumn("[bold CornflowerBlue]Index[/]");
        table.AddColumn("[bold DarkMagenta]Name[/]");
        table.AddColumn("[bold aqua]Type[/]");
        table.AddColumn("[bold blue]Quantity[/]");
        table.AddColumn("[bold teal]Total Weight[/]");

        for (int index = 0; index < character.CharactersInventory.Count; index++)
        {
            CharacterInventory item = character.CharactersInventory[index];
            table.AddRow(
                index.ToString(),
                Markup.Escape(item.Name),
                Markup.Escape(item.Type),
                item.Quantity.ToString(),
                item.TotalWeight.ToString());
        }

        AnsiConsole.Write(table);
    }

    public static void Add(Character character, CharacterInventory item)
    {
        character.CharactersInventory.Add(item);
    }

    public static bool Remove(Character character, int index)
    {
        if (index < 0 || index >= character.CharactersInventory.Count)
        {
            return false;
        }

        character.CharactersInventory.RemoveAt(index);
        return true;
    }

    private static void RemoveFromMenu(Character character)
    {
        if (character.CharactersInventory.Count == 0)
        {
            AnsiConsole.MarkupLine("[yellow]The inventory is empty.[/]");
            return;
        }

        Display(character);
        while (true)
        {
            AnsiConsole.Markup("[aqua]Which inventory index do you want to remove? (enter to cancel)[/] ");
            string input = Console.ReadLine()?.Trim() ?? string.Empty;

            if (string.IsNullOrEmpty(input))
            {
                return;
            }

            if (!int.TryParse(input, out int index) ||
                index < 0 ||
                index >= character.CharactersInventory.Count)
            {
                AnsiConsole.MarkupLine("[red]Enter a valid inventory index.[/]");
                continue;
            }

            string itemName = character.CharactersInventory[index].Name;
            if (Remove(character, index))
            {
                AnsiConsole.MarkupLine($"[red]{Markup.Escape(itemName)} removed from the inventory.[/]");
            }

            return;
        }
    }
}
