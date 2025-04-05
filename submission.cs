using System;
using System.Xml.Schema;
using System.Xml;
using Newtonsoft.Json;
using System.IO;



/**
 * This template file is created for ASU CSE445 Distributed SW Dev Assignment 4.
 * Please do not modify or delete any existing class/variable/method names. However, you can add more variables and functions.
 * Uploading this file directly will not pass the autograder's compilation check, resulting in a grade of 0.
 * **/


namespace ConsoleApp1
{


    public class Program
    {
        public static string xmlURL = "https://github.com/gfiedler2026/cse445_assignment4/blob/main/Hotels.xml";
        public static string xmlErrorURL = "https://github.com/gfiedler2026/cse445_assignment4/blob/main/HotelsErrors.xml";
        public static string xsdURL = "https://github.com/gfiedler2026/cse445_assignment4/blob/main/Hotels.xsd";

        public static void Main(string[] args)
        {
            string result = Verification(xmlURL, xsdURL);
            Console.WriteLine(result);


            result = Verification(xmlErrorURL, xsdURL);
            Console.WriteLine(result);


            result = Xml2Json(xmlURL);
            Console.WriteLine(result);
        }

        // Q2.1
        public static string Verification(string xmlUrl, string xsdUrl)
        {
            string errorMessage = "No Error";

            try
            {
                string xmlStuff = new WebClient().DownloadString(xmlUrl);
                string xsdStuff = new WebClient().DownloadString(xsdUrl);

                XmlSchemaSet schema = XmlSchema.Read(xsdReader, null);
                using (StringReader xsdReader = new StringReader(xsdStuff))
                {
                    XmlSchema schema = XmlSchema.Read(xsdReader, null);
                    schemaSet.Add(schema);
                }

                XmlReaderSettings settings = new XmlReaderSettings();
                settings.Schemas = schemaSet;
                settings.ValidationType = ValidationType.Schema;
                settings.ValidationEventHandler += (sender, args) =>
                {
                    errorMessage = args.Message; 
                }
                using (StringReader xmlReader = new StringReader(xmlStuff))
                using (xmlReader reader = xmlReader.Create(xmlReader, settings))
                {
                    while (reader.Reader()) { }
                }
            }
            catch(Exception ex)
            {
                errorMessage = ex.Message;
            }
            return errorMessage;
        }

        public static string Xml2Json(string xmlUrl)
        {
            try
            {
                string xmlContent = new WebClient().DownloadString(xmlUrl);
                XmlDocument doc  new XmlDocument();
                doc.LoadXml(xmlContent);
                string json = JsonConvert.SeralizeXmlNode(doc, Newtonsoft.Json.Formatting.Indented);
                return json;
            }
            catch(Exception ex)
            {
                return $"Error: {ex.Message}";
            }

            // The returned jsonText needs to be de-serializable by Newtonsoft.Json package. (JsonConvert.DeserializeXmlNode(jsonText))
        }
    }

}
