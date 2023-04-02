using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace KorzinkaTZ.Model
{
    //Информация о покупке
    public class HistoryPurchase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
        private int _id;
        public int Id
        {
            get => _id; set
            {
                _id = value;
                OnPropertyChanged(nameof(Id));
            }
        }

        private DateTime _date;
        public DateTime Date
        {
            get => _date; set
            {
                _date = value;
                OnPropertyChanged(nameof(Date));
            }
        }
    }
}
