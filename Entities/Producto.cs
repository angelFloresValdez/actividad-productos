using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Act_Productos_poo2.Entities
{
    public class Producto
    {

         public Guid Id { get; set; }
        public string NombreProd { get; set; }
        public int Cantidad  { get; set; }

        public string  Marca { get; set; }

        public int Precio  { get; set; }


    }
}