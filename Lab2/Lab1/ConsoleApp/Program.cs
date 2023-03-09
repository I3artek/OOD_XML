using Library;
using System;
using System.Xml;
using System.Xml.Schema;

namespace ConsoleApp
{
    public static class Program
    {
        public static void Main()
        {
            const string xml_file = "../../../../Library/example_library.xml";
            try
            {
                LibraryReader.ValidateFile(xml_file);
            }
            catch
            {
                return;
            }
            var myLib = LibraryReader.ReadLibrary(xml_file);
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