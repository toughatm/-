using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ООО_Чистый_ремонт.Classes
{
    public class DecorationExt
    {
        public Model.Decoration decoration { get; set; }
        public string IimagePath { get; set; }
        //public double DiscountPrice { get; set; }
        //public double NewPrice { get; set; }
        //public int Count { get; set; }
        //public Visibility visible { get; set; }

        //public string IimagePath
        //{
        //    get
        //    {
                
        //        if (decoration.DecorationImg != null)
        //        {
        //            this.IimagePath = Directory.GetCurrentDirectory() + "/Images/" + decoration.DecorationImg;
        //        }
        //        else
        //        {
        //            temp = "../Resources/null.png";
        //        }
        //        return temp;
        //    }
        //}

        public double NewPrice
        {
            get
            {
                if (decoration.DecorationDiscountPercent != null)
                {
                    double discount = decoration.DecorationPriceSqM * ((double)decoration.DecorationDiscountPercent / 100);
                    return decoration.DecorationPriceSqM - discount;
                }
                else
                {
                    return decoration.DecorationPriceSqM;
                }

            }
        }

        public DecorationExt(Model.Decoration decoration)
        {
            this.decoration = decoration;

            if (decoration.DecorationImg != null)
            {
                this.IimagePath = Directory.GetCurrentDirectory() + "/Image/" + decoration.DecorationImg;
            }
            else
            {
                this.IimagePath = "/Resources/null.png";
            }
            //    if (decoration.DecorationDiscountPercent != null)
            //    {
            //        this.DiscountPrice = (double)(decoration.DecorationPriceSqM * (decoration.DecorationDiscountPercent / 100));
            //    }
            //    else
            //    {
            //        this.DiscountPrice = 0;
            //        visible = Visibility.Collapsed;
            //    }
            //    this.NewPrice = decoration.DecorationPriceSqM - this.DiscountPrice;
            //    this.Count = 0;
        }
    }
}
