using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace IatDteBridge
{
    public partial class FormEmisor : Form
    {
        public FormEmisor()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void FormEmisor_Load(object sender, EventArgs e)
        {
            //abrir archivo empresa.txt si existe recuperar datos en textbox

            FileStream empresa = new FileStream("c://tool/empresa.txt",FileMode.OpenOrCreate,FileAccess.Write);
            string datos = "";

            while ((datos = empresa.ReadLine()) != null){


            }
        }
    }
}
