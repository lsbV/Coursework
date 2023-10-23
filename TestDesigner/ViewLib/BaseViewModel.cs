using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestDesigner.ViewLib
{
    public abstract class BaseViewModel : INotifyPropertyChanged, IDisposable
    {
        public string Title { get; set; }
        public event PropertyChangedEventHandler? PropertyChanged;

        public virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void Dispose()
        {
            OnDispose();
        }

        protected abstract void OnDispose();
    }
}
