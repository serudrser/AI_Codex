using Microsoft.AspNetCore.Mvc;
using PersonalManagementApp.Services;
using PersonalManagementApp.ViewModels;

namespace PersonalManagementApp.Controllers;

public class HomeController(IPersonalManagementService service) : Controller
{
    public IActionResult Index()
    {
        var tasks = service.GetTasks();
        var contacts = service.GetContacts();

        var model = new DashboardViewModel
        {
            TotalTasks = tasks.Count,
            CompletedTasks = tasks.Count(t => t.IsCompleted),
            OpenTasks = tasks.Count(t => !t.IsCompleted),
            TotalContacts = contacts.Count,
            UpcomingTasks = tasks
                .Where(t => !t.IsCompleted)
                .OrderBy(t => t.DueDate ?? DateTime.MaxValue)
                .Take(5)
                .ToList(),
            RecentContacts = contacts.Take(5).ToList()
        };

        return View(model);
    }
}
