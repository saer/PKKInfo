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
            AddVisualizer("Кадастровый номер", data.feature.attrs.cn);
            AddVisualizer("Адрес", data.feature.attrs.address);
            AddVisualizer("Кадастровая стоимость", data.feature.attrs.cad_cost, "{0:0.00, руб\\.}");

            var cadEng = data.feature.attrs.cad_eng_data;
            if (cadEng != null)
            {
                string cadEngStr = string.Empty;

                if (cadEng.co_name != null)
                {
                    cadEngStr = cadEng.co_name;
                }
                else if (cadEng.ci_first != null)
                {
                    cadEngStr = String.Format("{0} {1} {2}, аттестат № {3}", cadEng.ci_surname, cadEng.ci_first, cadEng.ci_patronymic, cadEng.ci_n_certificate);
                }

                if (!String.IsNullOrEmpty(cadEngStr))
                    AddVisualizer("Кадастровый инженер", cadEngStr);
            }
        }

        public void AddVisualizer(string name, object value, string format = null)
        {
            if (CentralStack.Children.Count > 0)
            {
                Border brd = new Border();
                brd.Width = 100;
                brd.BorderThickness = new Thickness(1, 1, 1, 1);
                brd.HorizontalAlignment = HorizontalAlignment.Center;
                brd.BorderBrush = new SolidColorBrush(Color.FromRgb(255, 0, 0));
                CentralStack.Children.Add(brd);
            }

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
