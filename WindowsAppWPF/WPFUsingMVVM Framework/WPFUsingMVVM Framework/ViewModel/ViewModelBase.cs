using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Threading;
using WPFUsingMVVM_Framework.Model;

namespace WPFUsingMVVM_Framework.ViewModel
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        private readonly PersonnelBusinessObject _personnel;
        ObservableCollection<NationalityCollection> _nationalityCollection;

        public ViewModelBase()
        {
            _personnel = new PersonnelBusinessObject();
        }

        //basic ViewModelBase
        public void RaisePropertyChanged(string prop)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
        public event PropertyChangedEventHandler PropertyChanged;

        //Extra Stuff, shows why a base ViewModel is useful
        bool? _CloseWindowFlag;
        public bool? CloseWindowFlag
        {
            get { return _CloseWindowFlag; }
            set
            {
                _CloseWindowFlag = value;
                RaisePropertyChanged("CloseWindowFlag");
            }
        }

        public virtual void CloseWindow(bool? result = true)
        {
            Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Background, new Action(() =>
            {
                CloseWindowFlag = CloseWindowFlag == null
                    ? true
                    : !CloseWindowFlag;
            }));
        }

        public ObservableCollection<NationalityCollection> NationalityList
        {
            get
            {
                _nationalityCollection = new ObservableCollection<NationalityCollection>(_personnel.GetNationality());
                return _nationalityCollection;
            }
        }
    }
}
