using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;
using System.Windows.Forms;

namespace IatDteBridge
{
    class Folio
    {
        String strConn = @"Data Source=C:/IatFiles/iatDB.sqlite;Pooling=true;FailIfMissing=false;Version=3";

        public String rut {get;set;}
        public String rsnsocial{get;set;}
        public Int32 tipoDte{get;set;}
        public Int32 folioSgte { get; set; }
        public Int32 folioIni{get;set;}
        public Int32 folioFin{get;set;}
        public String fecha{get;set;}
        public String rango { get; set; }

        public void creaTabla()
        {

            try
            {
                if (!System.IO.File.Exists("C:/IatFiles/iatDB.sqlite"))
                {
                    MessageBox.Show("La Base de Datos no Existe");
                }
                else
                {
                    SQLiteConnection myConn = new SQLiteConnection(strConn);
                    myConn.Open();
                    String sql1 = "CREATE TABLE IF NOT EXISTS folio (rut VARCHAR(10), rsnsocial VARCHAR(255),tipoDte INTEGER, ,folioSgte INTEGER, folioIni INTEGER,folioFin INTEGER, fecha VARCHAR(12), rango VARCHAR(255)";
                    SQLiteCommand cmd = new SQLiteCommand(sql1, myConn);
                    cmd.ExecuteNonQuery();
                    myConn.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("ERROR: {0}", e.ToString());
            }
        }

        public void incrementaSgte(String tDte, String rutEmisor)
        {
            try
            {
                SQLiteConnection myConn = new SQLiteConnection(strConn);
                myConn.Open();

                string sql = "UPDATE folio set folioSgte = folioSgte+1" +
                                "WHERE tipoDte = "+tDte+" and rut ='"+ rutEmisor +"';";

                SQLiteCommand command = new SQLiteCommand(sql, myConn);
                command.ExecuteNonQuery();

                myConn.Close();
            }
            catch (Exception empUpdate)
            {
                Console.WriteLine("ERROR: {0}" + empUpdate.ToString());
                MessageBox.Show("ERROR: {0}" + empUpdate.ToString());
            }


        }

        public Folio getFolio(int tDte, String rutEmisor)
        {
            Folio f = new Folio();
            try
            {
                SQLiteConnection myConn = new SQLiteConnection(strConn);
                myConn.Open();

                string sql = "select * from folio where tipoDte = "+tDte+" and rut ='"+ rutEmisor +"' order by fch;";
                SQLiteCommand command = new SQLiteCommand(sql, myConn);
                SQLiteDataReader reader = command.ExecuteReader();
                while (reader.Read())
                    f.rut = reader["rut"].ToString();
                    f.rsnsocial = reader["rsnsocial"].ToString();
                    f.tipoDte = Int32.Parse(reader["tipoDte"].ToString());
                    f.folioIni = Int32.Parse(reader["folioIni"].ToString());
                    f.folioFin = Int32.Parse(reader["folioFin"].ToString());
                    f.folioSgte = Int32.Parse(reader["folioSgte"].ToString());
                    f.fecha = reader["fecha"].ToString();
                    f.rango = reader["rango"].ToString();
                myConn.Close();
                return f;
            }
            catch (Exception e)
            {
                Console.WriteLine("ERROR: {0}", e.ToString());
                return f;
            }
        }
    }
}
