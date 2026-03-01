using Microsoft.AspNetCore.Mvc;
using PersonalManagementApp.Models;
using PersonalManagementApp.Services;

namespace PersonalManagementApp.Controllers;

public class TasksController(IPersonalManagementService service) : Controller
{
    public IActionResult Index() => View(service.GetTasks());

    public IActionResult Create() => View(new TaskItem { DueDate = DateTime.UtcNow.Date });

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(TaskItem task)
    {
        if (!ModelState.IsValid)
        {
            return View(task);
        }

        service.AddTask(task);
        return RedirectToAction(nameof(Index));
    }

    public IActionResult Edit(int id)
    {
        var task = service.GetTaskById(id);
        return task is null ? NotFound() : View(task);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, TaskItem task)
    {
        if (id != task.Id)
        {
            return BadRequest();
        }

        if (!ModelState.IsValid)
        {
            return View(task);
        }

        return service.UpdateTask(task) ? RedirectToAction(nameof(Index)) : NotFound();
    }

    public IActionResult Delete(int id)
    {
        var task = service.GetTaskById(id);
        return task is null ? NotFound() : View(task);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int id)
    {
        service.DeleteTask(id);
        return RedirectToAction(nameof(Index));
    }
}
