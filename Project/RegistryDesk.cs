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
}