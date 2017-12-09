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
        public int ID { get; set; }
        public DateTime Date { get; set; }
        public string Host { get; set; }
        public int HostId { get; set; }
        public string HostPath { get; set; }
        public string Guest { get; set; }
        public int GuestId { get; set; }
        public string GuestPath { get; set; }
        public int ScoreHost { get; set; }
        public int ScoreGuest { get; set; }
        public StadiumEntity Stadium { get; set; }

        public MatchEntity(int ID,DateTime Date, string Host, int HostId, string HostPath, string Guest, int GuestId, string GuestPath, int ScoreHost, int ScoreGuest, StadiumEntity Stadium)
        {
            string basePath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Ekstraklasa\\";
            this.ID = ID;
            this.Date = Date;
            this.Host = Host;
            this.HostId = HostId;
            this.HostPath = String.IsNullOrEmpty(HostPath.Trim()) ? ConfigurationManager.AppSettings["default_logo"] : basePath +HostPath;
            this.Guest = Guest;
            this.GuestId = GuestId;
            this.GuestPath = String.IsNullOrEmpty(GuestPath.Trim()) ? ConfigurationManager.AppSettings["default_logo"] : basePath + GuestPath;
            this.ScoreHost = ScoreHost;
            this.ScoreGuest = ScoreGuest;
            this.Stadium = Stadium;
        }
    }
}
