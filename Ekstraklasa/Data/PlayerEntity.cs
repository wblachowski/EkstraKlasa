using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ekstraklasa
{
    public class PlayerEntity : PersonEntity
    {
        public int? Weight { get; set; }
        public int? Height { get; set; }
        public int? Nr { get; set; }
        public string Position { get; set; }
        public string Display { get; set; }

        public PlayerEntity() : base() { }

        public PlayerEntity(string Pesel, string Firstname, string Lastname, DateTime DateOfBirth, string Nationality, int Weight, int Height, int Nr, string Position)
            : base(Pesel, Firstname, Lastname, DateOfBirth, Nationality)
        {
            this.Weight = Weight;
            this.Height = Height;
            this.Nr = Nr;
            this.Position = Position;
            this.Display = Firstname.Substring(0, 1) + '.' + Lastname;
        }
    }
}
