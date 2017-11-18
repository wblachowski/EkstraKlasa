using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ekstraklasa
{
    public class TableEntity
    {
        public int Nr { get; set; }
        public string Path { get; set; }
        public string Name { get; set; }
        public int Matches { get; set; }
        public int Wins { get; set; }
        public int Ties { get; set; }
        public int Loses { get; set; }
        public string Goals { get; set; }
        public int Points { get; set; }

        public TableEntity(int Nr, string Name, int Matches, int Wins, int Ties, int Loses, string Goals, int Points,string Path= "C:\\Users\\wblachowski\\Documents\\Visual Studio 2017\\Projects\\Ekstraklasa\\Logos\\Legia_Warszawa.png")
        {
            this.Nr = Nr;
            this.Path = Path;
            this.Name = Name;
            this.Matches = Matches;
            this.Wins = Wins;
            this.Ties = Ties;
            this.Loses = Loses;
            this.Goals = Goals;
            this.Points = Points;
        }
    }
}
