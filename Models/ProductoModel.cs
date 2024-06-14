using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Act_Productos_poo2.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Act_Productos_poo2.Models
{
    public class ProductoModel
    {
         public Guid Id { get; set; }
        public string? NombreProd { get; set; }
        public int Cantidad  { get; set; }

        public string?  Marca { get; set; }

         [DisplayFormat(DataFormatString = "{0:C}")]
        public int Precio { get; set; }
        public Guid? CategoriaId { get; set; }
        public CategoriaModel? CategoriaModel { get; set; }
        public string? NombreCat { get; set; }
        public List<SelectListItem>? ListCategorias { get; set; }
        
        
        
        
    }
}