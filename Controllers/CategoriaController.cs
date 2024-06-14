using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Act_Productos_poo2.Entities;
using Act_Productos_poo2.Models;
using Microsoft.AspNetCore.Mvc;

namespace Act_Productos_poo2.Controllers
{
    public class CategoriaController: Controller
    {
        private readonly ILogger<CategoriaController> _logger;
        private readonly ApplicationDbContext _context;
         public CategoriaController(ILogger<CategoriaController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }
         public IActionResult ListCategoria()
        {
           
             List<CategoriaModel> list = _context.Categorias.Select(c => new CategoriaModel
            {
                Id = c.Id,
                NombreCat = c.NombreCat,
                Descripcion=c.Descripcion
            }).ToList();

            return View(list);
        }

          public IActionResult CategoriaAdd()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CategoriaAdd(CategoriaModel model)
        {
            if (ModelState.IsValid)
            {
                Categoria categoria = new Categoria
                {
                    Id = Guid.NewGuid(),
                    NombreCat = model.NombreCat,
                    Descripcion = model.Descripcion,
                };

                _context.Categorias.Add(categoria);
                _context.SaveChanges();

                return RedirectToAction("ListCategoria");
            }
            return View(model);
        }

         public IActionResult CategoriaShowToDelete(Guid id)
        {
            Categoria? cat = this._context.Categorias.FirstOrDefault(p => p.Id == id);
            if (cat == null)
            {
                return RedirectToAction("ListCategoria");
            }

            CategoriaModel model = new CategoriaModel
            {
                Id = cat.Id,
                NombreCat = cat.NombreCat,
                Descripcion = cat.Descripcion,
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult CategoriaDelete(Guid id)
        {
            var categoria = _context.Categorias.FirstOrDefault(p => p.Id == id);
            if (categoria != null)
            {
                _context.Categorias.Remove(categoria);
                _context.SaveChanges();
            }

            return RedirectToAction("ListCategoria");
        }

         public IActionResult CategoriaEdit(Guid id)
        {
            var cate = _context.Categorias.FirstOrDefault(p => p.Id == id);
            if (cate == null)
            {
                return RedirectToAction("ListCategoria");
            }

            var model = new CategoriaModel
            {
                Id = cate.Id,
                NombreCat = cate.NombreCat,
                Descripcion = cate.Descripcion
            };

            return View(model);
        }

          [HttpPost]
        public IActionResult CategoriaEdit(CategoriaModel model)
        {
            if (ModelState.IsValid)
            {
                var cate = _context.Categorias.FirstOrDefault(p => p.Id == model.Id);
                if (cate != null)
                {
                    cate.NombreCat = model.NombreCat;
                    cate.Descripcion = model.Descripcion;

                    _context.SaveChanges();
                    return RedirectToAction("ListCategoria");
                }
            }

            return View(model);
        }


        
    }
    
}