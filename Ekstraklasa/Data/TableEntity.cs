using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ekstraklasa
{
    public class TableEntity
    {
        public TableEntity(int Nr, string Name, int Matches, int Wins, int Ties, int Loses, string Goals, int Points)
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

        public int Nr { get; set; }
        public string Name { get; set; }
        public int Matches { get; set; }
        public int Wins { get; set; }
        public int Ties { get; set; }
        public int Loses { get; set; }
        public string Goals { get; set; }
        public int Points { get; set; }
    }
}
