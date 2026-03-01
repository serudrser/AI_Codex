using PersonalManagementApp.Models;

namespace PersonalManagementApp.ViewModels;

public class DashboardViewModel
{
    public int TotalTasks { get; init; }
    public int CompletedTasks { get; init; }
    public int OpenTasks { get; init; }
    public int TotalContacts { get; init; }

    public IReadOnlyCollection<TaskItem> UpcomingTasks { get; init; } = [];
    public IReadOnlyCollection<Contact> RecentContacts { get; init; } = [];
}
