using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using WebATB.Data;
using WebATB.Data.Entities;
using WebATB.Models.Category;

namespace WebATB.Controllers;

//звичайни клас, який наслідує Controller

public class MainController(AppATBDbContext dbContext, IMapper mapper) : Controller
{
    //метод контролер - action method
    public async Task<IActionResult> Index()
    {
        //var list = dbContext.Categories.ToList();

        //var model = await dbContext.Categories.Select(x => new CategoryItemModel
        //{
        //    Id = x.Id,
        //    Name = x.Name,
        //    Image = x.Image
        //}).ToListAsync();

        var model = await dbContext.Categories
            .ProjectTo<CategoryItemModel>(mapper.ConfigurationProvider)
            .ToListAsync();

        return View(model);
    }
    //відображаємо сторінку створення категорії
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    //обробляємо форму створення категорії
    [HttpPost]
    public IActionResult Create(CategoryCreateModel model)
    {
        var fileName = string.Empty;
        if (model.Image != null)
        {
            fileName = $"{Guid.NewGuid()}{Path.GetExtension(model.Image.FileName)}";
            var dir = Path.Combine(Directory.GetCurrentDirectory(), "images");
            //Directory.CreateDirectory(dir);
            var filePath = Path.Combine(dir, fileName);
            using var stream = System.IO.File.Create(filePath);
            model.Image.CopyTo(stream);
        }

        var entity = mapper.Map<CategoryEntity>(model);

        entity.Image = fileName;
        dbContext.Categories.Add(entity);
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
