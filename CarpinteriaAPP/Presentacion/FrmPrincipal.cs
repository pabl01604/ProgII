﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CarpinteriaAPP.Presentacion;

namespace CarpinteriaAPP
{
    public partial class FrmPrincipal : Form
    {
        public FrmPrincipal()
        {
            InitializeComponent();
        }
        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmNuevoPresupuesto nuevo = new FrmNuevoPresupuesto();
            nuevo.ShowDialog();
        }


        //cuando haga click en soporte me tiene q abrir el ABM de producto
    }
}
