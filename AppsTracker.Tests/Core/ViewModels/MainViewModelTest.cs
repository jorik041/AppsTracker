﻿#region Licence
/*
  *  Author: Marko Devcic, madevcic@gmail.com
  *  Copyright: Marko Devcic, 2014
  *  Licence: http://creativecommons.org/licenses/by-nc-nd/4.0/
 */
#endregion

using System.Collections.Generic;

using AppsTracker.DAL.Service;
using AppsTracker.Models.EntityModels;
using AppsTracker.Tests.Fakes.Service;
using AppsTracker.ViewModels;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AppsTracker.Tests.Core.ViewModels
{
    [TestClass]
    public class MainViewModelTest
    {
        [TestInitialize]
        public void Init()
        {
            if (!ServiceFactory.ContainsKey<IAppsService>())
                ServiceFactory.Register<IAppsService>(() => new AppsServiceMock());
            if (!ServiceFactory.ContainsKey<IChartService>())
                ServiceFactory.Register<IChartService>(() => new ChartServiceMock());
        }

        [TestMethod]
        public void TestMainVM()
        {
            var mainVM = new MainViewModel();

            Assert.IsNotNull(mainVM);
            Assert.IsInstanceOfType(mainVM.UserCollection, typeof(List<Uzer>), "UserCollection types don't match");
            Assert.IsInstanceOfType(mainVM.SelectedChild, typeof(DataHostViewModel), "Selected child types don't match");

            mainVM.ChangePageCommand.Execute(typeof(StatisticsHostViewModel));

            Assert.IsInstanceOfType(mainVM.SelectedChild, typeof(StatisticsHostViewModel), "Change page is not working");
        }
    }
}