using System;
using System.IO;
using System.Collections;

namespace IatDteBridge
{
    class File
    {
        public void OpenFile()
        {
            StreamReader objReader = new StreamReader("c://file/test.txt");
            string sLine = "";
            ArrayList arrText = new ArrayList();

            while (sLine != null)
            {
                sLine = objReader.ReadLine();
                if (sLine != null)
                    arrText.Add(sLine);
            }
            objReader.Close();

            foreach (string sOutput in arrText)
                Console.WriteLine(sOutput);
            Console.ReadLine();
        }
    }


}
