﻿#region Licence
/*
  *  Author: Marko Devcic, madevcic@gmail.com
  *  Copyright: Marko Devcic, 2015
  *  Licence: http://creativecommons.org/licenses/by-nc-nd/4.0/
 */
#endregion

using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Windows.Input;
using AppsTracker.Common.Utils;

namespace AppsTracker.MVVM
{
    public abstract class HostViewModel : ViewModelBase
    {
        private readonly IDictionary<Type, ViewModelResolver> childrenMap = new Dictionary<Type, ViewModelResolver>();

        private ViewModelBase selectedChild;


        public ViewModelBase SelectedChild
        {
            get { return selectedChild; }
            set
            {
                if (value == null || (selectedChild != null && selectedChild.Title == value.Title))
                    return;

                SetPropertyValue(ref selectedChild, value);
            }
        }

      
        protected T ProduceViewModel<T>(ExportFactory<T> factory) where T : ViewModelBase
        {
            using (var context = factory.CreateExport())
            {
                return context.Value;
            }
        }


        protected void RegisterChild<T>(Func<T> valueFactory) where T : ViewModelBase
        {
            if (childrenMap.ContainsKey(typeof(T)))
                return;
            Ensure.NotNull(valueFactory);

            var resolver = new ViewModelResolver(valueFactory);
            childrenMap.Add(typeof(T), resolver);
        }


        protected ViewModelBase GetChild<T>() where T : ViewModelBase
        {
            Ensure.Condition<InvalidOperationException>(childrenMap.ContainsKey(typeof(T)),
                string.Format("Type {0} not registed", typeof(T)));

            var resolver = childrenMap[typeof(T)];
            return GetChildInternal(resolver);
        }


        protected ViewModelBase GetChild(Type type)
        {
            Ensure.NotNull(type);
            Ensure.Condition<InvalidOperationException>(childrenMap.ContainsKey(type) == true,
                string.Format("Type {0} not registed", type));

            var resolver = childrenMap[type];
            return GetChildInternal(resolver);
        }


        private ViewModelBase GetChildInternal(ViewModelResolver resolver)
        {
            ViewModelBase viewModel = null;
            resolver.Reference.TryGetTarget(out viewModel);
            if (viewModel == null)
            {
                viewModel = resolver.ValueFactory();
                resolver.Reference.SetTarget(viewModel);
            }
            return viewModel;
        }


        private sealed class ViewModelResolver
        {
            public WeakReference<ViewModelBase> Reference { get; private set; }
            public Func<ViewModelBase> ValueFactory { get; private set; }

            public ViewModelResolver(Func<ViewModelBase> valueFactory)
            {
                Reference = new WeakReference<ViewModelBase>(null);
                ValueFactory = valueFactory;
            }
        }
    }
}
