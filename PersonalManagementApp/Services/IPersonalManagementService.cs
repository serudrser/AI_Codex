using PersonalManagementApp.Models;

namespace PersonalManagementApp.Services;

public interface IPersonalManagementService
{
    IReadOnlyCollection<TaskItem> GetTasks();
    TaskItem? GetTaskById(int id);
    TaskItem AddTask(TaskItem task);
    bool UpdateTask(TaskItem task);
    bool DeleteTask(int id);

    IReadOnlyCollection<Contact> GetContacts();
    Contact? GetContactById(int id);
    Contact AddContact(Contact contact);
    bool UpdateContact(Contact contact);
    bool DeleteContact(int id);
}
