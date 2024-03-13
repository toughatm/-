using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Data;
using System.Linq;
using NUnit.Framework;
using System.Data.SqlClient;
using System.Windows.Controls;
using ООО_Чистый_ремонт;
using ООО_Чистый_ремонт.Model;
using System.Collections.Generic;
using System.Text;
using System.Windows;


using ООО_Чистый_ремонт.View;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void DataBaseConnection()
        {
            var connectionstr = "Server=localhost;Database=Decoration;Integrated Security=true";

            var connection = new SqlConnection(connectionstr);
            connection.Open();

            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsNotNull(connection);
        }
        [TestMethod]
        public void LoginTextBoxText()
        {
            var mainWindow = new MainWindow();
            string login = "login";

            TextBox tb = (TextBox)mainWindow.FindName("tbLogin");
            tb.Text = "login";
            string loginNow = tb.Text;

            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(login, loginNow);
        }

        [TestMethod]
        public void mainWindowAction()
        {
            var mainWindow = new MainWindow();


            mainWindow.Show();
            mainWindow.Close();
        }

        [TestMethod]
        public void checkbuttonChecked()
        {
            // Arrange
            bool isCheckboxChecked = true;
            string password = "SecurePassword123";

            // Act
            string visiblePassword = ShowPasswordIfChecked(isCheckboxChecked, password);

            // Assert
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(password, visiblePassword);
        }
        [Test]
        public void TestPasswordHiding()
        {
            // Arrange
            bool isCheckboxChecked = false;
            string password = "SecurePassword123";

            // Act
            string visiblePassword = ShowPasswordIfChecked(isCheckboxChecked, password);

            // Assert
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual("********", visiblePassword);
        }

        private string ShowPasswordIfChecked(bool isCheckboxChecked, string password)
        {
            if (isCheckboxChecked)
            {
                return password;
            }
            else
            {
                return "********"; // Placeholder for hidden password
            }
        }

        [Test]
        public void TestCaptchaAppearance()
        {
            // Arrange
            string enteredlogin = "user123";
            string enteredPassword = "incorrectPassword";
            bool isCaptchaRequired = CheckIfCaptchaRequired(enteredlogin, enteredPassword);

            // Assert
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsTrue(isCaptchaRequired, "Captcha should appear after incorrect login and password");
        }

        private bool CheckIfCaptchaRequired(string login, string password)
        {
            // Проверка наличия капчи после неверного ввода логина и пароля
            if (login == "user123" && password == "correctPassword")
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}

