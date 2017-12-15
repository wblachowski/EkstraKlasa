using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ekstraklasa
{
    abstract public class PersonEntity
    {
        public string Pesel { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Nationality { get; set; }

        public PersonEntity() { }

        public PersonEntity(string Pesel, string Firstname, string Lastname, DateTime DateOfBirth, string Nationality)
        {
            this.Pesel = Pesel;
            this.Firstname = Firstname;
            this.Lastname = Lastname;
            this.DateOfBirth = DateOfBirth;
            this.Nationality = Nationality;
        }
    }
}
