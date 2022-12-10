using EsoftApp.FailiDannih;
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
    /// Interaction logic for PageGlavnayaForma.xaml
    /// </summary>
    public partial class PageGlavnayaForma : Page
    {
        public PageGlavnayaForma()
        {
            InitializeComponent();
        }

        private void InfoNetRazdela()
        {
            MessageBox.Show("Функционал всё ещё в разработке.", "Нет раздела", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void BtnIspoliteli_Click(object sender, RoutedEventArgs e)
        {
            InfoNetRazdela();
        }

        private void BtnZadachi_Click(object sender, RoutedEventArgs e)
        {

            NavigaciyaObj.frmNavigaciya.Navigate(new PageZadachi());
        }

        private void BtnYpravleniyeCoefficientami_Click(object sender, RoutedEventArgs e)
        {
            InfoNetRazdela();
        }

        private void BtnRaschetZarplati_Click(object sender, RoutedEventArgs e)
        {
            InfoNetRazdela();
        }
    }
}
