﻿#region Licence
/*
  *  Author: Marko Devcic, madevcic@gmail.com
  *  Copyright: Marko Devcic, 2014
  *  Licence: http://creativecommons.org/licenses/by-nc-nd/4.0/
 */
#endregion

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;


namespace AppsTracker.Data.Models
{
    public class MostUsedAppModel
    {
        public string AppName { get; set; }
        public double Duration { get; set; }
    }

    public class AllUsersModel
    {
        public string Username { get; set; }
        public double LoggedInTime { get; set; }
    }

    public struct BlockedAppModel
    {
        public string AppName { get; set; }
        public int Count { get; set; }
    }

    public class KeystrokeModel
    {
        public string AppName { get; set; }
        public int Count { get; set; }
    }

    public class ScreenshotModel
    {
        public string AppName { get; set; }
        public int Count { get; set; }
    }

    public struct UsageModel
    {
        public string Date { get; set; }
        public double Count { get; set; }
    }

    public struct DailyUsedAppsSeries
    {
        public string Date { get; set; }
        public List<MostUsedAppModel> DailyUsedAppsCollection { get; set; }
    }

    public struct UsageSummary
    {
        public string UsageType { get; set; }
        public double Time { get; set; }
    }

    public struct UsageTypeSeries
    {
        public string Date { get; set; }
        public DateTime DateInstance { get; set; }
        public ObservableCollection<UsageSummary> DailyUsageTypeCollection { get; set; }
    }

    public struct UsageByTime
    {
        public string Time { get; set; }
        public ObservableCollection<UsageSummary> UsageSummaryCollection { get; set; }
    }

    public struct DailyTopWindowSeries
    {
        public string Date { get; set; }
        public ObservableCollection<WindowSummary> DailyUsageTypeCollection { get; set; }
    }

    public struct DailyBlockedAppModel
    {
        public string Date { get; set; }
        public int Count { get; set; }
    }

    public struct DailyAppModel
    {
        public string Date { get; set; }
        public double Duration { get; set; }
    }

    public struct DailyKeystrokeModel
    {
        public string Date { get; set; }
        public int Count { get; set; }
    }

    public struct DailyScreenshotModel
    {
        public string Date { get; set; }
        public int Count { get; set; }
    }

    public struct FilewatcherModel
    {
        public string Event { get; set; }
        public int Count { get; set; }
    }

    public struct WindowDuration
    {
        public double Duration { get; set; }
        public string Title { get; set; }
    }

    public struct WindowDurationOverview
    {
        public string Date { get; set; }
        public List<WindowDuration> DurationCollection { get; set; }
    }

    public struct CategoryModel
    {
        public string Name { get; set; }
        public double TotalTime { get; set; }
    }
}