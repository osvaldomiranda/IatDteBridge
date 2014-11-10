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



  
    }
}
