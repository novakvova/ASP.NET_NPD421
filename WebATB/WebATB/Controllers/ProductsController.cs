using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using WebATB.Data;
using WebATB.Data.Entities;
using WebATB.Models.Helpers;
using WebATB.Models.Product;

namespace WebATB.Controllers;

public class ProductsController(AppATBDbContext dbContext,
    IMapper mapper) : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]    
    public IActionResult Create()
    {
        ProductCreateModel model = new ProductCreateModel();
        model.Categories = dbContext.Categories
            .ProjectTo<SelectItemHelper>(mapper.ConfigurationProvider)
            .ToList();
        return View(model);
    }

    [HttpPost]
    public IActionResult Create(ProductCreateModel model)
    {
        if(!ModelState.IsValid)
        {
            model.Categories = dbContext.Categories
                .ProjectTo<SelectItemHelper>(mapper.ConfigurationProvider)
                .ToList();
            return View(model);
        }
        var entity = mapper.Map<ProductEntity>(model);
        dbContext.Products.Add(entity);
        dbContext.SaveChanges();
        return RedirectToAction(nameof(Index));
    }
}
