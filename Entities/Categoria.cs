using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Act_Productos_poo2.Entities
{
    public class Categoria
    {
          public Guid Id { get; set; }
        public string? NombreCat { get; set; }
        public string? Descripcion { get; set;}
        public List<Producto>? productos { get; set; }
    }
}