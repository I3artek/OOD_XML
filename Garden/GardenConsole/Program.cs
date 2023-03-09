using GardenLibrary;
using System;
using System.Xml;
using System.Xml.Schema;

namespace ConsoleApp
{
    public static class Program
    {
        public static void Main()
        {
            var xml_file = Environment.GetCommandLineArgs()[1];
            try
            {
                GardenReader.ValidateFile(xml_file);
            }
            catch
            {
                return;
            }
            var myLib = GardenReader.ReadFromFile(xml_file);
            foreach(var b in myLib.book)
            {
                foreach(var a in b.authors)
                {
                    Console.WriteLine(a.surname);
                }
            }
        }
    }
}