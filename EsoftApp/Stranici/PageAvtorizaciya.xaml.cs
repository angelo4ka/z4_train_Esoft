using EsoftApp.FailiDannih;
using LibraryRaschetZarplati;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EsoftApp.Stranici
{
    /// <summary>
    /// Interaction logic for PageAvtorizaciya.xaml
    /// </summary>
    public partial class PageAvtorizaciya : Page
    {
        public PageAvtorizaciya()
        {
            InitializeComponent();
        }

        private void BtnVhod_Click(object sender, RoutedEventArgs e)
        {
            bool PodtvergzdeniyeVhoda = false;

            List<User> polzovateli = PodclucheniyeOdb.podcluchObj.User.ToList();
            foreach (User polzovatel in polzovateli)
            {
                if ((polzovatel.Login == TbxLogin.Text) && Avtorizaciya.ProverkaParolya(PswbxParol.Password, polzovatel.Password))
                {
                    PodtvergzdeniyeVhoda = true;

                    break;
                }
            }

            if (!PodtvergzdeniyeVhoda)
            {
                MessageBox.Show("Неверный логин или пароль. Пожалуйста, попробуйте ввести данные заново.",
                    "Ошибка авторизации", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                MessageBox.Show("Вы выполнили вход!");
                NavigaciyaObj.frmNavigaciya.Navigate(new PageGlavnayaForma());
            }
        }
    }
}
