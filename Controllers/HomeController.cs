using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Act_Productos_poo2.Models;
using Act_Productos_poo2.Entities;
using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Act_Productos_poo2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

       public async Task<IActionResult> ProductosAdd()
{
    ProductoModel producto = new ProductoModel();
    producto.ListCategorias = await _context.Categorias.Select(c => new SelectListItem()
    {
        Value = c.Id.ToString(),
        Text = c.NombreCat
    }).ToListAsync();

    return View(producto);
}

[HttpPost]
public async Task<IActionResult> ProductosAdd(ProductoModel model)
{
    if (ModelState.IsValid)
    {
        Producto producto = new Producto
        {
            Id = Guid.NewGuid(),
            NombreProd = model.NombreProd,
            Marca = model.Marca,
            Cantidad = model.Cantidad,
            Precio = model.Precio,
            CategoriaId = model.CategoriaId  
        };

        _context.Productos.Add(producto);
        await _context.SaveChangesAsync();

        return RedirectToAction("Productos");
    }

    model.ListCategorias = await _context.Categorias.Select(c => new SelectListItem()
    {
        Value = c.Id.ToString(),
        Text = c.NombreCat
    }).ToListAsync();

    return View(model);
}


        public IActionResult ProductoShowToDelete(Guid id)
        {
            Producto? product = this._context.Productos.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return RedirectToAction("Productos");
            }

            ProductoModel model = new ProductoModel
            {
                Id = product.Id,
                NombreProd = product.NombreProd,
                Marca = product.Marca,
                Cantidad = product.Cantidad,
                Precio = product.Precio
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult ProductoDelete(Guid id)
        {
            var producto = _context.Productos.FirstOrDefault(p => p.Id == id);
            if (producto != null)
            {
                _context.Productos.Remove(producto);
                _context.SaveChanges();
            }

            return RedirectToAction("Productos");
        }

       public IActionResult ProductoEdit(Guid id)
{
    var product = _context.Productos.Include(p => p.Categoria).FirstOrDefault(p => p.Id == id);
    if (product == null)
    {
        return RedirectToAction("Productos");
    }

    var model = new ProductoModel
    {
        Id = product.Id,
        NombreProd = product.NombreProd,
        Marca = product.Marca,
        Cantidad = product.Cantidad,
        Precio = product.Precio,
        CategoriaId = product.CategoriaId,  
        ListCategorias = _context.Categorias.Select(s => new SelectListItem
        {
            Value = s.Id.ToString(),
            Text = s.NombreCat
        }).ToList()
    };

    return View(model);
}

[HttpPost]
public IActionResult ProductoEdit(ProductoModel model)
{
    if (ModelState.IsValid)
    {
        var product = _context.Productos.FirstOrDefault(p => p.Id == model.Id);
        if (product != null)
        {
            product.NombreProd = model.NombreProd;
            product.Marca = model.Marca;
            product.Cantidad = model.Cantidad;
            product.Precio = model.Precio;
            product.CategoriaId = model.CategoriaId; 

            _context.SaveChanges();
            return RedirectToAction("Productos");
        }
    }

   
    model.ListCategorias = _context.Categorias.Select(c => new SelectListItem
    {
        Value = c.Id.ToString(),
        Text = c.NombreCat
    }).ToList();

    return View(model);
}

        public async Task<IActionResult> Productos()
{
    List<ProductoModel> productos = await _context.Productos
        .Include(p => p.Categoria) 
        .Select(p => new ProductoModel
        {
            Id = p.Id,
            NombreProd = p.NombreProd,
            Marca = p.Marca,
            Cantidad = p.Cantidad,
            Precio = p.Precio,
            NombreCat = p.Categoria.NombreCat 
        })
        .ToListAsync();

    return View(productos);
}

      
    }
}