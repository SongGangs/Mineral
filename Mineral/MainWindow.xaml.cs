using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        private ObservableCollection<CDisplay> displays = null;

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
            OrginMineralDs.Tables.Add(OrginHeteMineralDt.Copy());
            OrginMineralDs.Tables.Add(OrginHomoMineralDt.Copy());
            InitDataGridByDataSet(OrginMineralDs);
            //HeterogeneousMineralInfo heterogeneousMineral=new HeterogeneousMineralInfo(0,"矿物","矿物","矿物","矿物","矿物","矿物","矿物","矿物","矿物",(float)0.52,"矿物","矿物","矿物","矿物","矿物","矿物","矿物","矿物");
            //AccessDb.Add(heterogeneousMineral);
        }
        /// <summary>
        /// 通过DataSet将DataGrid初始化
        /// </summary>
        /// <param name="ds"></param>
        private void InitDataGridByDataSet(DataSet ds)
        {
            displays = new ObservableCollection<CDisplay>();
            for (int i = 0; i < ds.Tables.Count; i++)
            {
                for (int j = 0; j < ds.Tables[i].Rows.Count; j++)
                {
                    displays.Add(new CDisplay()
                    {
                        ChineseName = ds.Tables[i].Rows[j]["ChineseName"].ToString()
                    });
                }
            }
            this.DataGrid.ItemsSource = displays;
        }
        /// <summary>
        /// 通过实体将文本框初始化
        /// </summary>
        /// <param name="mineral"></param>
        private void InitTextByEntity(IMineral mineral)
        {
            if (mineral.mineralType == 1)//若是均质矿物
            {
                HomogeneousMineralInfo homogeneousMineral = (HomogeneousMineralInfo)mineral;
                this.Txt_ChineseName.Text = homogeneousMineral.ChineseName;
                this.Txt_EnglishName.Text = homogeneousMineral.EnglishName;
                this.Txt_ChemicalFormula.Text = homogeneousMineral.ChemicalFormula;
                this.Txt_Syngony.Text = homogeneousMineral.Syngony;
                this.Txt_NonUniformity.Text = homogeneousMineral.NonUniformity;
                this.Txt_Reflectivity.Text = homogeneousMineral.Reflectivity;
                this.Txt_Hardness.Text = homogeneousMineral.Hardness;
                this.Txt_ReflectionColor.Text = homogeneousMineral.ReflectionColor;
                this.Txt_Rr.Text = homogeneousMineral.Rr + "°";
                this.Txt_DRr.Text = homogeneousMineral.DRr;
                this.Txt_InternalReflection.Text = homogeneousMineral.InternalReflection;
                this.Txt_Origin.Text = homogeneousMineral.Origin;
                this.Txt_IMK.Text = homogeneousMineral.IMK;
            }
            else if (mineral.mineralType == 2)//若是非均质矿物
            {
                HeterogeneousMineralInfo heterogeneousMineral = (HeterogeneousMineralInfo)mineral;
                this.Txt_ChineseName_No.Text = heterogeneousMineral.ChineseName;
                this.Txt_EnglishName_No.Text = heterogeneousMineral.EnglishName;
                this.Txt_ChemicalFormula_No.Text = heterogeneousMineral.ChemicalFormula;
                this.Txt_Syngony_No.Text = heterogeneousMineral.Syngony;
                this.Txt_NonUniformity_No.Text = heterogeneousMineral.NonUniformity;
                this.Txt_Reflectivity_No.Text = heterogeneousMineral.Reflectivity;
                this.Txt_Hardness_No.Text = heterogeneousMineral.Hardness;
                this.Txt_ReflectionColor_No.Text = heterogeneousMineral.ReflectionColor;
                this.Txt_Bireflection_No.Text = heterogeneousMineral.Bireflection;
                this.Txt_Ar_No.Text = heterogeneousMineral.Ar + "°";
                this.Txt_DAr_No.Text = heterogeneousMineral.DAr;
                this.Txt_Rs_No.Text = heterogeneousMineral.Rs;
                this.Txt_Ps_No.Text = heterogeneousMineral.Ps;
                this.Txt_DRr_No.Text = heterogeneousMineral.DRr;
                this.Txt_ReflectionDAR_No.Text = heterogeneousMineral.ReflectionDAR;
                this.Txt_InternalReflection_No.Text = heterogeneousMineral.InternalReflection;
                this.Txt_Origin_No.Text = heterogeneousMineral.Origin;
                this.Txt_IMK_No.Text = heterogeneousMineral.IMK;
            }
        }
        private void Btn_ExportToDB_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Excel2007(*.xlsx)|*.xlsx|Excel2003(*.xls)|*.xls";
            dlg.ShowDialog();
            if (!string.IsNullOrEmpty(dlg.FileName))
            {
                MessageBox.Show(ExcelHelper.ExportDataToAccess(dlg.FileName, OrginMineralDs));
            }

        }
    }
}
