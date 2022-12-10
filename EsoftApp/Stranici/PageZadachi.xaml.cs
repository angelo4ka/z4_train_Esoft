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
using System.Windows.Threading;

namespace EsoftApp.Stranici
{
    /// <summary>
    /// Interaction logic for PageZadachi.xaml
    /// </summary>
    public partial class PageZadachi : Page
    {
        public PageZadachi()
        {
            InitializeComponent();

            var Zadachi = FiltraciyaZadach(PodclucheniyeOdb.podcluchObj.Task.ToList());
            DtgdDanniye.ItemsSource = ZapolnitDanniyeIspolnitela(Zadachi);

            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += UpdateData;
            timer.Start();
        }

        public void UpdateData(object sender, object e)
        {
            // Получаем все записи из БД
            var Zadachi = PodclucheniyeOdb.podcluchObj.Task.ToList();
            Zadachi = FiltraciyaZadach(Zadachi);

            DtgdDanniye.ItemsSource = ZapolnitDanniyeIspolnitela(Zadachi);
        }

        /// <summary>
        /// Фильтрация задач
        /// </summary>
        /// <param name="Zadachi">Список задач</param>
        /// <returns>Отфильтрованные задачи</returns>
        private List<FailiDannih.Task> FiltraciyaZadach(List<FailiDannih.Task> Zadachi)
        {
            List<FailiDannih.Task> FiltrovanniyeZadachi = new List<FailiDannih.Task>();

            if (CkbxStatysIspolnyaetsa.IsChecked == true)
            {
                FiltrovanniyeZadachi.AddRange(Zadachi.Where(x => x.Status.Equals(CkbxStatysIspolnyaetsa.Content)).ToList());
            }
            if (CkbxStatysOtmenena.IsChecked == true)
            {
                FiltrovanniyeZadachi.AddRange(Zadachi.Where(x => x.Status.Equals(CkbxStatysOtmenena.Content)).ToList());
            }
            if (CkbxStatysVipolnyaetsa.IsChecked == true)
            {
                FiltrovanniyeZadachi.AddRange(Zadachi.Where(x => x.Status.Equals(CkbxStatysVipolnyaetsa.Content)).ToList());
            }
            if (CkbxStatysZaplanirovana.IsChecked == true)
            {
                FiltrovanniyeZadachi.AddRange(Zadachi.Where(x => x.Status.Equals(CkbxStatysZaplanirovana.Content)).ToList());
            }

            return FiltrovanniyeZadachi;
        }

        private void BtnRedactirovat_Click(object sender, RoutedEventArgs e)
        {
            FailiDannih.Task zadaniye = (FailiDannih.Task)DtgdDanniye.SelectedItem;

            if (zadaniye != null)
            {
                NavigaciyaObj.frmNavigaciya.Navigate(new PageDobavRedactZadachi(zadaniye));
            }
            else
            {
                MessageBox.Show("Не выделена строка для редактирования.", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void BtnUdalit_Click(object sender, RoutedEventArgs e)
        {
            FailiDannih.Task zadanie = (FailiDannih.Task)DtgdDanniye.SelectedItem;

            if (zadanie != null)
            {
                try
                {
                    PodclucheniyeOdb.podcluchObj.Task.Remove(zadanie);
                    PodclucheniyeOdb.podcluchObj.SaveChanges();

                    MessageBox.Show("Данные успешно удалены!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception er)
                {
                    MessageBox.Show(er.Message.ToString());
                }
            }
            else
            {
                MessageBox.Show("Не выделена строка для удаления.", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void BtnDobavit_Click(object sender, RoutedEventArgs e)
        {
            NavigaciyaObj.frmNavigaciya.Navigate(new PageDobavRedactZadachi(null));
        }

        /// <summary>
        /// Заполнение данных об исполнителе
        /// </summary>
        /// <param name="Zadachi">Список задач</param>
        /// <returns>Исполнители с заполненной информацией</returns>
        private List<FailiDannih.Task> ZapolnitDanniyeIspolnitela(List<FailiDannih.Task> Zadachi)
        {
            var Polzovateli = PodclucheniyeOdb.podcluchObj.User.ToList();
            var Ispolniteli = PodclucheniyeOdb.podcluchObj.Executor.ToList();

            foreach (var zadacha in Zadachi)
            {
                var polzovatel = Polzovateli.Where(x => x.ID.Equals(zadacha.ExecutorID)).ToList();
                var isponitel = Ispolniteli.Where(x => x.ID.Equals(zadacha.ExecutorID)).ToList();
                zadacha.ExecutorTekst = $"{polzovatel[0].LastName} {polzovatel[0].FirstName} {polzovatel[0].MiddleName} ({isponitel[0].Grade})";
            }

            return Zadachi;
        }
    }
}
