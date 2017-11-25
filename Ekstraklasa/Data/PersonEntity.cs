using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ekstraklasa
{
    abstract public class PersonEntity
    {
        public string Pesel;
        public string Firstname;
        public string Lastname;
        public DateTime DateOfBirth;
        public string Nationality;

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
