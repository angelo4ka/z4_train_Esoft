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
    /// Interaction logic for PageDobavRedactZadachi.xaml
    /// </summary>
    public partial class PageDobavRedactZadachi : Page
    {
        FailiDannih.Task _zadaniye;

        public PageDobavRedactZadachi(FailiDannih.Task zadaniye)
        {
            InitializeComponent();

            _zadaniye = zadaniye;
            var Ispolniteli = new List<int>(PodclucheniyeOdb.podcluchObj.Executor.Select(x => x.ID).ToList());
            CmbxExecutor.ItemsSource = (from i in PodclucheniyeOdb.podcluchObj.Executor
                                        join p in PodclucheniyeOdb.podcluchObj.User on i.ID equals p.ID
                                        select new { p.Login, i.ID}).ToList();
            DtprCreateDateTime.SelectedDate = DateTime.Today;
            DtprDeadline.SelectedDate = DateTime.Today;
            if (zadaniye != null)
            {
                // Заполнение данных формы при редактировании
                TbxZagolivok.Text = "Редактирование задачи";
                CmbxExecutor.SelectedIndex = Ispolniteli.IndexOf(zadaniye.ExecutorID);
                TbxTitle.Text = zadaniye.Title;
                TbxDescription.Text = zadaniye.Description;
                DtprCreateDateTime.Text = Convert.ToString(zadaniye.CreateDateTime);
                DtprDeadline.Text = Convert.ToString(zadaniye.Deadline);
                TbxDifficulty.Text = Convert.ToString(zadaniye.Difficulty);
                TbxTime.Text = Convert.ToString(zadaniye.Time);
                CmbxStatus.SelectedIndex = SpravochnayaInformaciya.Statusi[zadaniye.Status];
                CmbxWorkType.SelectedIndex = SpravochnayaInformaciya.HaracterZadachi[zadaniye.WorkType];
                DtprCompletedDateTime.Text = Convert.ToString(zadaniye.CompletedDateTime);
                CkbxPriznakUdalennosti.IsChecked = zadaniye.IsDeleted;
            }
            else
            {
                TbxZagolivok.Text = "Добавление задачи";
            }
        }

        private void BtnSohranit_Click(object sender, RoutedEventArgs e)
        {
            string tekstDannihIspolnitelya = $"{CmbxExecutor.SelectedItem}";
            string[] dannieIspolnitelya = tekstDannihIspolnitelya.Split(new string[] { "ID = " }, StringSplitOptions.RemoveEmptyEntries);
            int idIspolnitelya = Convert.ToInt32(dannieIspolnitelya[dannieIspolnitelya.Length - 1].Replace(" }", ""));

            string dataVipolneniya = $"{DtprCompletedDateTime.SelectedDate}";
            if (dataVipolneniya == "01.01.0001 0:00:00")
            {
                dataVipolneniya = null;
            }

            if (_zadaniye != null) // Редактирование
            {
                try
                {
                    IEnumerable<FailiDannih.Task> zadachi = PodclucheniyeOdb.podcluchObj.Task.Where(x => x.ID == _zadaniye.ID).AsEnumerable().Select(x =>
                    {
                        x.ExecutorID = idIspolnitelya;
                        x.Title = TbxTitle.Text;
                        x.Description = TbxDescription.Text;
                        x.CreateDateTime = (DateTime)DtprCreateDateTime.SelectedDate;
                        x.Deadline = (DateTime)DtprDeadline.SelectedDate;
                        x.Difficulty = Convert.ToInt32(TbxDifficulty.Text);
                        x.Time = Convert.ToInt32(TbxTime.Text);
                        x.Status = CmbxStatus.Text;
                        x.WorkType = CmbxWorkType.Text;
                        if (dataVipolneniya != null)
                        {
                            x.CompletedDateTime = (DateTime)DtprCompletedDateTime.SelectedDate;
                        }
                        x.IsDeleted = (bool)CkbxPriznakUdalennosti.IsChecked;

                        return x;
                    });

                    foreach (FailiDannih.Task zadacha in zadachi)
                    {
                        PodclucheniyeOdb.podcluchObj.Entry(zadacha).State = System.Data.Entity.EntityState.Modified;
                    }

                    PodclucheniyeOdb.podcluchObj.SaveChanges();
                    MessageBox.Show("Данные успешно изменены!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception er)
                {
                    MessageBox.Show(er.Message.ToString());
                }
            }
            else // Добавление
            {
                try
                {
                    FailiDannih.Task zadacha;
                    if (dataVipolneniya == null)
                    {
                        zadacha = new FailiDannih.Task()
                        {
                            ExecutorID = idIspolnitelya,
                            Title = TbxTitle.Text,
                            Description = TbxDescription.Text,
                            CreateDateTime = (DateTime)DtprCreateDateTime.SelectedDate,
                            Deadline = (DateTime)DtprDeadline.SelectedDate,
                            Difficulty = Convert.ToInt32(TbxDifficulty.Text),
                            Time = Convert.ToInt32(TbxTime.Text),
                            Status = CmbxStatus.Text,
                            WorkType = CmbxWorkType.Text,
                            IsDeleted = (bool)CkbxPriznakUdalennosti.IsChecked
                        };
                    }
                    else
                    {
                        zadacha = new FailiDannih.Task()
                        {
                            ExecutorID = idIspolnitelya,
                            Title = TbxTitle.Text,
                            Description = TbxDescription.Text,
                            CreateDateTime = (DateTime)DtprCreateDateTime.SelectedDate,
                            Deadline = (DateTime)DtprDeadline.SelectedDate,
                            Difficulty = Convert.ToInt32(TbxDifficulty.Text),
                            Time = Convert.ToInt32(TbxTime.Text),
                            Status = CmbxStatus.Text,
                            WorkType = CmbxWorkType.Text,
                            CompletedDateTime = Convert.ToDateTime(dataVipolneniya),
                            IsDeleted = (bool)CkbxPriznakUdalennosti.IsChecked
                        };
                    }
                    

                    PodclucheniyeOdb.podcluchObj.Task.Add(zadacha);
                    PodclucheniyeOdb.podcluchObj.SaveChanges();
                    MessageBox.Show("Данные успешно добавлены!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception er)
                {
                    MessageBox.Show(er.Message.ToString());
                }
            }

        }
    }
}
