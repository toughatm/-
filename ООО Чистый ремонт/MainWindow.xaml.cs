using System;
using System.Collections;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ООО_Чистый_ремонт
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            try 
            {
                App.DB = new Model.DecorationEntities();
                MessageBox.Show("Успешное подключение");
            }
            catch
            {
                MessageBox.Show("Подключение не удалось");
                App.Current.Shutdown();
            } 
            OpenedEye.Visibility = Visibility.Hidden;
            captcha.Visibility = Visibility.Hidden;
            tbCaptcha.Visibility = Visibility.Hidden;
        }

        private void btn_Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        System.Windows.Threading.DispatcherTimer timer;

       
        private void btn_Enter_Click(object sender, RoutedEventArgs e)
        {
            tbPassword.Text = PasswordDot.Password;
            string login = tbLogin.Text;
            string password = PasswordDot.Password;
           

            StringBuilder sb = new StringBuilder();
            if (login == "")
            {
                sb.Append("Введите логин.\n");
            }
            if (password == "")
            {
                sb.Append("Введите пароль.\n");
            }
            if (sb.Length > 0)
            {
                MessageBox.Show(sb.ToString());
                return;
            }
            else
            {
                App.UserCurrent = App.DB.User.ToList().Where(u => u.UserLogin == login && u.UserPassword == password).FirstOrDefault();
                if(App.UserCurrent == null)
                {
                    MessageBox.Show("Пользователь не найден");
                    captcha.Visibility=Visibility.Visible;
                    tbCaptcha.Visibility=Visibility.Visible;
                    captcha.CreateCaptcha(EasyCaptcha.Wpf.Captcha.LetterOption.Alphanumeric, 4);
                    
                    if(tbCaptcha.Text==captcha.CaptchaText&&App.UserCurrent != null)
                    {
                        MessageBox.Show(App.UserCurrent.UserName + "\n" + App.UserCurrent.Role.RoleName);
                        View.WindowCatalog windowCatalog = new View.WindowCatalog();
                        this.Hide();
                        windowCatalog.ShowDialog();
                        this.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("Вы заблокированы на 10 секунд");
                        btn_Enter.IsEnabled = false;
                        captcha.CreateCaptcha(EasyCaptcha.Wpf.Captcha.LetterOption.Alphanumeric, 4);
                        timer = new System.Windows.Threading.DispatcherTimer();
                        timer.Tick += new EventHandler(timerTick);
                        timer.Interval = new TimeSpan(0, 0, 10);
                        timer.Start();
                    }

                }
                if(App.UserCurrent != null)
                {
                    MessageBox.Show(App.UserCurrent.UserName+"\n" + App.UserCurrent.Role.RoleName);
                    View.WindowCatalog windowCatalog = new View.WindowCatalog();
                    this.Hide();
                    windowCatalog.ShowDialog();
                    this.ShowDialog();
                    
                }
               
            }

        }

        private void btn_EnterGuest_Click(object sender, RoutedEventArgs e)
        {
            View.WindowCatalog windowCatalog = new View.WindowCatalog();
            this.Hide();
            windowCatalog.ShowDialog();
            this.ShowDialog();
        }

        private async void timerTick(object sender, EventArgs e)
        {
            btn_Enter.IsEnabled = true;
        }

        private void cbPassVisibility_Checked(object sender, RoutedEventArgs e)
        {
            tbPassword.Visibility = Visibility.Visible;
            PasswordDot.Visibility = Visibility.Hidden;

            tbPassword.Text = PasswordDot.Password;
           //if(tbPassword.)
            OpenedEye.Visibility = Visibility.Visible;
            ClosedEye.Visibility = Visibility.Hidden;
        }

        private void cbPassVisibility_Unchecked(object sender, RoutedEventArgs e)
        {
            tbPassword.Visibility = Visibility.Hidden;
            PasswordDot.Visibility = Visibility.Visible;
            OpenedEye.Visibility = Visibility.Hidden;
            ClosedEye.Visibility = Visibility.Visible;
            tbPassword.Text = PasswordDot.Password;
        }

        private void tbPassword_TextChanged(object sender, TextChangedEventArgs e)
        {
            PasswordDot.Password = tbPassword.Text;
        }
    }
}
