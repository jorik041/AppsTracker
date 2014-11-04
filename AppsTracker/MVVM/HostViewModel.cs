﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using AppsTracker.Common.Utils;

namespace AppsTracker.MVVM
{
    internal abstract class HostViewModel : ViewModelBase
    {
        private Hashtable _childrenSet = new Hashtable();

        protected IChildVM _selectedChild;

        protected ICommand _changePageCommand;

        public virtual IChildVM SelectedChild
        {
            get
            {
                return _selectedChild;
            }
            set
            {
                if (_selectedChild != null && _selectedChild.Title == value.Title)
                    return;
                if (_selectedChild != null)
                    ((ViewModelBase)_selectedChild).Dispose();
                _selectedChild = value;
                PropertyChanging("SelectedChild");
                LoadChildContent();
            }
        }

        public virtual ICommand ChangePageCommand
        {
            get
            {
                return _changePageCommand ?? (_changePageCommand = new DelegateCommand(ChangePage));
            }
        }

        protected virtual void ChangePage(object parameter)
        {
            SelectedChild = Resolve(parameter);
        }

        protected void Register<T>(Func<T> getter) where T : IChildVM
        {
            Ensure.NotNull(getter);
            Ensure.Condition<InvalidOperationException>(_childrenSet.ContainsKey(typeof(T)) == false, string.Format("Type {0} is already bound!", typeof(T)));

            _childrenSet.Add(typeof(T), getter);
        }

        protected IChildVM Resolve(object type)
        {
            Ensure.NotNull(type);
            Ensure.Condition<InvalidOperationException>(_childrenSet.ContainsKey(type) == true, string.Format("Can't resolve {0} type!", type.GetType()));

            var getter = _childrenSet[type];
            var res = (Func<IChildVM>)getter;
            return res();
        }

        protected virtual void LoadChildContent()
        {
            if (this.SelectedChild != null)
            {
                if (!this.SelectedChild.IsContentLoaded)
                    this.SelectedChild.LoadContent();
            }
        }
    }
}
