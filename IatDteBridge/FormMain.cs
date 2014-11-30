using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace IatDteBridge
{
    public partial class FormMain : Form
    {

        PingProcess ping = new PingProcess();
        ProcesoIat proc = new ProcesoIat();
        ProcesoPaquete propack = new ProcesoPaquete();
        ProcesoLibroCompra procLibComp = new ProcesoLibroCompra();
        procesoLibroVenta procLibVta = new procesoLibroVenta();
        ProcesoLibroGuias procLibGuias = new ProcesoLibroGuias();
        ProcesoPaqueteXml procFromXml = new ProcesoPaqueteXml();

        public FormMain()
        {
            InitializeComponent();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Connect conn = new Connect();
            
            string response = conn.ping();

           Console.WriteLine("respuesta = {0}.", response);

        }

        private void FormMain_Load(object sender, EventArgs e)
        {

        }






        private void button6_Click(object sender, EventArgs e)
        {
            
            //ping.StartPing();


           // xmlAdmin a = new xmlAdmin();

          //  a.PruebaTimbreDD();
            

        }

        private void button7_Click(object sender, EventArgs e)
        {
           // ping.StopPing();
            proc.StopProcessIat();
        }

        private void datosEmpresaToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void empresaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormEmisor emisor = new FormEmisor();
            emisor.Show();
        }

        private void adminCAFToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormAdminCaf adminCaf = new FormAdminCaf();
            adminCaf.Show();
        }

        private void FormMain_Load_1(object sender, EventArgs e)
        {

           // proc.StartProcessIat();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            propack.procesoPaquete();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            proc.StartProcessIat();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            procLibComp.doLibroCompra();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            procLibVta.doLibroVta();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            procLibGuias.doLibroGuias();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            procFromXml.procesoPaqueteXml(textBox1.Text,textBox2.Text);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {

                System.IO.FileInfo fi = null;
                try
                {
                    fi = new System.IO.FileInfo(openFileDialog1.FileName);

                    textBox1.Text = openFileDialog1.FileName;
                }
                catch (System.IO.FileNotFoundException ex)
                {
                    Console.WriteLine(ex.Message);
                }

            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (openFileDialog2.ShowDialog() == DialogResult.OK)
            {

                System.IO.FileInfo fi = null;
                try
                {
                    fi = new System.IO.FileInfo(openFileDialog2.FileName);

                    textBox2.Text = openFileDialog2.FileName;
                }
                catch (System.IO.FileNotFoundException ex)
                {
                    Console.WriteLine(ex.Message);
                }

            }
        }



  
    }
}
