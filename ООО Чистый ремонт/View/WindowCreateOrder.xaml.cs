using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
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
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure;
using ООО_Чистый_ремонт.Model;

namespace ООО_Чистый_ремонт.View
{
    /// <summary>
    /// Логика взаимодействия для WindowCreateOrder.xaml
    /// </summary>
    public partial class WindowCreateOrder : Window
    {
        List<Classes.OrderDec> orderDecs = new List<OrderDec>();
        public WindowCreateOrder()
        {
            InitializeComponent();
        }

        private void btn_Back_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ShowOrder();
        }
        public WindowCreateOrder(List<OrderDec> orderDecs) 
        {
            InitializeComponent();
            this.orderDecs = orderDecs;
            lbDecoration.ItemsSource=orderDecs;
            ShowOrder();
        }
        

        //public WindowCreateOrder(List<OrderDec> orderDecs)
        //{
        //    InitializeComponent();
        //    this.listOrder = listOrder;
        //    listBoxOrder.ItemsSource = listOrder;
        //    ShowInfo();
        //    List<Model.Requests> points = new List<Model.Requests>();
        //    points = Helper.DB.Requests.ToList();

        //}

        double sum=0;
        double disc=0;
        double newprice=0;
        double SqM;

        private void ShowOrder()
        {
            tbFIO.Text = App.UserCurrent.UserName;
            List<Model.User> clients = App.DB.User.Where(u => u.UserRoleIID == 3).ToList();
            cbClient.ItemsSource = clients;
            cbClient.SelectedValuePath = "UserID";
            cbClient.DisplayMemberPath = "UserName";
            cbClient.SelectedIndex = 0;


            if (orderDecs.Count == 0) butOrder.Visibility = Visibility.Hidden;
            else butOrder.Visibility = Visibility.Visible;
           
            
             sum = 0;
             disc = 0;
             newprice = 0;
            //string text=tbSqM.Text;
            //if(text.Length >0)
            //{ SqM = Convert.ToDouble(text);}
            foreach (Classes.OrderDec decoration in orderDecs)
            {
                
                sum += decoration.DecorationExt.decoration.DecorationPriceSqM * decoration.CountProductInOrder;
                newprice += decoration.DecorationExt.NewPrice * decoration.CountProductInOrder;
                
            }
            lbDecoration.ItemsSource=orderDecs;
            tbTotalSum.Text = "Сумма заказа: " + sum.ToString();
            tbDiscount.Text = "Cумма скидки:" + Convert.ToString(sum - newprice);
            tbDiscountSum.Text = "Сумма со скидкой: " + newprice.ToString();
            
        }

        private void butOrder_Click(object sender, RoutedEventArgs e)
        {
            int idClient = Convert.ToInt32(cbClient.SelectedValue.ToString());
            Model.User client = App.DB.User.ToList().Where(u => u.UserID == idClient).FirstOrDefault();
            DateTime dtNow = DateTime.Now;

            Model.Order newOrder = new Model.Order();
            newOrder.OrderClientID = idClient;
            newOrder.OrderWorkerID = App.UserCurrent.UserID;
            newOrder.OrderDate = dtNow;
            newOrder.OrderStatusID = 1;
            if (tbComm.Text != "")
            {
                newOrder.OrderComm = tbComm.Text.ToString();
            }
            App.DB.Order.Add(newOrder);
            try
            {
                App.DB.SaveChanges();
                foreach (Classes.OrderDec item in orderDecs)
                {
                    Model.Order orderProduct = new Model.Order();
                    orderProduct.OrderDecID = item.DecorationExt.decoration.DecorationID;
                    orderProduct.OrderSqM = item.CountProductInOrder;
                    orderProduct.OrderPrice=item.DecorationExt.NewPrice;
                    App.DB.Order.Add(orderProduct);
                    App.DB.SaveChanges();
                }
                MessageBox.Show("Заказ оформлен");
                orderDecs.Clear();
                this.Close();
            }
            catch
            {
                MessageBox.Show("Произошёл сбой при сохранении");
            }
           
            //App.DB.SaveChanges();

            //newOrder.OrderID = App.DB.Order.ToList().Last().OrderID;

            //foreach (Classes.DecorationExt product in orderDecs)
            //{
            //    Model.Order productOrder = new Model.Order();
            //    productOrder.OrderDecID = product.decoration.DecorationID;
            //    productOrder.OrderSqM =orderDecs.Count;
            //    App.DB.Order.Add(productOrder);
            //    App.DB.SaveChanges();
            //}
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            //string art = (sender as Button).Tag as string;
           // OrderDec selectDec = orderDecs.Find(pr => pr.DecorationExt.decoration.DecorationName == art);
           Classes.OrderDec selectDec = (sender as Button).DataContext as Classes.OrderDec;
            orderDecs.Remove(selectDec);
            ShowOrder();
        }

        //private void tbSqM_TextChanged(object sender, TextChangedEventArgs e)
        //{

        //    //string text= tbSqM.Text;
        //    //tbSqM
        //    //ShowOrder();

        //}

        private void tbCountInOrder_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox tb = sender as TextBox;
            if (tb.Text != "")
            {

                OrderDec selectProduct = (sender as TextBox).DataContext as OrderDec;
                if (tb.Text != "0")
                {
                    try
                    {
                        int ind = orderDecs.IndexOf(selectProduct);
                        orderDecs[ind].CountProductInOrder = Convert.ToInt32(tb.Text.ToString());
                    }
                    catch
                    {
                        MessageBox.Show("Можно вводить только числа");
                    }
                }
                else
                {
                    orderDecs.Remove(selectProduct);
                }
                ShowOrder();
            }
        }
    }
}
