using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ekstraklasa
{
    public class TableEntity
    {
        public TableEntity(string Nr,string Name,string Matches,string Wins,string Ties,string Loses, string Goals,string Points)
        {
            this.Nr = Nr;
            this.Name = Name;
            this.Matches = Matches;
            this.Wins = Wins;
            this.Ties = Ties;
            this.Loses = Loses;
            this.Goals = Goals;
            this.Points = Points;
        }

        public string Nr;
        public string Name;
        public string Matches;
        public string Wins;
        public string Ties;
        public string Loses;
        public string Goals;
        public string Points;
    }
}
