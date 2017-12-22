using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ekstraklasa
{
    public class StadiumEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public int? Capacity { get; set; }

        public StadiumEntity() { }

        public StadiumEntity(int Id,string Name, string Address, string City, int? Capacity)
        {
            this.Id = Id;
            this.Name = Name;
            this.Address = Address;
            this.City = City;
            this.Capacity = Capacity;
        }
    }
}
