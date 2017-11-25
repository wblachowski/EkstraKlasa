﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ekstraklasa
{
    public class TeamEntity
    {
        public string Name = "";
        public string LogoPath = "";
        public DateTime FoundedDate;
        public CoachEntity Coach = null;
        public StadiumEntity Stadium = null;

        public TeamEntity() { }

        public TeamEntity(string Name,string LogoPath, DateTime FoundedDate, CoachEntity Coach, StadiumEntity Stadium)
        {
            string basePath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Ekstraklasa\\";
            this.Name = Name;
            this.LogoPath = String.IsNullOrEmpty(LogoPath.Trim()) ? ConfigurationManager.AppSettings["default_logo"] : basePath + LogoPath;
            this.FoundedDate = FoundedDate;
            this.Coach = Coach;
            this.Stadium = Stadium;
        }
    }
}
