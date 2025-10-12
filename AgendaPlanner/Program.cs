using AgendaPlanner.Models;
using AgendaPlanner.Services;

namespace AgendaPlanner;

internal static class Program
{
    private static readonly AgendaManager Agenda = new();

    private static void Main()
    {
        Console.WriteLine("Welcome to the Agenda Builder!\n");
        var exitRequested = false;

        while (!exitRequested)
        {
            DisplayMenu();
            Console.Write("Select an option: ");
            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    AddAgendaItem();
                    break;
                case "2":
                    ListAgendaItems();
                    break;
                case "3":
                    RemoveAgendaItem();
                    break;
                case "4":
                    SeedSampleAgenda();
                    break;
                case "0":
                    exitRequested = true;
                    break;
                default:
                    Console.WriteLine("Unknown option. Please try again.\n");
                    break;
            }
        }

        Console.WriteLine("Thanks for using the Agenda Builder. Goodbye!");
    }

    private static void DisplayMenu()
    {
        Console.WriteLine("Menu");
        Console.WriteLine("1. Add agenda item");
        Console.WriteLine("2. View agenda");
        Console.WriteLine("3. Remove agenda item");
        Console.WriteLine("4. Load sample agenda");
        Console.WriteLine("0. Exit");
    }

    private static void AddAgendaItem()
    {
        Console.Write("Title: ");
        var title = Console.ReadLine()?.Trim();
        if (string.IsNullOrWhiteSpace(title))
        {
            Console.WriteLine("A title is required.\n");
            return;
        }

        var startTime = PromptForDateTime("Start time (e.g. 2024-05-27 14:30): ");
        if (startTime is null)
        {
            return;
        }

        var duration = PromptForDuration("Duration in minutes: ");
        if (duration is null)
        {
            return;
        }

        Console.Write("Location (optional): ");
        var location = Console.ReadLine();

        Console.Write("Notes (optional): ");
        var notes = Console.ReadLine();

        Agenda.AddItem(new AgendaItem(title, startTime.Value, duration.Value, location, notes));
        Console.WriteLine("Item added successfully!\n");
    }

    private static void ListAgendaItems()
    {
        var items = Agenda.Items;
        if (items.Count == 0)
        {
            Console.WriteLine("No agenda items yet.\n");
            return;
        }

        Console.WriteLine("\nYour Agenda:");
        for (var i = 0; i < items.Count; i++)
        {
            Console.WriteLine($"[{i + 1}]\n{items[i]}");
        }
    }

    private static void RemoveAgendaItem()
    {
        var items = Agenda.Items;
        if (items.Count == 0)
        {
            Console.WriteLine("No agenda items to remove.\n");
            return;
        }

        Console.Write("Enter the number of the item to remove: ");
        var input = Console.ReadLine();
        if (!int.TryParse(input, out var index) || index <= 0 || index > items.Count)
        {
            Console.WriteLine("Invalid selection.\n");
            return;
        }

        if (Agenda.RemoveItem(index - 1))
        {
            Console.WriteLine("Item removed.\n");
        }
    }

    private static DateTime? PromptForDateTime(string prompt)
    {
        Console.Write(prompt);
        var input = Console.ReadLine();

        if (!DateTime.TryParse(input, out var value))
        {
            Console.WriteLine("Invalid date/time format.\n");
            return null;
        }

        return value;
    }

    private static TimeSpan? PromptForDuration(string prompt)
    {
        Console.Write(prompt);
        var input = Console.ReadLine();

        if (!int.TryParse(input, out var minutes) || minutes <= 0)
        {
            Console.WriteLine("Duration must be a positive number of minutes.\n");
            return null;
        }

        return TimeSpan.FromMinutes(minutes);
    }

    private static void SeedSampleAgenda()
    {
        Agenda.AddItem(new AgendaItem(
            "Project Kickoff Meeting",
            DateTime.Today.AddHours(9),
            TimeSpan.FromMinutes(60),
            "Conference Room A",
            "Review milestones and deliverables."));

        Agenda.AddItem(new AgendaItem(
            "Client Lunch",
            DateTime.Today.AddHours(12),
            TimeSpan.FromMinutes(90),
            "Downtown Bistro",
            "Discuss partnership opportunities."));

        Agenda.AddItem(new AgendaItem(
            "Code Review",
            DateTime.Today.AddHours(15),
            TimeSpan.FromMinutes(45),
            "Online",
            "Focus on the new API endpoints."));

        Console.WriteLine("Sample agenda created!\n");
    }
}
