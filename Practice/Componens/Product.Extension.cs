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
                if (Navigation.AutoUser.RolyId == 2)
                    return Visibility.Collapsed;
                else
                    return Visibility.Visible;
            }
        }
        public string ColorDis
        {
            get
            {
                if (InStock != null)
                    return "#00FF00";
                else
                    return "#FF0000";
            }
        }
        public string Activ
        {
            get
            {
                if (InStock != null)
                    return "Есть в наличии";
                else
                    return "Нет в наличии";
            }
        }
    }
}
