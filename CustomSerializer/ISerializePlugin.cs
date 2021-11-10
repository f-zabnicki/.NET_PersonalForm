using System;
using PersonFormConsole;

namespace CustomSerializer
{
    public interface ISerializePlugin
    {
        //Nazwa serializatora/pluginu -> wyswietlany dla uzytkownika na ekranie
        public string Name { get; }
        //Opis dzialania pluginu -> wyswietlany dla uzytkownika na ekranie
        public string Description { get; }
        public void Serialize(Person person);
        public Person Deserialize();
    }
}
