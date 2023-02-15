using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Survey
{
    public class Person
    {
        public string Id { get; set; } = Guid.NewGuid().ToString().Substring(0, 8);
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Number { get; set; }
        public string Birthdate { get; set; }
        public string Filename { get; set; }

        public override string ToString()
        {
            return $"{Name}-{Surname}-{Email}-{Number}-{Birthdate}";
        }
    }
}
