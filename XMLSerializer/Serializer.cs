using CustomSerializer;
using PersonFormConsole;
using System;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace XMLSerializer
{
    public class Serializer : ISerializePlugin
    {
        public string Name => "XML Serializer";

        public string Description => "This plugin will let you to serializer and deserialize file using XML format";

        public Person Deserialize()
        {
            var directory = new DirectoryInfo(@"C:\Users\filip.zabnicki\source\repos\.NET_PersonalForm\Forms");
            var myFile = directory.GetFiles()
                         .OrderByDescending(f => f.LastWriteTime)
                         .First();
            XmlSerializer serializer = new XmlSerializer(typeof(Person));
            using (Stream reader = new FileStream(myFile.FullName, FileMode.Open))
            {
                return (Person)serializer.Deserialize(reader);
            }
        }

        public void Serialize(Person person)
        {
            XmlSerializer xs = new XmlSerializer(typeof(Person));
            TextWriter txtWriter = new StreamWriter(@$"C:\Users\filip.zabnicki\source\repos\.NET_PersonalForm\Forms\form{DateTime.Now.ToString("yyyyMMddTHH_mm_ssZ")}.xml");
            xs.Serialize(txtWriter, person);
            txtWriter.Close();
        }
    }
}
