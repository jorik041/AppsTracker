﻿using System;
using AppsTracker.Common.Utils;

namespace AppsTracker.Tracking.Hooks
{
    public abstract class HookBase : IDisposable
    {
        private const uint WINEVENT_OUTOFCONTEXT = 0;

        private bool isDisposed;

        private readonly WinHookCallBack winHookCallBack;

        private readonly IntPtr hookID = IntPtr.Zero;

        public HookBase(uint minEvent, uint maxEvent)
        {
            winHookCallBack = new WinHookCallBack(WinHookCallback);
            hookID = NativeMethods.SetWinEventHook(minEvent, maxEvent, IntPtr.Zero, winHookCallBack, 0, 0, WINEVENT_OUTOFCONTEXT);
        }

        protected abstract void WinHookCallback(IntPtr hWinEventHook, 
            uint eventType, IntPtr hWnd, int idObject, 
            int idChild, uint dwEventThread, uint dwmsEventTime);

        ~HookBase()
        {
            System.Diagnostics.Debug.WriteLine("WinHook finalizer called");
            Dispose(false);
        }

        private void Dispose(bool disposing)
        {
            if (isDisposed)
                return;
            NativeMethods.UnhookWinEvent(hookID);
            isDisposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
