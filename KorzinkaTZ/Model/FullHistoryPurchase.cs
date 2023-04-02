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
    //Покупка из истории покупок
    public class FullHistoryPurchase
    {
        public ObservableCollection<HistoryProduct> Products { get; set; }
        public HistoryPurchase Purchase { get; set; }
        public decimal FullPrice { get => Products.Select(p => p.FullPrice).Sum(); }
    }
}
