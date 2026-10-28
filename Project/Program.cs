using System.ComponentModel;
using Spectre.Console;
using System;
using System.Diagnostics;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        // 1. If the "--child" flag is NOT present, spawn a separate window and exit this one
        if (!args.Contains("--child"))
        {
            // Determine if we are running via 'dotnet run' (dll) or directly (exe)
            string currentExe = Process.GetCurrentProcess().MainModule.FileName;
            string command, arguments;

            if (currentExe.EndsWith("dotnet.exe", StringComparison.OrdinalIgnoreCase) ||
                currentExe.EndsWith("dotnet", StringComparison.OrdinalIgnoreCase))
            {
                // Running via 'dotnet run'
                command = "dotnet";
                arguments = $"run -- --child"; // Pass --child to the new window
            }
            else
            {
                // Running the compiled executable directly
                command = currentExe;
                arguments = "--child";
            }

            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = command,
                Arguments = arguments,
                UseShellExecute = true, // Mandatory: Forces the OS to spawn a separate window
                CreateNoWindow = false
            };

            try
            {
                Process.Start(startInfo);
                return; // Safely shut down the integrated panel instance
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to open separate window: {ex.Message}");
            }
        }

        var registry = new Registry();
        string registryFile = "registry.json";
        registry.Load(registryFile);

        Character phoenix = new Character("Phoenix Longbottom", "Phoenix", "Longbottom", "Fairy", "Rouge", "background", 45);
        registry.Add(phoenix);
        Character virgil = new Character("Virgil Pureman", "Virgil", "Pureman", "Human", "Bard", "background", 36);
        registry.Add(virgil);

        Console.WriteLine(Registry.Topic);
        Console.WriteLine($"{registry.Count} on file.");
        Console.WriteLine();

        bool noExit = true;

        var fontPath = Path.Combine(AppContext.BaseDirectory, "Small.flf");
        var font = FigletFont.Load(fontPath);
        var dungeonsAndDragons = new FigletText(font, "Dungeons And Dragons")
        {
            Justification = Justify.Center,
            Color = Color.DeepSkyBlue1

        };
        var characterSheet = new FigletText(font, "Character Sheet Manager")
        {
            Justification = Justify.Center,
            Color = Color.BlueViolet

        };

        var mediumFont = FigletFont.Load(Path.Combine(AppContext.BaseDirectory, "bubble.flf"));

        var onFile = new FigletText(mediumFont, $"{registry.Count} on File")
        {
            Justification = Justify.Center,
            Color = Color.DeepSkyBlue4
        };

        AnsiConsole.Write(dungeonsAndDragons);
        AnsiConsole.Write(characterSheet);
        AnsiConsole.Write(onFile);
        AnsiConsole.WriteLine();

        string[] dragonLines = Dragon.Art.Split(Environment.NewLine);
        int dragonWidth = dragonLines.Max(line => line.Length);
        int leftPadding = Math.Max(0, (Console.WindowWidth - dragonWidth) / 2);
        for (int lineIndex = 0; lineIndex < dragonLines.Length; lineIndex++)
        {
            string color = lineIndex % 2 == 0 ? "purple" : "blue";
            string centeredLine = new string(' ', leftPadding) + dragonLines[lineIndex];
            AnsiConsole.MarkupLine($"[{color}]{Markup.Escape(centeredLine)}[/]");
        }

        while (noExit)
        {
            Menu();
        }

        void Menu()
        {

            AnsiConsole.MarkupLine("[bold aqua]\nD&D Character Menu[/]");
            foreach (string item in MenuItems.Items)
            {
                AnsiConsole.MarkupLine($"\t[fuchsia]{Markup.Escape(item)}[/]");
            }
            Console.WriteLine();

            AnsiConsole.Markup("[bold blue]What do you want to do? [/] ");
            string choiceInput = Console.ReadLine() ?? string.Empty;

            if (!int.TryParse(choiceInput, out int choice))
            {
                AnsiConsole.MarkupLine("[fuchsia]Please enter a menu number.[/]");
                return;
            }

            switch (choice)
            {
                case 1:
                    Character character = Character.AddCharacter();
                    registry.Add(character);
                    break;
                case 2:
                    CharacterTable.Display(registry);
                    break;
                case 3:
                    CharacterTable.FindCharacter(registry);
                    break;

                case 4:
                    CharacterTable.RemoveCharacter(registry);
                    break;

                case 5:
                    CharacterTable.LevelUpCharacters(registry);
                    break;

                case 0:
                    noExit = false;
                    break;
                default:
                    AnsiConsole.MarkupLine("[fuchsia]Please enter 1, 2, or 0.[/]");
                    break;
            }
        }

        registry.Save(registryFile);
        Console.WriteLine($"{registry.Count} on file, saved to {registryFile}.");

        Console.WriteLine();
        Console.Write("Search (a word, or Enter to skip): ");
        string? term = Console.ReadLine();

        if (!string.IsNullOrWhiteSpace(term))
        {
            var found = registry.Matching(term.Trim());

            Console.WriteLine(found.Count == 0
                ? $"  Nothing on file with \"{term.Trim()}\" in it."
                : $"  {found.Count} on file with \"{term.Trim()}\" in it.");
        }

        Console.WriteLine();
        Console.WriteLine($"On the books, as they arrived: {string.Join(", ", registry.Names())}");

    }
}