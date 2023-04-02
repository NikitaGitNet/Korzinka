using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace KorzinkaTZ.Model
{
    //Продукт в истории покупок
    public class HistoryProduct : INotifyPropertyChanged
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

        private HistoryPurchase _purchase;
        public HistoryPurchase Purchase
        {
            get => _purchase; set
            {
                _purchase = value;
                OnPropertyChanged(nameof(Purchase));
            }
        }

        private GuidProduct _guidProduct;
        public GuidProduct GuidProduct
        {
            get => _guidProduct; set
            {
                _guidProduct = value;
                OnPropertyChanged(nameof(GuidProduct));
            }
        }


        private int _count;
        public int Count
        {
            get => _count; set
            {
                _count = value;
                OnPropertyChanged(nameof(Count));
                OnPropertyChanged(nameof(FullPrice));
            }
        }

        private decimal _price;
        public decimal Price
        {
            get => _price; set
            {
                _price = value;
                OnPropertyChanged(nameof(Price));
                OnPropertyChanged(nameof(FullPrice));
            }
        }

        public decimal FullPrice => Count * Price;
    }
}
