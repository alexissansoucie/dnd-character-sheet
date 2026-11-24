using System.Text.Json;
public static class RegistryDesk
{
    public static void Remove(Registry registry)
    {
        while (true)
        {
            Console.Write("Take one off the books (Enter to skip): ");
            string? typed = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(typed))
            {
                return;
            }

            if (registry.Remove(typed.Trim()))
            {
                Console.WriteLine("Removed.");
                return;
            }

            Console.WriteLine("Nothing by that name.");
        }
    }
        public static void Import(Registry registry)
    {
        while (true)
        {
            Console.Write("File to import (Enter to skip): ");
            string? typed = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(typed))
            {
                return;
            }

            string path = typed.Trim();

            try
            {
                int added = registry.Import(path);
                Console.WriteLine($"{added} new.");
                return;
            }
            catch (IOException)
            {
                Console.WriteLine($"No file at \"{path}\".");
            }
            catch (JsonException)
            {
                Console.WriteLine("Refused: that file is damaged. Nothing was added.");
                return;
            }
        }
    }
}