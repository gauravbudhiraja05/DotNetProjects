using System.ComponentModel;

namespace WPFUsingMVVM_Framework.Model
{
    public class NationalityCollection : INotifyPropertyChanged
    {
        ICollectionView _nationalityCollection;
        public ICollectionView NationalityView
        {
            get { return _nationalityCollection; }
            set
            {
                if (value != _nationalityCollection)
                {
                    _nationalityCollection = value;
                    NotifyOfPropertyChange("NationalityCollection");
                }
            }
        }

        public string Nationality
        {
            get { return _nationality; }
            set
            {
                _nationality = value;
                NotifyOfPropertyChange("Nationality");
            }
        }

        #region INotifyPropertyChanged Members
        public event PropertyChangedEventHandler PropertyChanged;
        private string _nationality;

        private void NotifyOfPropertyChange(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
