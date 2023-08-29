using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using CarpinteriaAPP.Entidades;

namespace CarpinteriaAPP.Presentacion
{
    public partial class FrmNuevoPresupuesto : Form
    {

        Presupuesto nuevo = null;





        public FrmNuevoPresupuesto()
        {
            InitializeComponent();
            nuevo = new Presupuesto();

        }

        private void FrmNuevoPresupuesto_Load(object sender, EventArgs e)
        {
            txtFecha.Text = DateTime.Today.ToShortDateString();  //Formato Fecha dd/mm/aaaa

            txtCliente.Text = "Consumidor Final";
            txtDto.Text = "0";
            txtCantidad.Text = "1";

            ProximoPresupuesto();
            CargarProductos();
        }





        private void CargarProductos()
        {
            SqlConnection conexion = new SqlConnection();
            //conexion.ConnectionString = @"Data Source=172.16.10.196;Initial Catalog=Carpinteria_2023;User ID=alumno1w1; password=alumno1w1";

            conexion.ConnectionString = @"Data Source=LAPTOP-5F2RB0QP\SQLEXPRESS;Initial Catalog=carpinteria_db;Integrated Security=True";

            conexion.Open();
            SqlCommand comando = new SqlCommand();
            comando.Connection = conexion;
            comando.CommandType = CommandType.StoredProcedure;
            comando.CommandText = "SP_CONSULTAR_PRODUCTOS";
            DataTable tabla = new DataTable();
            tabla.Load(comando.ExecuteReader());
            conexion.Close();

            cboProducto.DataSource = tabla;


            cboProducto.ValueMember = tabla.Columns[0].ColumnName;
            cboProducto.DisplayMember = tabla.Columns[1].ColumnName;
        }

        private void ProximoPresupuesto()
        {
            SqlConnection conexion = new SqlConnection();
            //conexion.ConnectionString = @"Data Source=172.16.10.196;Initial Catalog=Carpinteria_2023;User ID=alumno1w1; password=alumno1w1";
            conexion.ConnectionString = @"Data Source=LAPTOP-5F2RB0QP\SQLEXPRESS;Initial Catalog=carpinteria_db;Integrated Security=True";

            conexion.Open();
            SqlCommand comando = new SqlCommand();
            comando.Connection = conexion;
            comando.CommandType = CommandType.StoredProcedure;
            comando.CommandText = "SP_PROXIMO_ID";

            SqlParameter param = new SqlParameter();   //CREO EL PARAMETRO PARA RECIBIR LO Q ME MANDA EL PROCEDIMIENTO ALMACENADO

            param.ParameterName = "@next";      //Esta linea y la de abajo puede ir en el constructor DE ARRIBA
            param.SqlDbType = SqlDbType.Int;

            param.Direction = ParameterDirection.Output;

            comando.Parameters.Add(param);
            comando.ExecuteNonQuery();

            conexion.Close();

            lblPresupuestoNro.Text = lblPresupuestoNro.Text + " " + param.Value.ToString(); //hay q poner as+i para q salga el valor del parametro y no el nombre
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (cboProducto.SelectedIndex == -1)
            {
                MessageBox.Show("Seleccione un producto...", "Control", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (string.IsNullOrEmpty(txtCantidad.Text) || !int.TryParse(txtCantidad.Text, out _))
            {
                MessageBox.Show("Debe ingresar una cantidad valida...", "Control", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            foreach (DataGridViewRow row in dgvDetalles.Rows)
            {
                if (row.Cells["ColProducto"].Value.ToString() == cboProducto.Text)
                {
                    MessageBox.Show("eL ITEM YA ESTA PRESUPUESTADO", "Control", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
            }

            DataRowView item = (DataRowView)cboProducto.SelectedItem;

            int prodNro = Convert.ToInt32(item.Row.ItemArray[0]);

            string nombre = item.Row.ItemArray[1].ToString();

            double pre = Convert.ToDouble(item.Row.ItemArray[2]);


            Producto p = new Producto(prodNro, nombre, pre);


            int cant = Convert.ToInt32(txtCantidad.Text);

            DetallePresupuesto dp = new DetallePresupuesto(p, cant);


            nuevo.AgregarDetalle(dp);
            //dgvDetalles.Rows.Add(new object[] { dp.Producto.pProdutoNro,dp.Producto.Nombre,dp.Producto.Precio,dp.Cantidad});

            dgvDetalles.Rows.Add(new object[] { prodNro, nombre, pre, cant, "Quitar" });

            CalcularTotales();
        }

        private void CalcularTotales()   //Lo hacemos en otro metodo porque 
        {
            txtSubTotal.Text = nuevo.CalcularTotal().ToString();

            if (string.IsNullOrEmpty(txtDto.Text) && int.TryParse(txtDto.Text, out _))
            {
                double desc = nuevo.CalcularTotal() + Convert.ToDouble(txtDto.Text) / 100;
                txtTotal.Text = (nuevo.CalcularTotal() - desc).ToString();
            }
                

            //txtTotal.Text = (nuevo.CalcularTotal() - desc).ToString();

        }

        private void dgvDetalles_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if(dgvDetalles.CurrentCell.ColumnIndex== 4) 
            {
                nuevo.QuitarDetalle(dgvDetalles.CurrentRow.Index);
                dgvDetalles.Rows.RemoveAt(dgvDetalles.CurrentRow.Index);
                CalcularTotales();
            }
        }
    }
}


// 1 de los ejercicios de la guia Si ya tenemos el repositorio, hacer modelo de datos(diagrama de bd y el uml ) y el formulario de carga.  subir el proyecto a github y en la uv se sube e link 
