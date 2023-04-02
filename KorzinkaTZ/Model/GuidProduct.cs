using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace KorzinkaTZ.Model
{
    //Продукт в справочнике
    public class GuidProduct : INotifyPropertyChanged
    {
        private int _id;
        private bool _isActive;
        private string _name = default!;

        public int Id
        {
            get => _id; set
            {
                _id = value;
                OnPropertyChanged(nameof(Id));
            }
        }
        public string Name
        {
            get => _name; set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }
        public bool IsActive
        {
            get => _isActive; set
            {
                _isActive = value;
                OnPropertyChanged(nameof(IsActive));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
