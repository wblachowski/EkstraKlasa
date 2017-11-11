using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ekstraklasa
{
    public class MatchEntity
    {
        public MatchEntity(int ID,DateTime Date, string Host, string Guest, int ScoreHost, int ScoreGuest, string Stadium, string City, string Address)
        {
            this.ID = ID;
            this.Date = Date;
            this.Host = Host;
            this.Guest = Guest;
            this.ScoreHost = ScoreHost;
            this.ScoreGuest = ScoreGuest;
            this.Stadium = Stadium;
            this.City = City;
            this.Address = Address;
        }

        public int ID;
        public DateTime Date;
        public string Host;
        public string Guest;
        public int ScoreHost;
        public int ScoreGuest;
        public string Stadium;
        public string City;
        public string Address;
    }
}
