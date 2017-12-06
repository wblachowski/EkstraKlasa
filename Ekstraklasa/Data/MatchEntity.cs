using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ekstraklasa
{
    public class MatchEntity
    {
        public int ID;
        public DateTime Date;
        public string Host;
        public string HostPath;
        public string Guest;
        public string GuestPath;
        public int ScoreHost;
        public int ScoreGuest;
        public StadiumEntity Stadium { get; set; }

        public MatchEntity(int ID,DateTime Date, string Host, string HostPath, string Guest, string GuestPath, int ScoreHost, int ScoreGuest, StadiumEntity Stadium)
        {
            string basePath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Ekstraklasa\\";
            this.ID = ID;
            this.Date = Date;
            this.Host = Host;
            this.HostPath = String.IsNullOrEmpty(HostPath.Trim()) ? ConfigurationManager.AppSettings["default_logo"] : basePath +HostPath;
            this.Guest = Guest;
            this.GuestPath = String.IsNullOrEmpty(GuestPath.Trim()) ? ConfigurationManager.AppSettings["default_logo"] : basePath + GuestPath;
            this.ScoreHost = ScoreHost;
            this.ScoreGuest = ScoreGuest;
            this.Stadium = Stadium;
        }
    }
}
