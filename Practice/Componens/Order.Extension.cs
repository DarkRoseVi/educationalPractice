using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice.Componens
{
    public partial class Order
    {
        public ObservableCollection<ProductOrder> ProductOrders
        {
            get
            {
                return new ObservableCollection<ProductOrder>(this.ProductOrder);
            }
        }
        public int? Quanity
        {
            get
            {
                return this.ProductOrder.Sum(x => x.Quantity);
            }
        }
        public decimal? TotalSum
        {
            get
            {
                return this.ProductOrder.Sum(x => x.Quantity * x.Product.Cost);
            }
        }
    }
}
