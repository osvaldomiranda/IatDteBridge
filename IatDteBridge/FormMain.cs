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

        private void button2_Click(object sender, EventArgs e)
        {
            File archivo = new File();

            archivo.OpenFile();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Pdf pdf = new Pdf();
            pdf.OpenPdf();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Timbre timbre = new Timbre();
            timbre.CreaTimbre();

            Console.WriteLine("Timbre creado!!");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Timbre codbarra = new Timbre();
            codbarra.CreaCodigoBarra();
            Console.WriteLine("Codigo de barra creado!!");

        }



  
    }
}
