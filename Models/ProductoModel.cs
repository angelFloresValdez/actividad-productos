using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Act_Productos_poo2.Models
{
    public class ProductoModel
    {
         public Guid Id { get; set; }
        public string NombreProd { get; set; }
        public int Cantidad  { get; set; }

        public string  Marca { get; set; }

         [DisplayFormat(DataFormatString = "{0:C}")]
        public int Precio { get; set; }
        
        
    }
}