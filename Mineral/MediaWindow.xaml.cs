using Microsoft.Win32;
using System;
using System.Windows;
using System.Windows.Threading;

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
        DispatcherTimer timer = null;
        private void Btn_ChoseFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog mediaFileDialog = new OpenFileDialog();
            mediaFileDialog.ShowDialog();
            if (!String.IsNullOrEmpty(mediaFileDialog.FileName))
            {
                mediaElement.Source = new Uri(mediaFileDialog.FileName, UriKind.Relative);
                mediaElement.Play();
            }
        }

        private void Btn_Stop_Click(object sender, RoutedEventArgs e)
        {
            mediaElement.Stop();
        }

        private void Btn_Play_Click(object sender, RoutedEventArgs e)
        {
            mediaElement.Play();
        }

        private void Btn_Pause_Click(object sender, RoutedEventArgs e)
        {
            mediaElement.Pause();
        }

        private void mediaElement_MediaOpened(object sender, RoutedEventArgs e)
        {
            sldProgress.Maximum = mediaElement.NaturalDuration.TimeSpan.TotalSeconds;
            MaxTime.Content = DoubleToTime(sldProgress.Maximum);
            //媒体文件打开成功
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += new EventHandler(timer_tick);
            timer.Start();
        }
        private void timer_tick(object sender, EventArgs e)
        {
            sldProgress.Value = mediaElement.Position.TotalSeconds;
        }
        public string DoubleToTime(double time)
        {
            int hour = 0;
            int minute = 0;
            int second = 0;
            second = Convert.ToInt32(time);
            if (second > 60)
            {
                minute = second / 60;
                second = second % 60;
            }
            if (minute > 60)
            {
                hour = minute / 60;
                minute = minute % 60;
            }
            return (hour + ":" + minute + ":" + second);
        }

        private void mediaElement_Loaded(object sender, RoutedEventArgs e)
        {
            //如何解决后缀名？
            try
            {
                if (!String.IsNullOrEmpty(MainWindow.MediaUrl))
                {
                    this.mediaElement.Source = new Uri(MainWindow.MediaUrl);
                    mediaElement.Play(); 
                }
            }
            catch (Exception)
            {
                //throw;
            }
        }
    }
}
