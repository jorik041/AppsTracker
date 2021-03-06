﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using AppsTracker.Data.Models;

namespace AppsTracker.Domain.Apps
{
    public sealed class AppModel
    {
        public AppModel(Aplication aplication)
        {
            ApplicationID = aplication.ID;
            Name = aplication.Name;
            FileName = aplication.FileName;
            Version = aplication.Version;
            Description = aplication.Description;
            Company = aplication.Company;
            WinName = aplication.WinName;

            if (aplication.Limits != null)
            {
                Limits = aplication.Limits.Select(l => new AppLimitModel(l));
                ObservableLimits = new ObservableCollection<AppLimitModel>(Limits);
            }

            if (aplication.Categories != null)
            {
                Categories = aplication.Categories.Select(c => new AppCategoryModel(c));
                ObservableCategories = new ObservableCollection<AppCategoryModel>(Categories);
            }

        }

        public ObservableCollection<AppLimitModel> ObservableLimits { get; }

        public ObservableCollection<AppCategoryModel> ObservableCategories { get; }

        public int ApplicationID { get; }

        public string Name { get; }

        public string FileName { get; }

        public string Version { get; }

        public string Description { get; }

        public string Company { get; }

        public string WinName { get; }

        public IEnumerable<AppLimitModel> Limits { get; }

        public IEnumerable<AppCategoryModel> Categories { get; }
    }
}
