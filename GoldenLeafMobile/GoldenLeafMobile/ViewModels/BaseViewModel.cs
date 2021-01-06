using GoldenLeafMobile.Models.ClerkModels;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace GoldenLeafMobile.ViewModels
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        private bool _wait;
                
        public bool Wait
        {
            get { return _wait; }
            set
            {
                _wait = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
