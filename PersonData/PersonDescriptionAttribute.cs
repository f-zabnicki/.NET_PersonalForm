using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonFormConsole
{
    public class PersonDescriptionAttribute : Attribute
    {
        public string Description{ get; set; }
        public PersonDescriptionAttribute(string value)
        {
            Description = value;
        }
    }
}
