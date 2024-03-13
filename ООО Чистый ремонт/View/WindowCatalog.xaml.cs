using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ООО_Чистый_ремонт.Classes;
using ООО_Чистый_ремонт.Model;

namespace ООО_Чистый_ремонт.View
{
    /// <summary>
    /// Логика взаимодействия для WindowCatalog.xaml
    /// </summary>
    public partial class WindowCatalog : Window
    {
        List<OrderDec> order = new List<OrderDec>();
        public WindowCatalog()
        {
            InitializeComponent();
        }

        private void btn_Back_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            btnAddDec.Visibility = Visibility.Collapsed;
            btnOrder.Visibility=Visibility.Collapsed;
            btnMyOrders.Visibility=Visibility.Collapsed;
            cmAdd.Visibility = Visibility.Collapsed;
            if (App.UserCurrent != null)
            {
                tbFIO.Text = App.UserCurrent.UserName;
                switch (App.UserCurrent.UserRoleIID)
                {
                    case 1:
                        cmAdd.Visibility = Visibility.Visible;
                        btnAddDec.Visibility = Visibility.Visible;
                        btnOrder.Visibility = Visibility.Visible;
                        break;
                    case 2:
                        cmAdd.Visibility = Visibility.Visible;
                        btnOrder.Visibility = Visibility.Visible;
                        break;
                    case 3:
                        btnMyOrders.Visibility = Visibility.Visible;
                        break;
                }
            }
            ShowCatalog();
            
        }

        private void ShowCatalog()
        {
           

            List<Model.Decoration> decorations = new List<Model.Decoration>();
            decorations = App.DB.Decoration.ToList();
            List<Classes.DecorationExt> decorationExts = new List<Classes.DecorationExt>();
            foreach (Model.Decoration decoration in decorations)
            {
                Classes.DecorationExt decorationExt = new Classes.DecorationExt(decoration);
                decorationExt.decoration=decoration;
                decorationExts.Add(decorationExt);
            }
           

            switch (cbPrice.SelectedIndex)
            {
                case 1:
                    decorationExts = decorationExts.OrderBy(pr => pr.NewPrice).ToList();
                    break;
                case 2:
                    decorationExts = decorationExts.OrderByDescending(pr => pr.NewPrice).ToList();
                    break;
            }

            switch (cbDiscount.SelectedIndex)
            {
                case 1:
                    decorationExts = decorationExts.OrderBy(pr => pr.decoration.DecorationDiscountPercent).ToList();
                    break;
                case 2:
                    decorationExts = decorationExts.OrderByDescending(pr => pr.decoration.DecorationDiscountPercent).ToList();
                    break;
            }

            string search = tbSearch.Text;	
            if (search.Length > 0)		
            {
                search = search.ToLower();
                decorationExts = decorationExts.Where(pr => pr.decoration.DecorationName.ToLower().Contains(search)).ToList();
            }
            lbDecoration.ItemsSource = decorationExts;
            tbCol.Text = decorationExts.Count.ToString() + " позиций из " + decorations.Count.ToString();
        }

        private void btnAddDec_Click(object sender, RoutedEventArgs e)
        {
            View.WindowDecoration orderWindow = new WindowDecoration();
            this.Hide();
            orderWindow.ShowDialog();
            this.ShowDialog();
        }

        private void btnOrder_Click(object sender, RoutedEventArgs e)
        {
            View.WindowCreateOrder orderWindow = new WindowCreateOrder(order);
            this.Hide();
            orderWindow.ShowDialog();
            this.ShowDialog();
        }

        private void tbSearch_SelectionChanged(object sender, RoutedEventArgs e)
        {
            ShowCatalog();
        }

        private void cbPrice_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ShowCatalog();
        }

        private void cbDiscount_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ShowCatalog();
        }

        private void miAdd_Click(object sender, RoutedEventArgs e)
        {
           
            int ind = lbDecoration.SelectedIndex;
            Classes.DecorationExt decExtendedSelected = (Classes.DecorationExt)lbDecoration.SelectedItem;

            //Поиск выбранного товара по артиклу в заказе
            Classes.OrderDec decInOrderFind = order.Find(pr => pr.DecorationExt.decoration.DecorationID == decExtendedSelected.decoration.DecorationID);
            //Товар найден увеличить количество

            if (decInOrderFind == null)
            {
                Classes.OrderDec orderNew = new OrderDec(decExtendedSelected);
                order.Add(orderNew);
            }
            
        }

        private void miAddDec_Click(object sender, RoutedEventArgs e)
        {
            //int ind = lbDecoration.SelectedIndex;
            //Classes.DecorationExt decExtendedSelected = (Classes.DecorationExt)lbDecoration.SelectedItem;

            ////Поиск выбранного товара по артиклу в заказе
            //App.DB.Decoration.Add; decInOrderFind =
            //    //= order.Find(pr => pr.DecorationExt.decoration.DecorationID == decExtendedSelected.decoration.DecorationID);
            ////Товар найден увеличить количество

            //if (decInOrderFind == null)
            //{
            //    Classes.OrderDec orderNew = new OrderDec(decExtendedSelected);
            //    order.Add(orderNew);
            //}
        }

        private void btnMyOrders_Click(object sender, RoutedEventArgs e)
        {
            View.WindowMyOrder windowMyOrder = new WindowMyOrder();
            this.Hide();
            windowMyOrder.ShowDialog();
            this.ShowDialog();
        }
    }
}
