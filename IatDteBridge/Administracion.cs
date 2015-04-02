﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace IatDteBridge
{
    public partial class Administracion : Form
    {
        
        ProcesoPaquete propack = new ProcesoPaquete();
        procesoLibroVenta procLibVta = new procesoLibroVenta();
        ProcesoLibroCompra procLibComp = new ProcesoLibroCompra();
        ProcesoLibroGuias procLibGuias = new ProcesoLibroGuias();

        public Administracion()
        {
            InitializeComponent();
        }

        private void datosEmisorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormEmisor formemisor = new FormEmisor();
            formemisor.MdiParent = this;
            formemisor.Show();
        }

        private void empresaToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void pdfToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void Administracion_Load(object sender, EventArgs e)
        {

        }

        private void procesaPaqueteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            propack.procesoPaquete();
        }

        private void libroVentasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            procLibVta.doLibroVta();
        }

        private void libroComprasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            procLibComp.doLibroCompra();
        }

        private void libroGuiasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            procLibGuias.doLibroGuias();
        }
    }
}
