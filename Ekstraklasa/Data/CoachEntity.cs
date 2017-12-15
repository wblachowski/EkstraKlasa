using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ekstraklasa
{
    public class CoachEntity : PersonEntity
    {
        public DateTime? DateOfHiring { get; set; }
        
        public CoachEntity() : base() { }

        public CoachEntity(string Pesel, string Firstname, string Lastname, DateTime? DateOfBirth, string Nationality, DateTime? DateOfHiring)
            : base(Pesel, Firstname, Lastname, DateOfBirth, Nationality)
        {
            this.DateOfHiring = DateOfHiring;
        }
    }
}
