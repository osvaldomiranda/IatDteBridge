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
            String lineEmisor = String.Empty;
           
            try
            {
                using (StreamReader sr = new StreamReader(@"C:/IatFiles/config/empresa" + ".txt"))
                {
                    int i = 1;
                    while ((lineEmisor = sr.ReadLine()) != null)
                    {
                        Console.WriteLine(lineEmisor);
                        switch (i)
                        {
                            case 1: textBox_RUTEmisor.Text = lineEmisor;
                                break;
                            case 2: textBox_RznSoc.Text = lineEmisor;
                                break;
                            case 3: textBox_GiroEmis.Text = lineEmisor;
                                break;
                            case 4: textBox_Telefono.Text = lineEmisor;
                                break;
                            case 5: textBox_CorreoEmisor.Text = lineEmisor;
                                break;
                            case 6: textBox_Acteco.Text = lineEmisor;
                                break;
                            case 7: textBox_CdgSIISucur.Text = lineEmisor;
                                break;
                            case 8: textBox_DirOrigen.Text = lineEmisor;
                                break;
                            case 9: textBox_CmnaOrigen.Text = lineEmisor;
                                break;
                            /*case 7: doc.CmnaOrigen = lineEmisor;
                                break;
                            case 8: doc.CiudadOrigen = lineEmisor;
                                break;*/

                        }

                        i++;

                    }

                }
            }
            catch (Exception en)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(en.Message);
            }
        }

        private void button_guardar_Click(object sender, EventArgs e)
        {
            // guardar información del FormEmisor

            using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"C:/IatFiles/config/empresa" + ".txt"))
            {

                         file.WriteLine(textBox_RUTEmisor.Text);
                         file.WriteLine(textBox_RznSoc.Text);
                         file.WriteLine(textBox_GiroEmis.Text);
                         file.WriteLine(textBox_Telefono.Text);
                         file.WriteLine(textBox_CorreoEmisor.Text);
                         file.WriteLine(textBox_Acteco.Text);
                         file.WriteLine(textBox_CdgSIISucur.Text);
                         file.WriteLine(textBox_DirOrigen.Text);
                         file.WriteLine(textBox_CmnaOrigen.Text);
         
                  
            }

         }


     }
}

