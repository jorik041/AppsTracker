﻿#region Licence
/*
  *  Author: Marko Devcic, madevcic@gmail.com
  *  Copyright: Marko Devcic, 2014
  *  Licence: http://creativecommons.org/licenses/by-nc-nd/4.0/
 */
#endregion

using System;
using System.Collections.Generic;
using AppsTracker.Data.Models;
using AppsTracker.Data.Service;
using AppsTracker.MVVM;

namespace AppsTracker.ViewModels
{
    internal sealed class KeystrokesViewModel : ViewModelBase, ICommunicator
    {
        private readonly IDataService _dataService;

        public override string Title
        {
            get { return "KEYSTROKES"; }
        }

        private readonly AsyncProperty<IEnumerable<Log>> _logList;
        public AsyncProperty<IEnumerable<Log>> LogList
        {
            get { return _logList; }
        }

        public IMediator Mediator
        {
            get { return MVVM.Mediator.Instance; }
        }

        public KeystrokesViewModel()
        {
            _dataService = ServiceFactory.Get<IDataService>();

            _logList = new AsyncProperty<IEnumerable<Log>>(GetContent, this);

            Mediator.Register(MediatorMessages.RefreshLogs, new Action(_logList.Reload));
        }

        private IEnumerable<Log> GetContent()
        {
            return _dataService.GetFiltered<Log>(l => l.KeystrokesRaw != null
                                                && l.DateCreated >= Globals.DateFrom
                                                && l.DateCreated <= Globals.DateTo
                                                && l.Window.Application.UserID == Globals.SelectedUserID
                                                , l => l.Window.Application
                                                , l => l.Screenshots);
        }
    }
}