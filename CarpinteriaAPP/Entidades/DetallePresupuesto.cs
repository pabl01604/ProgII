using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarpinteriaAPP.Entidades
{
    internal class DetallePresupuesto
    {

        public Producto Producto { get; set; }
        public int Cantidad { get; set; }



        public DetallePresupuesto(Producto p, int cant)
        {
            Producto = p;
            Cantidad = cant;

        }

        //con Ctor tab tab se hace solo el constructor

        public double CalcularSubtotal()
        {
            return Cantidad * Producto.Precio;
        }



    }
}
