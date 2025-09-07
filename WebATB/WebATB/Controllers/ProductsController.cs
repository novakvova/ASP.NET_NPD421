using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using WebATB.Data;
using WebATB.Data.Entities;
using WebATB.Interfaces;
using WebATB.Models.Helpers;
using WebATB.Models.Product;

namespace WebATB.Controllers;

public class ProductsController(AppATBDbContext dbContext,
    IMapper mapper,
    IImageService imageService) : Controller
{
    public IActionResult Index()
    {
        var model = dbContext.Products
            .ProjectTo<ProductItemModel>(mapper.ConfigurationProvider)
            .ToList();

        return View(model);
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
        if(model.Images != null && model.Images.Length > 0)
        {
            short priority = 1;
            foreach (var image in model.Images)
            {
                if(image.Length > 0)
                {
                    var imageName = imageService.SaveImageAsync(image).Result;
                    var imageEntity = new ProductImageEntity
                    {
                        ProductId = entity.Id,
                        Priority = priority++,
                        Name = imageName
                    };
                    dbContext.ProductImages.Add(imageEntity);
                }
            }
            dbContext.SaveChanges();
        }
        return RedirectToAction(nameof(Index));
    }
}
