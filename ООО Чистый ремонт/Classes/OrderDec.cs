using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ООО_Чистый_ремонт.Classes
{
    public class OrderDec
    {
       public DecorationExt DecorationExt { get; set; }
        public int CountProductInOrder { get; set; }
        public OrderDec(DecorationExt decorationextended)
        {
            //ProductInOrder productInOrder = new ProductInOrder(productExtended);
            DecorationExt = decorationextended;
           CountProductInOrder = 1;
        }

        //public int id { get; set; }
        //public string ClientName { get; set; }
        //public string WorkerName { get; set; }
        //public DateTime Date { get; set; }
        //public int Status { get; set; }
        //public string Comment { get; set; }
        //public double SqM { get; set; }
        //public Model.Order order { get; set; }
        //public OrderDec(Model.Order order)
        //{
        //    this.id = order.OrderID;
        //    this.ClientName = App.DB.User.ToList().Where(u => u.UserID == order.OrderClientID).FirstOrDefault().UserName;
        //    this.WorkerName = App.DB.User.ToList().Where(u => u.UserID == order.OrderWorkerID).FirstOrDefault().UserName;
        //    this.Date = order.OrderDate;
        //    this.Status = (int)order.OrderStatusID;
        //    this.Comment = order.OrderComm;
        //    this.SqM = order.OrderSqM;
        //    this.order = order;
        //}
    }
}
