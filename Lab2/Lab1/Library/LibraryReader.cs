using System;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Library
{
    public static class LibraryReader
    {
        public static libraryType ReadLibrary(string PathToFile)
        {
            var xs = new XmlSerializer(typeof(libraryType));
            libraryType ret;
            using (var reader = new FileStream(PathToFile, FileMode.Open))
            {
                ret = (libraryType)xs.Deserialize(reader);
            }
           
            return ret;
        }

        public static void ValidateFile(string PathToFile)
        {
            XmlReaderSettings settings = new XmlReaderSettings();
            // Validator settings
            settings.ValidationType = ValidationType.Schema;
            settings.ValidationFlags |= XmlSchemaValidationFlags.ProcessInlineSchema;
            settings.ValidationFlags |= XmlSchemaValidationFlags.ReportValidationWarnings;

            // Here we add xsd files to namespaces we want to validate
            // (It's like XML -> Schemas setting in Visual Studio)
            settings.Schemas.Add("library", "../../../../Library/library.xsd");

            // Processing XSI Schema Location attribute
            // (Disabled by default as it is a security risk). 
            settings.ValidationFlags |= XmlSchemaValidationFlags.ProcessSchemaLocation;

            // A function delegate that will be called when 
            // validation error or warning occurs
            settings.ValidationEventHandler += ValidationHandler;

            using (XmlReader reader = XmlReader.Create(PathToFile, settings))
            {
                // Read method reads next element or attribute from the document
                // It will call ValidationEventHandler if some invalid
                // part occurs
                while (reader.Read())
                {
                }
            }
        }

        private static void ValidationHandler(Object sender, ValidationEventArgs args)
        {
            if (args.Severity == XmlSeverityType.Warning)
                Console.WriteLine("Warning: {0}", args.Message);
            else
                Console.WriteLine("Error: {0}", args.Message);


            throw new Exception();
        }

    }
}