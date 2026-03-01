using Microsoft.AspNetCore.Mvc;
using PersonalManagementApp.Models;
using PersonalManagementApp.Services;

namespace PersonalManagementApp.Controllers;

public class ContactsController(IPersonalManagementService service) : Controller
{
    public IActionResult Index() => View(service.GetContacts());

    public IActionResult Create() => View(new Contact());

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Contact contact)
    {
        if (!ModelState.IsValid)
        {
            return View(contact);
        }

        service.AddContact(contact);
        return RedirectToAction(nameof(Index));
    }

    public IActionResult Edit(int id)
    {
        var contact = service.GetContactById(id);
        return contact is null ? NotFound() : View(contact);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, Contact contact)
    {
        if (id != contact.Id)
        {
            return BadRequest();
        }

        if (!ModelState.IsValid)
        {
            return View(contact);
        }

        return service.UpdateContact(contact) ? RedirectToAction(nameof(Index)) : NotFound();
    }

    public IActionResult Delete(int id)
    {
        var contact = service.GetContactById(id);
        return contact is null ? NotFound() : View(contact);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int id)
    {
        service.DeleteContact(id);
        return RedirectToAction(nameof(Index));
    }
}
