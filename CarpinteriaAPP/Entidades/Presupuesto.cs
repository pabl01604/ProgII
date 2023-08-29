using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarpinteriaAPP.Entidades
{
    internal class Presupuesto
    {

        public int PresupuestoNro { get; set; }
        public DateTime Fecha { get; set; }
        public string Cliente { get; set; }
        public double CostoMO{ get; set; }
        public double Descuento { get; set; }
        public DateTime FechaBaja { get; set; }

        public List<DetallePresupuesto>  Detalles { get; set; }


        public Presupuesto()
        { 
            Detalles = new List<DetallePresupuesto>();
        }


        public void AgregarDetalle(DetallePresupuesto miDeetalle)
        {
            Detalles.Add(miDeetalle);
        }

        public void QuitarDetalle(int posicion) 
        {

            Detalles.RemoveAt(posicion);               //borra el objeto de la lista q este en la posicion
        }


        public double CalcularTotal()
        {
            double total = 0;
            for (int i = 0; i < Detalles.Count; i++)
            {
                total = Detalles[i].CalcularSubtotal();
            }


            //Alternativa con ForEach

            //foreach (DetallePresupuesto d in Detalles)
            //{
            //    total = d.CalcularSubtotal();
            //}
            return total;

        }


    }
}
