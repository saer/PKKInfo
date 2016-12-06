using RestSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace PKKInfo
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private RestClient restClient;

        public MainWindow()
        {
            InitializeComponent();
            restClient = new RestClient("http://pkk5.rosreestr.ru/api");
        }

        public void OnNewDataReceived(PKKObjectParcel data)
        {
            if (data == null)
            {
                AddVisualizer("Ошибка", "Сервис не вернул данных");
                return;
            }

            if (!String.IsNullOrEmpty(data.note))
            {
                AddVisualizer("Ошибка", $"Сервис вернул ошибку. Код: {data.status}. Сообщение:{data.note}");
                return;
            }


            if (data.feature == null)
            {
                AddVisualizer("Ошибка", "Объект не найден");
                return;
            }


            ParcelData p = new ParcelData(data);
            AddVisualizer("Кадастровый номер", p.CadastralNumber);
            AddVisualizer("Статус", p.Status);
            AddVisualizer("Адрес", p.Address);
            AddVisualizer("Площадь", p.Area, "{0} " + p.AreaUnit);
            AddVisualizer("Кадастровая стоимость", p.CadastralCost, "{0} "+ p.CadastralCostUnit);
            AddVisualizer("Разрешенное использование (справочник)", p.UtilityByDict);
            AddVisualizer("Разрешенное использование (документ)", p.UtilityByDoc);
            AddVisualizer("Кадастровый инженер", p.CadastralEngineer);
            AddVisualizer("Дата постановки на учет", p.CadastralRegDate);
        }

        public void AddVisualizer(string name, object value, string format = null)
        {
            PropertyVisualizer vis = new PropertyVisualizer();
            vis.VisualizerName = name;
            vis.VisualizerValue = value;
            vis.BorderThickness = new Thickness(1, 1, 1, 1);

            if (format != null)
                vis.VisualizerFormat = format;

            CentralStack.Children.Add(vis);
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string cn = Helpers.HardClearCadastralNumber(MainTextBox.Text);
            MainTextBox.Text = cn;

            var request = new RestRequest("features/{type}/{id}");
            request.JsonSerializer = new CustomJsonSerializer();
            request.AddUrlSegment("type", "1");
            request.AddUrlSegment("id", Helpers.CompressCadastralNumber(cn));

            CentralStack.Children.Clear();

            restClient.ExecuteAsync<PKKObjectParcel>(request, response =>
            {
                Dispatcher.Invoke((Action)delegate ()
                {
                    OnNewDataReceived(response.Data);
                });
            });
        }
    }
}
