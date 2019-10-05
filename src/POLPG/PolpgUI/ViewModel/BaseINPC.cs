using System.ComponentModel;

namespace PolpgUI.ViewModel
{
    /// <summary>
    /// Based on example from: https://www.codeproject.com/articles/100175/model-view-viewmodel-mvvm-explained .
    /// </summary>
    public abstract class BaseINPC : INotifyPropertyChanged
    {
        protected void RaisePropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;

            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}