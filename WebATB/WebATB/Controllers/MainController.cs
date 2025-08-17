using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;
using WebATB.Data;
using WebATB.Data.Entities;

namespace WebATB.Controllers;

//звичайни клас, який наслідує Controller

public class MainController(AppATBDbContext dbContext) : Controller
{
    //метод контролер - action method
    public IActionResult Index()
    {
        var list = dbContext.Categories.ToList();
        return View(list);
    }
    //відображаємо сторінку створення категорії
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    //обробляємо форму створення категорії
    [HttpPost]
    public IActionResult Create(CategoryEntity model)
    {
        dbContext.Categories.Add(model);
        dbContext.SaveChanges();

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public IActionResult Delete(int id)
    {
        var category = dbContext.Categories.FirstOrDefault(c => c.Id == id);
        if (category == null)
        {
            return NotFound();
        }
        return View(category);
    }

    [HttpPost]
    public IActionResult Delete(CategoryEntity model)
    {
        var category = dbContext.Categories.FirstOrDefault(c => c.Id == model.Id);
        if (category == null)
        {
            return NotFound();
        }
        dbContext.Categories.Remove(category);
        dbContext.SaveChanges();
        return RedirectToAction(nameof(Index));
    }
}
