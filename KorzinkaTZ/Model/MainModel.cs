using KorzinkaTZ.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace KorzinkaTZ.Model
{
    internal class MainModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        //Справочник
        public ObservableCollection<GuidProduct> GuidProducts { get; }
        //Активные продукты из справочника
        public ObservableCollection<GuidProduct> ActiveProducts { get; }

        //Корзина
        public ObservableCollection<HistoryProduct> BasketProducts { get; }
        private HistoryPurchase _basket;
        public HistoryPurchase Basket
        {
            get => _basket; set
            {
                _basket = value;
                OnPropertyChanged(nameof(Basket));
            }
        }

        //История
        public ObservableCollection<FullHistoryPurchase> FullHistoryPurchases { get; }

        DataBaseContext db = new();
        public MainModel()
        {

            db.Database.EnsureCreated();
            // Вывод списка продуктов из справочника
            GuidProducts = new ObservableCollection<GuidProduct>(db.GuidProducts);

            //Вывод истории
            FullHistoryPurchases = new ObservableCollection<FullHistoryPurchase>();
            foreach (var purchase in db.HistoryPurchases)
            {
                FullHistoryPurchase fullHistoryPurchase = new FullHistoryPurchase();
                fullHistoryPurchase.Purchase = purchase;
                fullHistoryPurchase.Products = new(db.HistoryProducts.Where(p => p.Purchase == purchase));
                FullHistoryPurchases.Add(fullHistoryPurchase);
            }

            //Корзина
            Basket = new HistoryPurchase();
            Basket.Date = DateTime.Now;
            BasketProducts = new();

            ActiveProducts = new(GuidProducts.Where(p => p.IsActive));

            foreach (var product in GuidProducts)
            {
                product.PropertyChanged += OnGuidProductPropertyChanged;
            }
        }
        ~MainModel() 
        {
            db.Dispose();
        }
        //Нажатие на чекбокс в справочнике - добавление продукта в список продуктов MainWindow
        void OnGuidProductPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (sender != null)
            {
                var product = (GuidProduct)sender;

                db.GuidProducts.Update(product);
                db.SaveChanges();

                if (e.PropertyName == "IsActive")
                {
                    if (product.IsActive)
                    {
                        ActiveProducts.Add(product);
                    }
                    else
                    {
                        ActiveProducts.Remove(product);
                    }
                }
            }

        }
        //Добавление продукта в справочник
        public void AddGuidProduct(string name)
        {
            if(GuidProducts.Any(p => p.Name == name))
            {
                return;
            }
            GuidProduct product = new GuidProduct();
            product.Name = name;
            db.GuidProducts.Add(product);
            db.SaveChanges();
            GuidProducts.Add(product);
            product.PropertyChanged += OnGuidProductPropertyChanged;
            OnPropertyChanged("AllProducts");
        }
        // Добавление продукта в корзину
        public void AddProductToBasket(GuidProduct product)
        {
            HistoryProduct historyProduct = new HistoryProduct();
            historyProduct.GuidProduct = product;
            historyProduct.Purchase = Basket;
            BasketProducts.Add(historyProduct);
        }
        //Перемещение из корзины в историю
        public void SavePurchase()
        {
            if (BasketProducts.Count != 0)
            {
                db.HistoryPurchases.Add(Basket);
                foreach (var product in BasketProducts)
                {
                    db.HistoryProducts.Add(product);
                }
                db.SaveChanges();

                FullHistoryPurchases.Add(new FullHistoryPurchase { Products = new(BasketProducts), Purchase = Basket });

                BasketProducts.Clear();
                Basket = new HistoryPurchase();
                Basket.Date = DateTime.Now;
            }
        }
        //Очистка корзины
        public void ClearPurchase()
        {
            BasketProducts.Clear();
        }
        //Копирование из истории в корзину
        public void FromHistoryToBasket(FullHistoryPurchase fullHistoryPurchase)
        {
            var newBasket = new HistoryPurchase() { 
                Date= DateTime.Now
            };

            Basket = newBasket;
            BasketProducts.Clear();

            foreach (var product in fullHistoryPurchase.Products)
            {
                var newProduct = new HistoryProduct()
                {
                    Count= product.Count,
                    Price= product.Price,
                    Purchase = Basket,
                    GuidProduct = product.GuidProduct
                };
                BasketProducts.Add(newProduct);
            }
        }
    }
}
