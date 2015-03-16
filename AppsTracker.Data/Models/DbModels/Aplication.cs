﻿#region Licence
/*
  *  Author: Marko Devcic, madevcic@gmail.com
  *  Copyright: Marko Devcic, 2014
  *  Licence: http://creativecommons.org/licenses/by-nc-nd/4.0/
 */
#endregion

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AppsTracker.Data.Utils;

namespace AppsTracker.Data.Models
{
    public class Aplication
    {
        [NotMapped]
        public TimeSpan Duration
        {
            get
            {
                return GetAppDuration();
            }
        }

        private TimeSpan GetAppDuration()
        {
            long ticks = 0;
            foreach (var window in this.Windows)
            {
                foreach (var log in window.Logs)
                {
                    ticks += log.Duration;
                }
            }

            return new TimeSpan(ticks);
        }

        public Aplication() { }

        public Aplication(string name, string fileName, string version, string description, string company, string realName)
        {
            this.BlockedApps = new HashSet<BlockedApp>();
            this.Windows = new HashSet<Window>();
            this.Categories = new HashSet<AppCategory>();
            this.Warnings = new HashSet<AppWarning>();

            this.Name = !string.IsNullOrEmpty(name) ? name.Truncate(250) : !string.IsNullOrEmpty(realName) ? realName.Truncate(250) : fileName.Truncate(250);
            this.FileName = fileName.Truncate(360);
            this.Version = version.Truncate(50);
            this.Description = description.Truncate(150);
            this.Company = company.Truncate(150);
            this.WinName = realName.Truncate(100);
        }


        public Aplication(IAppInfo appInfo)
            : this(appInfo.Name, appInfo.FileName, appInfo.Version, appInfo.Description, appInfo.Company, appInfo.FullName)
        {

        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ApplicationID { get; set; }

        [Required]
        [StringLength(250)]
        public string Name { get; set; }

        [StringLength(360)]
        public string FileName { get; set; }

        [StringLength(50)]
        public string Version { get; set; }

        [StringLength(150)]
        public string Description { get; set; }

        [StringLength(150)]
        public string Company { get; set; }

        [Required]
        public int UserID { get; set; }

        [StringLength(100)]
        public string WinName { get; set; }

        [ForeignKey("UserID")]
        public virtual Uzer User { get; set; }
        public virtual ICollection<BlockedApp> BlockedApps { get; set; }
        public virtual ICollection<Window> Windows { get; set; }
        public virtual ICollection<AppCategory> Categories { get; set; }
        public virtual ICollection<AppWarning> Warnings { get; set; }

    }
}