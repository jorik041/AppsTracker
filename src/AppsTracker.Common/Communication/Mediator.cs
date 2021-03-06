﻿using System;
using System.ComponentModel.Composition;

namespace AppsTracker.Common.Communication
{
    [Export]
    public class Mediator
    {
        private readonly MessageToActionsMap _invocationList = new MessageToActionsMap();
        private readonly MessageToActionsMap _parameterInvocationList = new MessageToActionsMap();

        public void Register(string message, Action callback)
        {
            if (callback == null)
                throw new ArgumentNullException("callBack");

            _invocationList.AddAction(message, callback.Target, callback.Method, null);
        }

        public void Register<T>(string message, Action<T> callback)
        {
            if (callback == null)
                throw new ArgumentNullException("callBack");

            _parameterInvocationList.AddAction(message, callback.Target, callback.Method, typeof(T));
        }

        public virtual void NotifyColleagues<T>(string message, T parameter)
        {
            var actions = _parameterInvocationList.GetActions(message);

            if (actions != null)
                actions.ForEach(action => ((Action<T>)action).Invoke(parameter));
        }

        public virtual void NotifyColleagues(string message)
        {
            var actions = _invocationList.GetActions(message);

            if (actions != null)
                actions.ForEach(action => ((Action)action).Invoke());
        }
    }
}