using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarpinteriaAPP.Entidades
{
    internal class Producto
    {
        //Propiedades

        public int pProdutoNro { get; set; }

        public string Nombre { get; set; }

        public double Precio { get; set; }

        public bool Activo{ get; set; }
            
        public  Producto() {
            pProdutoNro = 0;
            Nombre = string.Empty;
            Precio = 0;
            Activo= true;
        
        
        }  

        public Producto(  int productoNro, string nombre, double precio)
        {
            pProdutoNro = productoNro;
            Nombre = nombre;
            Precio = precio;
            Activo = true;

        }









    }
}
