using System;
using System.Collections.Generic;
using System.Configuration;
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

        public TableEntity(int Nr, string Name, int Matches, int Wins, int Ties, int Loses, string Goals, int Points,string Path)
        {
            this.Nr = Nr;
            this.Path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Ekstraklasa\\" + Path;
            if (String.IsNullOrEmpty(Path.Trim()))
            {
                this.Path = ConfigurationManager.AppSettings["default_logo"];
            }
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
