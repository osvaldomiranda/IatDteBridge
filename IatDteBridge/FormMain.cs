using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using iTextSharp.text.pdf;
using System.IO;

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
        ProcesoContingencia procContig = new ProcesoContingencia();
        public CheckBox checkbox1 = new CheckBox();
        LocalDataBase ldb = new LocalDataBase();

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
            
            ping.StartPing();


        }

        private void button7_Click(object sender, EventArgs e)
        {
            ping.StopPing();
         
        }

        private void datosEmpresaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Administracion admin = new Administracion();
            admin.Show();
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
            //Creadirectorios
            Directory.CreateDirectory(@"c://IatFiles");
            Directory.CreateDirectory(@"c://IatFiles/cafs");
            Directory.CreateDirectory(@"c://IatFiles/config");
            Directory.CreateDirectory(@"c://IatFiles/file");
            Directory.CreateDirectory(@"c://IatFiles/file/libroCompra");
            Directory.CreateDirectory(@"c://IatFiles/file/libroVenta");
            Directory.CreateDirectory(@"c://IatFiles/file/libroGuia");
            Directory.CreateDirectory(@"c://IatFiles/file/pdf");
            Directory.CreateDirectory(@"c://IatFiles/file/xml");
            Directory.CreateDirectory(@"c://IatFiles/file/xml/enviado");
            Directory.CreateDirectory(@"c://IatFiles/file/xml/enviomasivo");
            Directory.CreateDirectory(@"c://IatFiles/file/xml/enviounitario");
            Directory.CreateDirectory(@"c://IatFiles/fileprocess");
            // crea base de datos
            ldb.creaDB();
            // Inicia proceso IAt
            proc.StartProcessIat();
            this.label4.Text = "IatProcess En Ejecución";
            this.timer1.Start();
            //Inicia Proceso contingencia
            procContig.StartProcessConting();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            propack.procesoPaquete();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            proc.StartProcessIat();
            this.label4.Text = "IatProcess En Ejecución";
            this.timer1.Start();

            procContig.StartProcessConting();
            
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

        private void button12_Click(object sender, EventArgs e)
        {
     
        }

        private void button12_Click_1(object sender, EventArgs e)
        {
            PdfMasivo pdfM = new PdfMasivo();

            pdfM.pdfMasivo();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            Log l = new Log();
 
            if (ldb.creaDB())
            {
                l.addLog("Creacion de DB", "OK");
                MessageBox.Show("Base de Datos Se ha creado con Exito");
            }
            else
            {
                MessageBox.Show("Base de datos ya Existe");
            }

            l.verLog();
        }

        private void button14_Click(object sender, EventArgs e)
        {

            Environment.Exit(0);
        }

        private void button15_Click(object sender, EventArgs e)
        {
            
            EnvioMasivo enmas = new EnvioMasivo();
            enmas.envioMasivo();
            
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click_1(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (progressBar1.Value == 100)
            {
                progressBar1.Value = 1;
            }else{
            this.progressBar1.Increment(1);
            }
        }


        private void progressBar1_Click(object sender, EventArgs e)
        {

        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void button16_Click(object sender, EventArgs e)
        {
            EnvioMasivo envM = new EnvioMasivo();
            envM.envioMasivo();
        }

        private void button17_Click(object sender, EventArgs e)
        {
            Log l = new Log();
            l.verLog();

        }

        private void button18_Click(object sender, EventArgs e)
        {
            ReenvioSql reenv = new ReenvioSql();
            reenv.verReenv();
        }

        private void button19_Click(object sender, EventArgs e)
        {
            ProcesoContingencia pc = new ProcesoContingencia();
            pc.procesoContingencia();
        }

        private void button20_Click(object sender, EventArgs e)
        {
            LocalDataBase l = new LocalDataBase();
            l.addCollumnToReenvio();
        }

        private void button21_Click(object sender, EventArgs e)
        {
            Empresa empresa = new Empresa();
            empresa.creaTabla();
        }

        private void button13_Click_1(object sender, EventArgs e)
        {
            proc.StopProcessIat();
            procContig.StopProcessConting();
            this.label4.Text = "IatProcess Detenido";
            this.timer1.Stop();

        }
    }
}
