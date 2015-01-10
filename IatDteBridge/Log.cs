using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;
using System.IO;

namespace IatDteBridge
{
    class Log
    {
        String strConn = @"Data Source=C:/IatFiles/iatDB.sqlite;Pooling=true;FailIfMissing=false;Version=3";

        public bool creaDB()
        {
            try
            {
                if (!System.IO.File.Exists("C:/IatFiles/iatDB.sqlite"))
                {
                    SQLiteConnection.CreateFile("C:/IatFiles/iatDB.sqlite");
                    SQLiteConnection myConn = new SQLiteConnection(strConn);
                    myConn.Open();

                    String sql = "CREATE TABLE log (fch VARCHAR(20), suceso VARCHAR(255), estado VARCHAR(20)) ";
                    String sql2 = "CREATE TABLE reenvio (fch VARCHAR(20), jsonname VARCHAR(255), envunit VARCHAR(255), pdft VARCHAR(255), pdfc VARCHAR(255), estado VARCHAR(20)) ";

                    SQLiteCommand cmd = new SQLiteCommand(sql, myConn);
                    cmd.ExecuteNonQuery();

                    SQLiteCommand cmd2 = new SQLiteCommand(sql2, myConn);
                    cmd2.ExecuteNonQuery();

                    myConn.Close();
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("ERROR: {0}", e.ToString());
                return false;
            }
            return true;
        }

        public void addLog( String suceso, String estado)
        {
            try
            {
                DateTime thisDay = DateTime.Now;
                String fecha = String.Format("{0:yyyyMMddTHHmmss}", thisDay);

                SQLiteConnection myConn = new SQLiteConnection(strConn);
                myConn.Open();

                string sql = "insert into log (fch, suceso, estado) values ('" + fecha + "' , '" + suceso + "', '" + estado + "')";
                SQLiteCommand command = new SQLiteCommand(sql, myConn);
                command.ExecuteNonQuery();

                myConn.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("ERROR: {0}", e.ToString());
            }
        }

      

        public String verLog()
        {

            String logRes = String.Empty;

            try
            {

                SQLiteConnection myConn = new SQLiteConnection(strConn);
                myConn.Open();

                string sql = "select * from log order by fch";
                SQLiteCommand command = new SQLiteCommand(sql, myConn);
                SQLiteDataReader reader = command.ExecuteReader();
                while (reader.Read())
                    Console.WriteLine("Estado :" + reader["estado"] + "\tFecha: " + reader["fch"] + "\tSuceso: " + reader["suceso"]);


                myConn.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("ERROR: {0}", e.ToString());
                return logRes;
            }

            return logRes;
        }

      

    }
}
