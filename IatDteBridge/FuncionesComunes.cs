﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Diagnostics;
using System.Drawing.Printing;




namespace IatDteBridge
{
    public class FuncionesComunes
    {
      
            static bool verbose = false;

            public static RSACryptoServiceProvider crearRsaDesdePEM(string base64)
            {

                ////
                //// Extraiga de la cadena los header y footer
                base64 = base64.Replace("-----BEGIN RSA PRIVATE KEY-----", string.Empty);
                base64 = base64.Replace("-----END RSA PRIVATE KEY-----", string.Empty);

                ////
                //// el resultado que se encuentra en base 64 cambielo a
                //// resultado string
                byte[] arrPK = Convert.FromBase64String(base64);

                ////
                //// obtenga el Rsa object a partir de
                return DecodeRSAPrivateKey(arrPK);

            }

            public static RSACryptoServiceProvider DecodeRSAPrivateKey(byte[] privkey)
            {
                byte[] MODULUS, E, D, P, Q, DP, DQ, IQ;

                // --------- Set up stream to decode the asn.1 encoded RSA private key ------
                MemoryStream mem = new MemoryStream(privkey);
                BinaryReader binr = new BinaryReader(mem);  //wrap Memory Stream with BinaryReader for easy reading
                byte bt = 0;
                ushort twobytes = 0;
                int elems = 0;
                try
                {
                    twobytes = binr.ReadUInt16();
                    if (twobytes == 0x8130) //data read as little endian order (actual data order for Sequence is 30 81)
                        binr.ReadByte();	//advance 1 byte
                    else if (twobytes == 0x8230)
                        binr.ReadInt16();	//advance 2 bytes
                    else
                        return null;

                    twobytes = binr.ReadUInt16();
                    if (twobytes != 0x0102) //version number
                        return null;
                    bt = binr.ReadByte();
                    if (bt != 0x00)
                        return null;


                    //------ all private key components are Integer sequences ----
                    elems = GetIntegerSize(binr);
                    MODULUS = binr.ReadBytes(elems);

                    elems = GetIntegerSize(binr);
                    E = binr.ReadBytes(elems);

                    elems = GetIntegerSize(binr);
                    D = binr.ReadBytes(elems);

                    elems = GetIntegerSize(binr);
                    P = binr.ReadBytes(elems);

                    elems = GetIntegerSize(binr);
                    Q = binr.ReadBytes(elems);

                    elems = GetIntegerSize(binr);
                    DP = binr.ReadBytes(elems);

                    elems = GetIntegerSize(binr);
                    DQ = binr.ReadBytes(elems);

                    elems = GetIntegerSize(binr);
                    IQ = binr.ReadBytes(elems);

                    Console.WriteLine("showing components ..");
                    if (verbose)
                    {
                        showBytes("\nModulus", MODULUS);
                        showBytes("\nExponent", E);
                        showBytes("\nD", D);
                        showBytes("\nP", P);
                        showBytes("\nQ", Q);
                        showBytes("\nDP", DP);
                        showBytes("\nDQ", DQ);
                        showBytes("\nIQ", IQ);
                    }

                    // ------- create RSACryptoServiceProvider instance and initialize with public key -----
                    CspParameters CspParameters = new CspParameters();
                    CspParameters.Flags = CspProviderFlags.UseMachineKeyStore;
                    RSACryptoServiceProvider RSA = new RSACryptoServiceProvider(1024, CspParameters);
                    RSAParameters RSAparams = new RSAParameters();
                    RSAparams.Modulus = MODULUS;
                    RSAparams.Exponent = E;
                    RSAparams.D = D;
                    RSAparams.P = P;
                    RSAparams.Q = Q;
                    RSAparams.DP = DP;
                    RSAparams.DQ = DQ;
                    RSAparams.InverseQ = IQ;
                    RSA.ImportParameters(RSAparams);
                    return RSA;
                }
                catch (Exception e)
                {
                    return null;
                }
                finally
                {
                    binr.Close();
                }
            }

