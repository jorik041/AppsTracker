﻿#region Licence
/*
  *  Author: Marko Devcic, madevcic@gmail.com
  *  Copyright: Marko Devcic, 2014
  *  Licence: http://creativecommons.org/licenses/by-nc-nd/4.0/
 */
#endregion

using System;
using System.Windows.Forms;

namespace AppsTracker.Controls
{
    public class TrayIcon : IDisposable
    {
        private NotifyIcon notifyIcon;
        private ContextMenuStrip iconMenu;
        private ToolStripMenuItem menuItemShowApp;
        private ToolStripMenuItem menuItemExit;

        public ToolStripMenuItem ShowApp
        {
            get { return menuItemShowApp; }
        }
        public bool IsVisible
        {
            get { return notifyIcon.Visible; }
            set { notifyIcon.Visible = value; }
        }

        #region Constructor

        public TrayIcon()
        {
            notifyIcon = new NotifyIcon();
            iconMenu = new ContextMenuStrip();
            menuItemShowApp = new ToolStripMenuItem(string.Format("Open {0}", Constants.APP_NAME));
            menuItemExit = new ToolStripMenuItem("Exit");
            iconMenu.Items.Add(menuItemShowApp);
            iconMenu.Items.Add(menuItemExit);
            notifyIcon.ContextMenuStrip = iconMenu;
            notifyIcon.Icon = Properties.Resources.icon1;
            notifyIcon.Text = Constants.APP_NAME;
            notifyIcon.Visible = true;

            #region Event Handlers

            menuItemExit.Click += (s, e) => { (App.Current as App).FinishAndExit(); };

            #endregion
        }

        #endregion


        #region IDisposable Members

        public void Dispose()
        {
            Dispose(true);
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (menuItemExit != null) { menuItemExit.Dispose(); menuItemExit = null; }
                if (menuItemShowApp != null) { menuItemShowApp.Dispose(); menuItemShowApp = null; }
                if (iconMenu != null) { iconMenu.Dispose(); iconMenu = null; }
                if (notifyIcon != null) { notifyIcon.Dispose(); notifyIcon = null; }
            }
        }

        #endregion
    }
}