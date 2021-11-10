using CustomSerializer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PersonFormConsole
{
    static class ConsoleUserInterface
    {
        public static void LoadOrNewForm()
        {
            Console.WriteLine("Create new form(y)? or load previous one(n)?");
            var input = Console.ReadKey();
            switch (input.KeyChar)
            {
                case 'y':
                    Console.Clear();
                    CreateNewForm();
                    break;
                case 'n':
                    Console.Clear();
                    ShowPreviousForm();
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine("Wrong key pressed. Please press(y/n).");
                    break;
            }
        }

        private static void ShowPreviousForm()
        {
            Person user = LoadExistingForm();
            if (user == null) { throw new NullReferenceException("Soemthng went wrong with loading previous form..."); };
            Console.WriteLine("Previous form:");
            Console.WriteLine($"Name:           {user.Name}");
            Console.WriteLine($"Surname:        {user.Surname}");
            Console.WriteLine($"Sex:            {user.Sex}");
            Console.WriteLine($"Married:        {user.IsMarried}");
            Console.WriteLine($"Date of birth:  {user.DateOfBirth}");
        }

        private static void CreateNewForm()
        {
            Person user = (Person)Activator.CreateInstance(typeof(Person));
            var properties = typeof(Person).GetProperties();
            foreach (var prop in properties)
            {
                var customAttributes = prop.GetCustomAttributes(typeof(PersonDescriptionAttribute), false);
                foreach (PersonDescriptionAttribute att in customAttributes)
                {
                    Console.WriteLine(att.Description);
                    var value = Console.ReadLine();
                    if (prop.PropertyType.Name == "DateTime")
                    {
                        var arr = value.Split('-');
                        int[] dates = arr.Select(int.Parse).ToArray();
                        prop.SetValue(user, new DateTime(dates[0], dates[1], dates[2]));
                        break;
                    }
                    if (prop.PropertyType.Name == "Sex")
                    {
                        var indicator = Int32.Parse(value);
                        prop.SetValue(user, (Sex)indicator);
                        break;
                    }
                    if (prop.PropertyType.Name == "Boolean")
                    {
                        bool married = bool.Parse(value);
                        prop.SetValue(user, married);
                        break;
                    }
                    prop.SetValue(user, value);
                }
            }
            WorkWithPlugins(user);
        }

        private static Person LoadExistingForm()
        {
            Person output = null;
            var plugins = GetPlugins();
            foreach (var plugin in plugins)
            {
                try
                {
                    output = plugin.Deserialize();
                    if (output != null) 
                    {
                        return output;
                        break;
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine($"{plugin.Name} coudn't deserialize.");
                }
            }
            throw new Exception("Can't deserialize previous form.");
            Console.WriteLine("Something went wrong with loading previous form. Try creating new one.");
            LoadOrNewForm();
        }

        private static List<ISerializePlugin> GetPlugins()
        {
            List<ISerializePlugin> plugins = new List<ISerializePlugin>();
            var collection = Directory.GetFiles(@"C:\Users\filip.zabnicki\source\repos\.NET_PersonalForm\Plugins", "*.dll");
            foreach (var item in collection)
            {
                var assemblyInfo = Assembly.LoadFrom(item);
                var type = assemblyInfo.GetTypes().FirstOrDefault(o => typeof(ISerializePlugin).IsAssignableFrom(o));
                ISerializePlugin plugin = (ISerializePlugin)Activator.CreateInstance(type);
                plugins.Add(plugin);
            }
            return plugins;
        }

        private static void WorkWithPlugins(Person user)
        {
            var plugins = GetPlugins();
            int serializerIndex = AskForSerializer();
            plugins[serializerIndex - 1].Serialize(user); // TU COŚ NIE DZIAŁA I NIE WIEM CZEMU!!!
        }
        private static int AskForSerializer()
        {
            Console.WriteLine("Would you like to serialize to JSON(1) or XML(2)");
            return int.Parse(Console.ReadLine());
        }
    }
}
