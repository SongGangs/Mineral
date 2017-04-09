using System;
using System.Collections.Generic;
using System.Data;
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
using Microsoft.Win32;
using Mineral.Common;
using Mineral.Helper;

namespace Mineral
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private DataTable OrginHomoMineralDt = null;//原生均质矿物的表
        private DataTable OrginHeteMineralDt = null;//原生非均质矿物的表
        private DataSet OrginMineralDs = new DataSet();//原生矿物的表集
        public MainWindow()
        {
            InitializeComponent();
        }

        private void showItemBtn_Click(object sender, RoutedEventArgs e)
        {
            if (this.StackPanel_Item.IsVisible)
            {
                this.StackPanel_Item.Visibility = Visibility.Hidden;
            }
            else
            {
                this.StackPanel_Item.Visibility = Visibility.Visible;
            }
        }

        private void showJunZhiBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Border_Home.Visibility = Visibility.Hidden;
            this.StackPanel_Item.Visibility = Visibility.Hidden;
            this.Border_JunZhi.Visibility = Visibility.Visible;
        }

        private void showNoJunZhiBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Border_Home.Visibility = Visibility.Hidden;
            this.StackPanel_Item.Visibility = Visibility.Hidden;
            this.Border_NoJunZhi.Visibility = Visibility.Visible;

        }

        private void Btn_Forword_Click(object sender, RoutedEventArgs e)
        {
            this.Border_NoJunZhi.Visibility = Visibility.Hidden;
            this.Border_JunZhi.Visibility = Visibility.Hidden;
            this.Border_Home.Visibility = Visibility.Visible;

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            OrginHeteMineralDt = AccessDb.Query(new HeterogeneousMineralInfo());
            OrginHomoMineralDt = AccessDb.Query(new HomogeneousMineralInfo());
            OrginHeteMineralDt.TableName = "非均质矿物";
            OrginHomoMineralDt.TableName = "均质矿物";
            OrginMineralDs.Tables.Add(OrginHeteMineralDt.Clone());
            OrginMineralDs.Tables.Add(OrginHomoMineralDt.Clone());
            //HeterogeneousMineralInfo heterogeneousMineral=new HeterogeneousMineralInfo(0,"矿物","矿物","矿物","矿物","矿物","矿物","矿物","矿物","矿物",(float)0.52,"矿物","矿物","矿物","矿物","矿物","矿物","矿物","矿物");
            //AccessDb.Add(heterogeneousMineral);
        }

        private void Btn_ExportToDB_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Excel2007(*.xlsx)|*.xlsx|Excel2003(*.xls)|*.xls";
            dlg.ShowDialog();
            if (!string.IsNullOrEmpty(dlg.FileName))
            {
                Console.WriteLine("....");
                MessageBox.Show(ExcelHelper.ExportDataToAccess(dlg.FileName, OrginMineralDs));
            }
            
        }
    }
}
