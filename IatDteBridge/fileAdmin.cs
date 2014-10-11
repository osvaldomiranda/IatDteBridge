using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace IatDteBridge
{
    class fileAdmin
    {
        public String nextFile(String path, String fileType)
        {

            //fileType ejemplo:  "*.txt"
            //path     ejemplo:   @"c:\files\"  

            String fileName = String.Empty;

            string currentDirName = System.IO.Directory.GetCurrentDirectory();
       
            if (!System.IO.Directory.Exists(path))
            {
                System.IO.Directory.CreateDirectory(path);
            }

            System.IO.Directory.SetCurrentDirectory(path);

            currentDirName = System.IO.Directory.GetCurrentDirectory();

            string[] files = System.IO.Directory.GetFiles(currentDirName, fileType);

            string s = files.First();

            System.IO.FileInfo fi = null;
            try
            {
                fi = new System.IO.FileInfo(s);
            }
            catch (System.IO.FileNotFoundException e)
            {
                Console.WriteLine(e.Message);
            }

            fileName = fi.Name;
            Console.WriteLine("NextFile   {0} : {1}", fi.Name, fi.Directory);

            return fileName;
        }


        public void mvFile(String fileName, String path, String path2)
        {
            Console.WriteLine("Mueve el archivo");


            try
            {
                if (!Directory.Exists(path2))
                {
                    DirectoryInfo di = Directory.CreateDirectory(path2);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The process failed: {0}", e.ToString());
            }


            try
            {

                if (System.IO.File.Exists(path2 + fileName)) System.IO.File.Delete(path2 + fileName);

                System.IO.File.Move(path + fileName, path2 + fileName);

            }
            catch (Exception e)
            {
                Console.WriteLine("The process failed: {0}", e.ToString());
            }


        }

    }
}
