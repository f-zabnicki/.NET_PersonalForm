using CustomSerializer;
using PersonFormConsole;
using System;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace JSONSerializer
{
    public class Serializer : ISerializePlugin
    {
        public string Name => "JSON Serializer";

        public string Description => "This plugin will let you to serializer and deserialize file using JSON format";

        public Person Deserialize()
        {
            var directory = new DirectoryInfo(@"C:\Users\filip.zabnicki\source\repos\.NET_PersonalForm\Forms");
            var myFile = directory.GetFiles()
                         .OrderByDescending(f => f.LastWriteTime)
                         .First();
            string jsonString = File.ReadAllText(myFile.FullName);
            return JsonSerializer.Deserialize<Person>(jsonString);
        }

        public void Serialize(Person person)
        {
            string fileName = @$"C:\Users\filip.zabnicki\source\repos\.NET_PersonalForm\Forms\form{DateTime.Now.ToString("yyyyMMddTHH_mm_ssZ")}.json";
            string jsonString = JsonSerializer.Serialize(person);
            File.WriteAllText(fileName, jsonString);
        }
    }
}
