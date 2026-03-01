using PersonalManagementApp.Models;

namespace PersonalManagementApp.Services;

public class InMemoryPersonalManagementService : IPersonalManagementService
{
    private readonly List<TaskItem> _tasks = [];
    private readonly List<Contact> _contacts = [];

    private int _taskId;
    private int _contactId;

    public InMemoryPersonalManagementService()
    {
        SeedData();
    }

    public IReadOnlyCollection<TaskItem> GetTasks() =>
        _tasks.OrderBy(t => t.IsCompleted).ThenBy(t => t.DueDate).ThenByDescending(t => t.Priority).ToList();

    public TaskItem? GetTaskById(int id) => _tasks.FirstOrDefault(t => t.Id == id);

    public TaskItem AddTask(TaskItem task)
    {
        task.Id = Interlocked.Increment(ref _taskId);
        task.CreatedAtUtc = DateTime.UtcNow;
        _tasks.Add(task);
        return task;
    }

    public bool UpdateTask(TaskItem task)
    {
        var existing = GetTaskById(task.Id);
        if (existing is null)
        {
            return false;
        }

        existing.Title = task.Title;
        existing.Description = task.Description;
        existing.Priority = task.Priority;
        existing.DueDate = task.DueDate;
        existing.IsCompleted = task.IsCompleted;
        existing.Category = task.Category;

        return true;
    }

    public bool DeleteTask(int id)
    {
        var task = GetTaskById(id);
        return task is not null && _tasks.Remove(task);
    }

    public IReadOnlyCollection<Contact> GetContacts() =>
        _contacts.OrderBy(c => c.FullName).ToList();

    public Contact? GetContactById(int id) => _contacts.FirstOrDefault(c => c.Id == id);

    public Contact AddContact(Contact contact)
    {
        contact.Id = Interlocked.Increment(ref _contactId);
        contact.CreatedAtUtc = DateTime.UtcNow;
        _contacts.Add(contact);
        return contact;
    }

    public bool UpdateContact(Contact contact)
    {
        var existing = GetContactById(contact.Id);
        if (existing is null)
        {
            return false;
        }

        existing.FullName = contact.FullName;
        existing.Email = contact.Email;
        existing.PhoneNumber = contact.PhoneNumber;
        existing.Company = contact.Company;
        existing.Notes = contact.Notes;

        return true;
    }

    public bool DeleteContact(int id)
    {
        var contact = GetContactById(id);
        return contact is not null && _contacts.Remove(contact);
    }

    private void SeedData()
    {
        AddTask(new TaskItem
        {
            Title = "Review weekly goals",
            Description = "Check progress and adjust priorities.",
            Priority = PriorityLevel.High,
            DueDate = DateTime.UtcNow.Date.AddDays(1),
            Category = "Planning"
        });

        AddTask(new TaskItem
        {
            Title = "Book dentist appointment",
            Priority = PriorityLevel.Medium,
            Category = "Health"
        });

        AddContact(new Contact
        {
            FullName = "Alex Johnson",
            Email = "alex.johnson@example.com",
            PhoneNumber = "+1 (555) 111-2233",
            Company = "Northwind Logistics",
            Notes = "Met at product meetup"
        });
    }
}
