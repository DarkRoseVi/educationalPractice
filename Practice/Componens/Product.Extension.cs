using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Practice.Componens
{
    public  partial class Product
    {
        public Visibility BtnVisible
        {
            get
            {
                if (Navigation.AutoUser.RolyId == 3)
                    return Visibility.Collapsed;
                else
                    return Visibility.Visible;
            }
        }
    }
}
