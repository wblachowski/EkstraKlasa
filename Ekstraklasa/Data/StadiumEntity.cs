using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ekstraklasa
{
    public class StadiumEntity
    {
        public string Name;
        public string Address;
        public string City;
        public int Capacity;

        public StadiumEntity(string Name, string Address, string City, int Capacity)
        {
            this.Name = Name;
            this.Address = Address;
            this.City = City;
            this.Capacity = Capacity;
        }
    }
}
