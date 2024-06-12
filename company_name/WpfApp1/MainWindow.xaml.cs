using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
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
using static WpfApp1.MainWindow;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public class EventForGrid
        {
            public object Name { get; set; }
            public object Direction { get; set; }
            public object Date { get; set; }
            public object Img { get; set; }

        }
        public void Load()
        {
            /*var eventsList = App.DB.events.ToList();*/
            List<EventForGrid> eventsList = new List<EventForGrid>();
            foreach (var i in App.DB.events)
            {
                string directionName = "";
                string imgPath = "";
                foreach (var j in App.DB.directions)
                {
                    if (j.directions_id == i.events_direction)
                    {
                        directionName = j.directions_name;
                    }
                }
                if (i.events_img == null)
                {
                    imgPath = "\\Resourses\\edit.png";
                }
                else
                {
                    imgPath = "\\Resourses\\" + i.events_img;
                }
                eventsList.Add(new EventForGrid { Name = i.events_name, Direction = directionName, Date = i.events_date.ToString("dd/MM/yy"), Img= imgPath });
            }
           

            EventsDataGrid.ItemsSource = eventsList;
        }
        public MainWindow()
        {
            InitializeComponent();
            Load();
        }



        private void testButton_Click(object sender, RoutedEventArgs e)
        {
            events eve = new events
            {
                events_name = "Тестовое название мероприятия",
                events_date = new DateTime(2024, 6, 12),
                events_days=3,
                events_city=34,
                events_winner=null
            };

            App.DB.events.Add(eve);
            App.DB.SaveChanges();

            var data=App.DB.events.ToList();
            string name ="";
            foreach(var i in data)
            {
                name += i.events_name.ToString() + "\n";
            }
            MessageBox.Show(name);
        }

        private void companyNameWindow_Loaded(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Maximized;
        }

        private void DirectionComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
