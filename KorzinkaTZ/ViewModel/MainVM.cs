using KorzinkaTZ.Model;
using KorzinkaTZ.View;
using MVVM;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace KorzinkaTZ.ViewModel
{
    //MVVM
    public class MainVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string? prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        MainModel _model = new();

        public MainVM()
        {
            _model.PropertyChanged += (s, e) => OnPropertyChanged(e.PropertyName);
        }

        public ObservableCollection<FullHistoryPurchase> FullHistoryPurchases { get => _model.FullHistoryPurchases; }
        public ObservableCollection<GuidProduct> GuidProducts { get => _model.GuidProducts; }
        public ObservableCollection<GuidProduct> ActiveProducts { get => _model.ActiveProducts; }
        public ObservableCollection<HistoryProduct> BasketProducts { get => _model.BasketProducts; }
        public HistoryPurchase Basket { get => _model.Basket; }

        bool guidWindowAlreadyOpen = false;
        private RelayCommand? _showGuidCommand;
        public RelayCommand ShowGuidCommand
        {
            get
            {
                return _showGuidCommand ??
                  (_showGuidCommand = new RelayCommand(obj =>
                  {
                      if (!guidWindowAlreadyOpen)
                      {
                          GuidWindow guidWindow = new(this);
                          guidWindow.Show();
                          guidWindowAlreadyOpen = true;
                          guidWindow.Closed += (s, e) => { guidWindowAlreadyOpen = false; };
                      }
                  }));
            }
        }

        private RelayCommand? _addProductToBasket;
        public RelayCommand AddProductToBasket
        {
            get
            {
                return _addProductToBasket ??
                  (_addProductToBasket = new RelayCommand(obj =>
                  {
                      if(obj is not null)
                      {
                          _model.AddProductToBasket(obj as GuidProduct);
                      }
                  }));
            }
        }

        private RelayCommand? _savePurchase;
        public RelayCommand SavePurchase
        {
            get
            {
                return _savePurchase ??
                  (_savePurchase = new RelayCommand(obj =>
                  {
                      _model.SavePurchase();
                  }));
            }
        }

        private RelayCommand? _clearPurchase;
        public RelayCommand ClearPurchase
        {
            get
            {
                return _clearPurchase ??
                  (_clearPurchase = new RelayCommand(obj =>
                  {
                      _model.ClearPurchase();
                  }));
            }
        }

        public RelayCommand? _fromHistoryToBasket;
        public RelayCommand FromHistoryToBasket
        {
            get
            {
                return _fromHistoryToBasket ??
                  (_fromHistoryToBasket = new RelayCommand(obj =>
                  {
                      if(obj is not null && obj is FullHistoryPurchase)
                      {
                          _model.FromHistoryToBasket(obj as FullHistoryPurchase);
                      }
                  }));
            }
        }


        private RelayCommand? _addGuidProduct;
        public RelayCommand AddGuidProduct
        {
            get
            {
                return _addGuidProduct ??= new RelayCommand(obj =>
                {
                    string productName = WritenProductName.Trim();
                    if (!string.IsNullOrWhiteSpace(productName))
                    {
                        _model.AddGuidProduct(productName);
                    }
                });
            }
        }

        private string _writenProductName = "";
        public string WritenProductName
        {
            get => _writenProductName;
            set { _writenProductName = value; }
        }
    }
}
