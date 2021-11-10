using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonFormConsole
{
    public class Person
    {
        [PersonDescription("What is Your Name?")]
        public string Name{ get; set; }
        [PersonDescription("What is Your Surname?")]
        public string Surname { get; set; }
        [PersonDescription("What is Your date of birth? (yyyy-mm-dd)")]
        public DateTime DateOfBirth { get; set; }
        [PersonDescription("What is Your sex? (Male: 1, Female: 2)")]
        public Sex Sex { get; set; }
        [PersonDescription("Are You married? (True, False)")]
        public bool IsMarried { get; set; }
        private Guid UniqueId { get;}
        public Person()
        {
            UniqueId = Guid.NewGuid();
        }
    }
}
