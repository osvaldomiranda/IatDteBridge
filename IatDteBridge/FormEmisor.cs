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
                using (StreamReader sr = new StreamReader("c://file/empresa" + ".txt"))
                {
                    int i = 1;
                    while ((lineEmisor = sr.ReadLine()) != null)
                    {
                        Console.WriteLine(lineEmisor);
                        switch (i)
                        {
                            case 1: textBox_rutempresa.Text = lineEmisor;
                                break;
                            case 2: textBox_razonsocial.Text = lineEmisor;
                                break;
                            case 3: textBox_giro.Text = lineEmisor;
                                break;
                            case 4: textBox_telefono.Text = lineEmisor;
                                break;
                            case 5: textBoxcorreoemisor.Text = lineEmisor;
                                break;
                           /* case 6: doc.DirOrigen = lineEmisor;
                                break;
                            case 7: doc.CmnaOrigen = lineEmisor;
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

            using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"c://file/empresa" + ".txt"))
            {

                         file.WriteLine(textBox_rutempresa.Text);
                         file.WriteLine(textBox_razonsocial.Text);
                         file.WriteLine(textBox_giro.Text);
                         file.WriteLine(textBox_telefono.Text);
                         file.WriteLine(textBoxcorreoemisor.Text);
                         
                  
            }

         }


     }
}

