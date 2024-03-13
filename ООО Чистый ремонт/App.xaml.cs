using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace ООО_Чистый_ремонт
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static Model.DecorationEntities DB;
        public static Model.User UserCurrent;
        public static List<Classes.DecorationExt> orderDecs;
    }
}
