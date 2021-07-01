using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace WebServices_DataSources
{
    public class DataSource
    {
        // Get the current users MyDocuments folder  
        static string directory = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        // Our File Name. You can change it in your project.  
        static string fileName = "/myServiceFile.txt";

        // static variable  
        // Will contain the content, for accessing and saving the data  
        static List<string> currentData = File.ReadAllLines(directory + fileName).ToList();

        // This method saves the Data sent  
        public void Save(string dataLabel)
        {
            currentData.Add(dataLabel);
            Directory.CreateDirectory(directory);
            File.Create(directory + fileName).Close();

            File.WriteAllLines(directory + fileName, currentData);
        }

        // Gets the current list.  
        public List<string> GetDataList()
        {
            return currentData;
        }
    }
}
