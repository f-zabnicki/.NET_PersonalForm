using CustomSerializer;
using Newtonsoft.Json;
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
            using (StreamReader r = new StreamReader(myFile.FullName))
            {
                string json = r.ReadToEnd();
                return JsonConvert.DeserializeObject<Person>(json);
            }
        }

        public void Serialize(Person person)
        {
            File.WriteAllText(@$"C:\Users\filip.zabnicki\source\repos\.NET_PersonalForm\Forms\form{DateTime.Today}.json", JsonConvert.SerializeObject(person));
        }
    }
}
