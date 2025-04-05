using System;
using System.Xml.Schema;
using System.Xml;
using Newtonsoft.Json;
using System.IO;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/**
 * This template file is created for ASU CSE445 Distributed SW Dev Assignment 4.
 * Please do not modify or delete any existing class/variable/method names. However, you can add more variables and functions.
 * Uploading this file directly will not pass the autograder's compilation check, resulting in a grade of 0.
 * **/


namespace ConsoleApp1
{


    public class Program
    {
        public static string xmlURL = "https://gfiedler2026.github.io/cse445_assignment4/Hotels.xml";
        public static string xmlErrorURL = "https://gfiedler2026.github.io/cse445_assignment4/HotelsErrors.xml";
        public static string xsdURL = "https://gfiedler2026.github.io/cse445_assignment4/Hotels.xsd";

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
                string xsdStuff = new WebClient().DownloadString(xsdUrl); //create new clients that we will use to complete verification

                XmlSchemaSet schemaSet = new XmlSchemaSet();
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
                };

                using (StringReader xmlReader = new StringReader(xmlStuff))
                using (XmlReader reader = XmlReader.Create(xmlReader, settings))
                {
                    while (reader.Read()) { } //read through 
                }
            }
            catch(Exception ex)
            {
                errorMessage = ex.Message; //give error message if error messgae
            }
            return errorMessage; //return No Error
        }

        public static string Xml2Json(string xmlUrl)
        {
            try
            {
                string xmlContent = new WebClient().DownloadString(xmlUrl); //create new client to transform the list of hotels to formatted JSON
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(xmlContent); //load xml doc
                string jsonText = JsonConvert.SerializeXmlNode(doc, Newtonsoft.Json.Formatting.Indented); //serialize and format
                return jsonText; //return formatted JSON text flie
            }
            catch(Exception ex)
            {
                return $"Error: {ex.Message}"; //return error message if need 
            }

            // The returned jsonText needs to be de-serializable by Newtonsoft.Json package. (JsonConvert.DeserializeXmlNode(jsonText))
        }
    }

}