            private static int GetIntegerSize(BinaryReader binr)
            {
                byte bt = 0;
                byte lowbyte = 0x00;
                byte highbyte = 0x00;
                int count = 0;
                bt = binr.ReadByte();
                if (bt != 0x02)   	 //expect integer
                    return 0;
                bt = binr.ReadByte();

                if (bt == 0x81)
                    count = binr.ReadByte();    // data size in next byte
                else
                    if (bt == 0x82)
                    {
                        highbyte = binr.ReadByte();    // data size in next 2 bytes
                        lowbyte = binr.ReadByte();
                        byte[] modint = { lowbyte, highbyte, 0x00, 0x00 };
                        count = BitConverter.ToInt32(modint, 0);
                    }
                    else
                    {
                        count = bt;   	 // we already have the data size
                    }

                while (binr.ReadByte() == 0x00)
                {    //remove high order zeros in data
                    count -= 1;
                }
                binr.BaseStream.Seek(-1, SeekOrigin.Current);   	 //last ReadByte wasn't a removed zero, so back up a byte
                return count;
            }

            private static void showBytes(String info, byte[] data)
            {
                Console.WriteLine("{0} [{1} bytes]", info, data.Length);
                for (int i = 1; i <= data.Length; i++)
                {
                    Console.Write("{0:X2} ", data[i - 1]);
                    if (i % 16 == 0)
                        Console.WriteLine();
                }
                Console.WriteLine("\n\n");
            }



            public static X509Certificate2 obtenerCertificado(string CN)
            {

                X509Certificate2 certificado = null;

                if (string.IsNullOrEmpty(CN) || CN.Length == 0)
                    return certificado;

                try
                {

                    X509Store store = new X509Store(StoreName.My, StoreLocation.CurrentUser);
                    store.Open(OpenFlags.ReadOnly);
                    X509Certificate2Collection Certificados1 = (X509Certificate2Collection)store.Certificates;
                    X509Certificate2Collection Certificados2 = Certificados1.Find(X509FindType.FindByTimeValid, DateTime.Now, false);
                    X509Certificate2Collection Certificados3 = Certificados2.Find(X509FindType.FindBySubjectName, CN, false);

                    ////
                    //// Si hay certificado disponible envíe el primero
                    if (Certificados3 != null && Certificados3.Count != 0)
                        certificado = Certificados3[0];

                    store.Close();


                }
                catch (Exception)
                {
                    certificado = null;
                }

                return certificado;

            }

        // Funcion Para imprimir sin adobereader
         //PrintParamter is a custom data structure to capture file related info
        public void PrintDocument(string printerName, String filename)
        {
           // if (!File.Exists(fs.FullyQualifiedName)) return;

           // var filename = fs.FullyQualifiedName ?? string.Empty;
           // printerName = printerName ?? GetDefaultPrinter(); //get your printer here

            string processArgs = " -dPrinted -dBATCH -dNOPAUSE -dNOSAFER -q -dNumCopies=1 -sDEVICE=pdfwrite -sOutputFile=%printer%" + GetDefaultPrinter() + "\" \"" + filename + "\" ";
                //string.Format("-ghostscript -copies=1 -all -printer \"{0}\" \"{1}\"", GetDefaultPrinter(), filename );
         
                var gsProcessInfo = new ProcessStartInfo
                                        {
                                          //  WindowStyle = ProcessWindowStyle.Hidden,
                                            FileName = @"C:\iatFiles\gswin32c.exe",
                                            Arguments = processArgs
                                        };
                using (var gsProcess = Process.Start(gsProcessInfo))
                {

                //    gsProcess.WaitForExit();

                }

        }


        public string GetDefaultPrinter()
        {
            PrinterSettings settings = new PrinterSettings();
            foreach (string printer in PrinterSettings.InstalledPrinters)
            {
                settings.PrinterName = printer;
                if (settings.IsDefaultPrinter)
                    return printer;
            }
            return string.Empty;
        }





        public void printPdf(String fileName,String impresora)
        {


            ProcessStartInfo copiaOriginal = new ProcessStartInfo();
            copiaOriginal.Arguments = "\"" + impresora + "\"";
            copiaOriginal.Verb = "printTo";
            copiaOriginal.FileName = fileName;
            copiaOriginal.CreateNoWindow = true;
            copiaOriginal.WindowStyle = ProcessWindowStyle.Hidden;

            Process p = new Process();
            p.StartInfo = copiaOriginal;
            p.Start();

            p.WaitForInputIdle();

            System.Threading.Thread.Sleep(10000);
            if (false == p.CloseMainWindow())
            {
                p.Kill();
            }

        }
    }

}
