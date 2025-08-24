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
        var existingCategory = dbContext.Categories.FirstOrDefault(c => c.Name == model.Name);
        if (existingCategory != null)
        {
            //Записуємо в стан моделі помилку - Поле Name
            ModelState.AddModelError("Name", $"Категорія з такою назвою вже існує id = {existingCategory.Id}");
        }

        if (!ModelState.IsValid) //Якщо модель не валідна, то вертаємося на форму створення
        {
            return View(model);
        }

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


    //відображаємо сторінку редагування категорії
    [HttpGet]
    public IActionResult Update(int id)
    {
        //мепимо з БД в модель CategoryUpdateModel
        var model = dbContext.Categories
            .ProjectTo<CategoryUpdateModel>(mapper.ConfigurationProvider)
            .FirstOrDefault(c => c.Id == id);

        return View(model);
    }

    [HttpPost]
    public IActionResult Update(CategoryUpdateModel model)
    {
        var entity = dbContext.Categories.FirstOrDefault(c => c.Id == model.Id);
        if (entity == null)
            return NotFound(); //Якщо не знайшли категорію в БД - повертаємо 404 - де його брати

        var existingCategory = dbContext.Categories
            .Where(c => c.Id != model.Id)
            .FirstOrDefault(c => c.Name == model.Name);

        if (existingCategory != null)
        {
            //Записуємо в стан моделі помилку - Поле Name
            ModelState.AddModelError("Name", $"Категорія з такою назвою вже існує id = {existingCategory.Id}");
        }

        if (!ModelState.IsValid) //Якщо модель не валідна, то вертаємося на форму створення
        {
            model.Image = $"/images/{entity.Image}"; //щоб показати поточне фото 
            return View(model);
        }

        var fileName = string.Empty;
        if (model.ImageFile != null)
        {
            fileName = $"{Guid.NewGuid()}{Path.GetExtension(model.ImageFile.FileName)}";
            var dir = Path.Combine(Directory.GetCurrentDirectory(), "images");
            //Directory.CreateDirectory(dir);
            var filePath = Path.Combine(dir, fileName);
            using var stream = System.IO.File.Create(filePath);
            model.ImageFile.CopyTo(stream);
        }

        if (string.IsNullOrEmpty(fileName) == false) //Якщо завантажено нове фото
        {
            //Видаляємо старе фото
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "images", entity.Image);
            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }
            entity.Image = fileName;
        }

        //Зберігає поточного об'єкта БД і перезаписує потрібні поля з моделі
        // Оновлюємо існуючий об’єкт
        mapper.Map(model, entity);
        dbContext.SaveChanges(); //Оновлюємо в БД - автоматично зробить UPDATE SQL запит

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public IActionResult Delete(int id)
    {
        var category = dbContext.Categories
            .ProjectTo<CategoryItemModel>(mapper.ConfigurationProvider)
            .FirstOrDefault(c => c.Id == id);
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
        if (category.Image != null)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "images", category.Image);
            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }
        }
        dbContext.Categories.Remove(category);
        dbContext.SaveChanges();
        return RedirectToAction(nameof(Index));
    }
}
