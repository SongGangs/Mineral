using Microsoft.Win32;
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
using System.Windows.Shapes;

namespace Mineral
{
    /// <summary>
    /// MediaWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MediaWindow : Window
    {
        public MediaWindow()
        {
            InitializeComponent();
        }

        private void Btn_ChoseFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog mediaFileDialog = new OpenFileDialog();
           // mediaFileDialog.Filter="|*.";
            mediaFileDialog.ShowDialog();
            if (!String.IsNullOrEmpty(mediaFileDialog.FileName))
            {
                mediaElement.Source = new Uri(mediaFileDialog.FileName,UriKind.Relative);
                mediaElement.Play();
            }
        }
    }
}
