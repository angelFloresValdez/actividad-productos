using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Act_Productos_poo2.Models;
using Act_Productos_poo2.Entities;

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

        public IActionResult ProductosAdd()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ProductosAdd(ProductoModel model)
        {
            if (ModelState.IsValid)
            {
                Producto producto = new Producto
                {
                    Id = Guid.NewGuid(),
                    NombreProd = model.NombreProd,
                    Marca = model.Marca,
                    Cantidad = model.Cantidad,
                    Precio = model.Precio
                };

                _context.Productos.Add(producto);
                _context.SaveChanges();

                return RedirectToAction("Productos");
            }
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
            var product = _context.Productos.FirstOrDefault(p => p.Id == id);
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
                Precio = product.Precio
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

                    _context.SaveChanges();
                    return RedirectToAction("Productos");
                }
            }

            return View(model);
        }

        public IActionResult Productos()
        {
            List<ProductoModel> list = _context.Productos.Select(s => new ProductoModel
            {
                Id = s.Id,
                NombreProd = s.NombreProd,
                Marca = s.Marca,
                Cantidad = s.Cantidad,
                Precio = s.Precio
            }).ToList();

            return View(list);
        }
    }
}