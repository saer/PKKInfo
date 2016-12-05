using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Логика взаимодействия для PropertyVisualizer.xaml
    /// </summary>
    public partial class PropertyVisualizer : UserControl, INotifyPropertyChanged
    {
        private string visualizerName = String.Empty;
        private object visualizerValue = String.Empty;
        private string visualizerFormat = String.Empty;

        public string VisualizerName
        {
            get { return visualizerName; }
            set
            {
                visualizerName = value;
                RaisePropertyChanged("VisualizerName");
            }
        }

        public object VisualizerValue
        {
            get { return visualizerValue; }
            set
            {
                visualizerValue = (value != null) ? (value) : ("Данные отсутствуют");
                RaisePropertyChanged("VisualizerValue");
                RaisePropertyChanged("VisualizerFormattedValue");
            }
        }

        public string VisualizerFormat
        {
            get { return visualizerFormat; }
            set
            {
                visualizerFormat = value;
                RaisePropertyChanged("VisualizerFormat");
                RaisePropertyChanged("VisualizerFormattedValue");
            }
        }

        public string VisualizerFormattedValue
        {
            get
            {
                if (!String.IsNullOrEmpty(VisualizerFormat))
                    return String.Format(VisualizerFormat, VisualizerValue);

                return VisualizerValue.ToString();
            }
        }

        public PropertyVisualizer()
        {
            InitializeComponent();
            VisualizerName = "NULL NAME";
            VisualizerValue = "NULL VALUE";
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
