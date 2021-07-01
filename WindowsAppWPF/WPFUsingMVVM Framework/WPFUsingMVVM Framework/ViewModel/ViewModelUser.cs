using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Data;
using System.Windows.Threading;
using WPFUsingMVVM_Framework.Model;

namespace WPFUsingMVVM_Framework.ViewModel
{
    public class ViewModelUser: ViewModelBase
    {
        public RelayCommand CancelCommand { get; set; }
        public RelayCommand SaveCommand { get; set; }
        public RelayCommand AddUserCommand { get; set; }
        public RelayCommand DeleteUserCommand { get; set; }

        PersonnelBusinessObject personnel;
        ObservableCollection<User> _Employee;
        BindingGroup _UpdateBindingGroup;
        public int SelectedIndex { get; set; }
        object _SelectedEmployee;

        public ViewModelUser()
        {
            personnel = new PersonnelBusinessObject();
            CancelCommand = new RelayCommand(DoCancel);
            SaveCommand = new RelayCommand(DoSave);
            AddUserCommand = new RelayCommand(AddUser);
            DeleteUserCommand = new RelayCommand(DeleteUser);

            personnel.EmployeeChanged += new EventHandler(personnel_EmployeeChanged);
            UpdateBindingGroup = new BindingGroup { Name = "Group1" };
        }
        public ObservableCollection<User> Employee
        {
            get
            {
                _Employee = new ObservableCollection<User>(personnel.GetEmployees());
                return _Employee;
            }
        }

        void DoCancel(object param)
        {
            UpdateBindingGroup.CancelEdit();
            if (SelectedIndex == -1)    //This only closes if new - just to show you how CancelEdit returns old values to bindings
                SelectedEmployee = null;
        }

        void DoSave(object param)
        {
            UpdateBindingGroup.CommitEdit();
            var employee = SelectedEmployee as User;
            if (SelectedIndex == -1)
            {
                personnel.AddEmployee(employee);
                RaisePropertyChanged("Employee"); // Update the list from the data source
            }
            else
                personnel.UpdateEmployee(employee);

            SelectedEmployee = null;
        }

        void AddUser(object parameter)
        {
            SelectedEmployee = null; // Unselects last selection. Essential, as assignment below won't clear other control's SelectedItems
            var employee = new User();
            SelectedEmployee = employee;
        }

        void DeleteUser(object parameter)
        {
            var employee = SelectedEmployee as User;
            if (SelectedIndex != -1)
            {
                personnel.DeleteEmployee(employee);
                RaisePropertyChanged("Employee"); // Update the list from the data source
            }
            else
                SelectedEmployee = null; // Simply discard the new object
        }

        void personnel_EmployeeChanged(object sender, EventArgs e)
        {
            Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Background, new Action(() =>
            {
                RaisePropertyChanged("Employee");
            }));
        }


        public BindingGroup UpdateBindingGroup
        {
            get
            {
                return _UpdateBindingGroup;
            }
            set
            {
                if (_UpdateBindingGroup != value)
                {
                    _UpdateBindingGroup = value;
                    RaisePropertyChanged("UpdateBindingGroup");
                }
            }
        }

        public object SelectedEmployee
        {
            get
            {
                return _SelectedEmployee;
            }
            set
            {
                if (_SelectedEmployee != value)
                {
                    _SelectedEmployee = value;
                    RaisePropertyChanged("SelectedEmployee");
                }
            }
        }
    }
}
