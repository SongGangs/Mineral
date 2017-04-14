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
using System.IO;

namespace Mineral
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private ObservableCollection<IMineral> OrginHomoMinerals = null; //初始均质矿物集合
        private ObservableCollection<IMineral> OrginHeteMinerals = null; //初始非均质矿物集合
        private ObservableCollection<IMineral> OrginMinerals = null; //初始矿物集合
        private IMineral curMineral = null; //当前显示的矿物
        private DataTable OrginHomoMineralDt = null; //初始均质矿物的表
        private DataTable OrginHeteMineralDt = null; //初始非均质矿物的表
        private DataSet OrginMineralDs = new DataSet(); //初始矿物的表集
        private int Viewflag = 0; //0表示主页 1表示均质矿物显示页面 2表示非均质矿物显示页面

        private Dictionary<string, string> paramters_Hete = new Dictionary<string, string>();
            //保存查询的参数Dictionary<类型, 实际值>---非均质

        private Dictionary<string, string> paramters_Hoto = new Dictionary<string, string>();
            //保存查询的参数Dictionary<类型, 实际值>---均质

        private bool IsAdd = false;
        private int modifyNum = 0;

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

        private void showHomoBtn_Click(object sender, RoutedEventArgs e)
        {
            UpdateCollections();
            InitDataGridByCollection(OrginHomoMinerals);
            FillTextProperty();
            ShowHomoMineralInfoView();
        }

        private void showHeteBtn_Click(object sender, RoutedEventArgs e)
        {
            UpdateCollections();
            InitDataGridByCollection(OrginHeteMinerals);
            FillTextProperty();
            ShowHeteMineralInfoView();
        }

        private void Btn_Forword_Click(object sender, RoutedEventArgs e)
        {
            ControlHelper.FindVisualChildItem<TextBox>(this.Txt_QueryByName, "Txt_QueryByName").Text = String.Empty;
            UpdateCollections();
            InitDataGridByCollection(OrginMinerals);
            ShowHomeView();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            InitDataTable();
            UpdateCollections();
            InitDataGridByCollection(OrginMinerals);
            //HeterogeneousMineralInfo heterogeneousMineral=new HeterogeneousMineralInfo(0,"矿物","矿物","矿物","矿物","矿物","矿物","矿物","矿物","矿物",(float)0.52,"矿物","矿物","矿物","矿物","矿物","矿物","矿物","矿物");
            //AccessDb.Add(heterogeneousMineral);
        }

        /// <summary>
        /// 初始化所以表
        /// </summary>
        private void InitDataTable()
        {
            OrginHeteMineralDt = new DataTable();
            OrginHomoMineralDt = new DataTable();
            OrginHeteMineralDt = AccessDB.Query(new HeterogeneousMineralInfo());
            OrginHomoMineralDt = AccessDB.Query(new HomogeneousMineralInfo());
            OrginHeteMineralDt.TableName = "非均质矿物";
            OrginHomoMineralDt.TableName = "均质矿物";
            OrginMineralDs = new DataSet();
            OrginMineralDs.Tables.Add(OrginHeteMineralDt.Copy());
            OrginMineralDs.Tables.Add(OrginHomoMineralDt.Copy());
        }

        /// <summary>
        /// 通过DataSet将DataGrid填充
        /// </summary>
        /// <param name="ds"></param>
        private void InitDataGridByCollection(ObservableCollection<IMineral> myCollectionminerals)
        {
            
            this.DataGrid.ItemsSource = myCollectionminerals;
            
        }

        /// <summary>
        /// 根据DataRow把他变为实体
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        private IMineral FillInfoByDataRow(DataRow dr, int type)
        {
            if (type == 1)
            {
                return new HomogeneousMineralInfo()
                {
                    ID = int.Parse(dr["ID"].ToString()),
                    ChineseName = dr["ChineseName"].ToString(),
                    EnglishName = dr["EnglishName"].ToString(),
                    ChemicalFormula = dr["ChemicalFormula"].ToString(),
                    Syngony = dr["Syngony"].ToString(),
                    NonUniformity = dr["NonUniformity"].ToString(),
                    Reflectivity = dr["Reflectivity"].ToString(),
                    Hardness = dr["Hardness"].ToString(),
                    ReflectionColor = dr["ReflectionColor"].ToString(),
                    Rr = float.Parse(dr["Rr"].ToString()),
                    DRr = dr["DRr"].ToString(),
                    InternalReflection = dr["InternalReflection"].ToString(),
                    Origin = dr["Origin"].ToString(),
                    IMK = dr["IMK"].ToString()
                };
            }
            else
            {
                return new HeterogeneousMineralInfo()
                {
                    ID = int.Parse(dr["ID"].ToString()),
                    ChineseName = dr["ChineseName"].ToString(),
                    EnglishName = dr["EnglishName"].ToString(),
                    ChemicalFormula = dr["ChemicalFormula"].ToString(),
                    Syngony = dr["Syngony"].ToString(),
                    NonUniformity = dr["NonUniformity"].ToString(),
                    Reflectivity = dr["Reflectivity"].ToString(),
                    Hardness = dr["Hardness"].ToString(),
                    ReflectionColor = dr["ReflectionColor"].ToString(),
                    Bireflection = dr["Bireflection"].ToString(),
                    Ar = float.Parse(dr["Ar"].ToString()),
                    DAr = dr["DAr"].ToString(),
                    Rs = dr["Rs"].ToString(),
                    Ps = dr["Ps"].ToString(),
                    DRr = dr["DRr"].ToString(),
                    ReflectionDAR = dr["ReflectionDAR"].ToString(),
                    InternalReflection = dr["InternalReflection"].ToString(),
                    Origin = dr["Origin"].ToString(),
                    IMK = dr["IMK"].ToString(),
                };
            }

        }

        /// <summary>
        /// 通过文本将实体填充
        /// </summary>
        /// <param name="type"></param>
        private void FillEntityByTextBox(int type)
        {
            if (type == 1)
            {
                HomogeneousMineralInfo homo = curMineral as HomogeneousMineralInfo;
                homo.ID = 0;
                homo.ChineseName = this.Txt_ChineseName.Text.Trim();
                homo.EnglishName = this.Txt_EnglishName.Text.Trim();
                homo.ChemicalFormula = this.Txt_ChemicalFormula.Text.Trim();
                homo.Syngony = this.Txt_Syngony.Text.Trim();
                homo.NonUniformity = this.Txt_NonUniformity.Text.Trim();
                homo.Reflectivity = this.Txt_Reflectivity.Text.Trim();
                homo.Hardness = this.Txt_Hardness.Text.Trim();
                homo.ReflectionColor = this.Txt_ReflectionColor.Text.Trim();
                if (!string.IsNullOrEmpty(this.Txt_Rr.Text.Trim()))
                {
                    homo.Rr = float.Parse(this.Txt_Rr.Text.Trim().Replace("°", null));
                }
                homo.DRr = this.Txt_DRr.Text.Trim();
                homo.InternalReflection = this.Txt_InternalReflection.Text.Trim();
                homo.Origin = this.Txt_Origin.Text.Trim();
                homo.IMK = this.Txt_IMK.Text.Trim();
                curMineral = homo;
            }
            else
            {
                HeterogeneousMineralInfo hete = curMineral as HeterogeneousMineralInfo;
                hete.ID = 0;
                hete.ChineseName = this.Txt_ChineseName_No.Text.Trim();
                hete.EnglishName = this.Txt_EnglishName_No.Text.Trim();
                hete.ChemicalFormula = this.Txt_ChemicalFormula_No.Text.Trim();
                hete.Syngony = this.Txt_Syngony_No.Text.Trim();
                hete.NonUniformity = this.Txt_NonUniformity_No.Text.Trim();
                hete.Reflectivity = this.Txt_Reflectivity_No.Text.Trim();
                hete.Hardness = this.Txt_Hardness_No.Text.Trim();
                hete.ReflectionColor = this.Txt_ReflectionColor_No.Text.Trim();
                hete.Bireflection = this.Txt_ReflectionColor_No.Text.Trim();
                if (!string.IsNullOrEmpty(this.Txt_Ar_No.Text.Trim()))
                {
                    hete.Ar = float.Parse(this.Txt_Ar_No.Text.Trim().Replace("°", null));
                }
                hete.DAr = this.Txt_DAr_No.Text.Trim();
                hete.Rs = this.Txt_Rs_No.Text.Trim();
                hete.Ps = this.Txt_Ps_No.Text.Trim();
                hete.DRr = this.Txt_DRr_No.Text.Trim();
                hete.ReflectionDAR = this.Txt_ReflectionDAR_No.Text.Trim();
                hete.InternalReflection = this.Txt_InternalReflection_No.Text.Trim();
                hete.Origin = this.Txt_Origin_No.Text.Trim();
                hete.IMK = this.Txt_IMK_No.Text.Trim();
                curMineral = hete;
            }

        }

        /// <summary>
        /// 通过实体将文本框填充
        /// </summary>
        /// <param name="mineral"></param>
        private void InitTextByEntity(IMineral mineral)
        {
            curMineral = mineral;
            if (mineral.mineralType == 1) //若是均质矿物
            {
                HomogeneousMineralInfo homogeneousMineral = (HomogeneousMineralInfo) mineral;
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
                ShowHomoMineralInfoView();
            }
            else if (mineral.mineralType == 2) //若是非均质矿物
            {
                HeterogeneousMineralInfo heterogeneousMineral = (HeterogeneousMineralInfo) mineral;
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
                ShowHeteMineralInfoView();
            }
        }

        /// <summary>
        /// 清空展示栏中的文本信息
        /// </summary>
        /// <returns></returns>
        private void ClealTextProperty()
        {
            #region  均质控件

            this.Txt_ChineseName.Text = String.Empty;
            this.Txt_EnglishName.Text = String.Empty;
            this.Txt_ChemicalFormula.Text = String.Empty;
            this.Txt_Syngony.Text = String.Empty;
            this.Txt_NonUniformity.Text = String.Empty;
            this.Txt_Reflectivity.Text = String.Empty;
            this.Txt_Hardness.Text = String.Empty;
            this.Txt_ReflectionColor.Text = String.Empty;
            this.Txt_Rr.Text = String.Empty;
            this.Txt_DRr.Text = String.Empty;
            this.Txt_InternalReflection.Text = String.Empty;
            this.Txt_Origin.Text = String.Empty;
            this.Txt_IMK.Text = String.Empty;

            #endregion

            #region  非均质控件


            this.Txt_ChineseName_No.Text = String.Empty;
            this.Txt_EnglishName_No.Text = String.Empty;
            this.Txt_ChemicalFormula_No.Text = String.Empty;
            this.Txt_Syngony_No.Text = String.Empty;
            this.Txt_NonUniformity_No.Text = String.Empty;
            this.Txt_Reflectivity_No.Text = String.Empty;
            this.Txt_Hardness_No.Text = String.Empty;
            this.Txt_ReflectionColor_No.Text = String.Empty;
            this.Txt_Bireflection_No.Text = String.Empty;
            this.Txt_Ar_No.Text = String.Empty;
            this.Txt_DAr_No.Text = String.Empty;
            this.Txt_Rs_No.Text = String.Empty;
            this.Txt_Ps_No.Text = String.Empty;
            this.Txt_DRr_No.Text = String.Empty;
            this.Txt_ReflectionDAR_No.Text = String.Empty;
            this.Txt_InternalReflection_No.Text = String.Empty;
            this.Txt_Origin_No.Text = String.Empty;
            this.Txt_IMK_No.Text = String.Empty;

            #endregion
        }

        /// <summary>
        /// 初始化检索栏信息
        /// </summary>
        /// <returns></returns>
        private void ClealComboBoxProperty()
        {
            paramters_Hoto.Clear();
            paramters_Hete.Clear();
            #region  均质控件
            this.ComboBox_DAR.SelectedIndex = 0;
            this.ComboBox_InternalReflection.SelectedIndex = 0;
            this.ComboBox_ReflectionColor.SelectedIndex = 0;
            this.ComboBox_ReflectivityClassify.SelectedIndex = 0;
            this.ComboBox_ScratchHardness.SelectedIndex = 0;
            this.Txt_VisualReflectivity.Text = String.Empty;
            this.Txt_VickersHardness.Text = String.Empty;
            this.Txt_Rr1.Text = String.Empty;


            #endregion

            #region  非均质控件

            this.ComboBox_Bireflection_no.SelectedIndex = 0;
            this.ComboBox_DAR_no.SelectedIndex = 0;
            this.ComboBox_DAr_no.SelectedIndex = 0;
            this.ComboBox_Heterogeneity_no.SelectedIndex = 0;
            this.ComboBox_InternalReflection_no.SelectedIndex = 0;
            this.ComboBox_MultipleDAR_no.SelectedIndex = 0;
            this.ComboBox_Ps_no.SelectedIndex = 0;
            this.ComboBox_ReflectionColor_no.SelectedIndex = 0;
            this.ComboBox_ReflectivityClassify_no.SelectedIndex = 0;
            this.ComboBox_Rs_no.SelectedIndex = 0;
            this.ComboBox_ScratchHardness_no.SelectedIndex = 0;
            this.Txt_VickersHardness_no.Text = String.Empty;
            this.Txt_LongAxis_no.Text = String.Empty;
            this.Txt_ShortAxis_no.Text = String.Empty;
            this.Txt_Ar_no.Text = String.Empty;

            #endregion
        }

        private void UpdateCollections()
        {
            OrginMinerals = new ObservableCollection<IMineral>();
            OrginHeteMinerals = new ObservableCollection<IMineral>();
            OrginHomoMinerals = new ObservableCollection<IMineral>();

            for (int i = 0; i < OrginMineralDs.Tables.Count; i++)
            {
                for (int j = 0; j < OrginMineralDs.Tables[i].Rows.Count; j++)
                {
                    if (OrginMineralDs.Tables[i].TableName.Contains("非均质矿物"))
                    {
                        HeterogeneousMineralInfo hete =
                            (HeterogeneousMineralInfo)FillInfoByDataRow(OrginMineralDs.Tables[i].Rows[j], 2);
                        OrginMinerals.Add(hete);
                        OrginHeteMinerals.Add(hete);
                    }
                    else
                    {
                        HomogeneousMineralInfo homo =
                            (HomogeneousMineralInfo)FillInfoByDataRow(OrginMineralDs.Tables[i].Rows[j], 1);
                        OrginMinerals.Add(homo);
                        OrginHomoMinerals.Add(homo);
                    }
                }
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
            InitDataTable();
            UpdateCollections();
            InitDataGridByCollection(OrginMinerals);
        }

        private void DataGrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            if (this.DataGrid.CurrentItem == null)
            {
                return;
            }
            //判断选择的是行数是属于非均质还是均质
            if (this.DataGrid.CurrentItem.GetType().Name.Equals("HomogeneousMineralInfo"))
            {
                curMineral = this.DataGrid.CurrentItem as HomogeneousMineralInfo;
                InitTextByEntity(curMineral);
            }
            else if (this.DataGrid.CurrentItem.GetType().Name.Equals("HeterogeneousMineralInfo"))
            {
                curMineral = this.DataGrid.CurrentItem as HeterogeneousMineralInfo;
                InitTextByEntity(curMineral);
            }
        }

        #region 视图切换显示 
        /// <summary>
        /// 显示非均质矿物信息视图
       /// </summary>
        private void ShowHeteMineralInfoView()
        {
            Viewflag = 2;
            this.Border_Home.Visibility = Visibility.Hidden;
            this.StackPanel_Item.Visibility = Visibility.Hidden;
            this.Border_Homo.Visibility = Visibility.Hidden;
            this.Border_Hete.Visibility = Visibility.Visible;
        }
        /// <summary>
        /// 显示均质矿物信息视图
        /// </summary>
        private void ShowHomoMineralInfoView()
        {
            Viewflag = 1;
            this.Border_Home.Visibility = Visibility.Hidden;
            this.StackPanel_Item.Visibility = Visibility.Hidden;
            this.Border_Hete.Visibility = Visibility.Hidden;
            this.Border_Homo.Visibility = Visibility.Visible;
        }
        /// <summary>
        /// 显示主页视图
        /// </summary>
        private void ShowHomeView()
        {
            Viewflag = 0;
            this.StackPanel_Item.Visibility = Visibility.Hidden;
            this.Border_Hete.Visibility = Visibility.Hidden;
            this.Border_Homo.Visibility = Visibility.Hidden;
            this.Border_Home.Visibility = Visibility.Visible;
        }
        #endregion

   

        /// <summary>
        /// 
        /// </summary>
        /// <param name="inputValue"></param>
        /// <param name="orginValue"></param>
        /// <param name="roat">上下浮动数</param>
        /// <returns></returns>
        private bool CompareVaule(string inputValue, string orginValue,double roat)
        {
            try
            {
                double maxValue = float.Parse(inputValue) * (1 + roat);
                double minValue = float.Parse(inputValue) * (1 - roat);
                double value = double.Parse(orginValue);
                if (minValue > value || maxValue < value)
                    return false;
                else
                    return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        /// <summary>
        /// 通过参数查询
        /// </summary>
        /// <param name="param"></param>
        /// <param name="myMinerals"></param>
        private void QueryByParamters(Dictionary<string, string> param, ObservableCollection<IMineral> myMinerals)
        {
            ObservableCollection<IMineral>list=new ObservableCollection<IMineral>();
            if (param.Count != 0)
            {
                int paramsNum = 0; //判断参数是否用完
                this.DataGrid.ItemsSource = null;//注意！！！！必须先删掉ItemsSource 才能修改Items
                this.DataGrid.Items.Clear();
                foreach (IMineral mineral in myMinerals)
                {
                    foreach (var item in param)
                    {
                        paramsNum++;
                        if (mineral.mineralType==1)//表示均质矿物
                        {
                           Dictionary<String, Object> map =MapHelper.ToMap(mineral as HomogeneousMineralInfo);
                           if (item.Key == "Reflectivity2" || item.Key == "Hardness2" || item.Key == "Rr")
                            {
                                if (item.Key == "Hardness2" && !string.IsNullOrEmpty(item.Value))
                                {
                                    if (string.IsNullOrEmpty(map["Hardness"].ToString()))
                                        break;
                                    string str = map[item.Key.Replace("2", null)].ToString();
                                    str = str.Substring(str.IndexOf("维氏硬度为") + 5,
                                        str.IndexOf("，") - str.IndexOf("维氏硬度为") - 5);
                                    if (!CompareVaule(item.Value, str, 0.02))
                                        break;
                                }
                                else if (item.Key == "Rr" && !string.IsNullOrEmpty(item.Value))
                                {
                                    if (!CompareVaule(item.Value, map[item.Key].ToString().Replace("°",null), 0.05))
                                        break;
                                }
                                else if (!map[item.Key.Replace("2", null)].ToString().Contains(item.Value) && !string.IsNullOrEmpty(item.Value))
                                {
                                        break;
                                }
                                if (paramsNum == param.Count)
                                {
                                    list.Add(mineral as HomogeneousMineralInfo);
                                }
                            }
                            else
                            {
                                if (!map[item.Key].ToString().Contains(item.Value))
                                {

                                    if (item.Key != "EnglishName" )
                                        break;
                                }
                                else
                                {
                                    if (item.Key == "EnglishName" && param.ContainsKey("ChemicalFormula"))
                                    {
                                        param.Remove("ChemicalFormula");
                                    }
                                    if (paramsNum == param.Count)
                                    {
                                        list.Add(mineral as HomogeneousMineralInfo);
                                    }
                                }
                            }
                        }
                        else if (mineral.mineralType == 2)//表示非均质矿物
                        {
                            Dictionary<String, Object> map = MapHelper.ToMap(mineral as HeterogeneousMineralInfo);
                            if (item.Key == "Reflectivity2" || item.Key == "Reflectivity1" || item.Key == "Hardness2" || item.Key == "Ar")
                            {
                                if (!string.IsNullOrEmpty(item.Value))
                                {
                                    if (item.Key == "Hardness2" )
                                    {
                                        if (string.IsNullOrEmpty(map["Hardness"].ToString()))
                                            break;
                                        string str = map[item.Key.Replace("2", null)].ToString();
                                        str = str.Substring(str.IndexOf("维氏硬度为") + 5,
                                            str.IndexOf("，") - str.IndexOf("维氏硬度为") - 5);
                                        if (!CompareVaule(item.Value, str, 0.02))
                                            break;
                                    }
                                    else if (item.Key == "Ar" )
                                    {
                                        if (!CompareVaule(item.Value, map[item.Key].ToString().Replace("°", null), 0.05))
                                            break;
                                    }
                                    else if (item.Key == "Reflectivity1")
                                    {
                                        if (!map[item.Key.Remove(item.Key.Length - 1)].ToString().Contains("长轴为"+item.Value))
                                            break;
                                    }
                                    else if (item.Key == "Reflectivity2")
                                    {
                                        if (!map[item.Key.Remove(item.Key.Length - 1)].ToString().Contains("短轴为" + item.Value))
                                            break;
                                    } 
                                }
                                if (paramsNum == param.Count)
                                {
                                    list.Add(mineral as HeterogeneousMineralInfo);
                                }
                            }
                            else
                            {
                                if (!map[item.Key].ToString().Contains(item.Value))
                                {

                                    if (item.Key != "EnglishName")
                                        break;
                                }
                                else
                                {
                                    if (item.Key == "EnglishName" && param.ContainsKey("ChemicalFormula"))
                                    {
                                        param.Remove("ChemicalFormula");
                                    }
                                    if (paramsNum == param.Count)
                                    {
                                        list.Add(mineral as HeterogeneousMineralInfo);
                                    }
                                }
                            }
                        }
                      
                    }
                    paramsNum = 0;
                }
                this.DataGrid.ItemsSource = list;
                FillTextProperty();
            }
            else
            {
                this.DataGrid.ItemsSource = myMinerals;
                FillTextProperty();
            }
        }

        /// <summary>
        /// 根据DataGrid的首行填充
        /// </summary>
        private void FillTextProperty()
        {
            if (this.DataGrid.Items.Count > 0)
            {
                this.DataGrid.SelectedItem = this.DataGrid.CurrentItem = this.DataGrid.Items[0];
                InitTextByEntity(this.DataGrid.CurrentItem as IMineral);
            }
            else
            {
              ClealTextProperty();  
            }
        }

  

        private void ComboBox_DropDownClosed(object sender, EventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
              string key = comboBox.Tag.ToString();
            if (comboBox.SelectedIndex > 0)
            {
                string value = comboBox.Text.Trim().Replace("：", null);
                if (!paramters_Hete.ContainsKey(key))
                    {
                        paramters_Hete.Add(key, value);
                    }
                    else
                    {
                        paramters_Hete[key] = value;
                    }
            }
            else
            {
                paramters_Hete.Remove(key);
            }
            QueryByParamters(paramters_Hete, OrginHeteMinerals);
        }
        private void ComboBox_homo_DropDownClosed(object sender, EventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            string key = comboBox.Tag.ToString();
            if (comboBox.SelectedIndex > 0)
            {
                string value = comboBox.Text.Trim().Replace("：", null);
                if (!paramters_Hoto.ContainsKey(key))
                {
                    paramters_Hoto.Add(key, value);
                }
                else
                {
                    paramters_Hoto[key] = value;
                }
            }
            else
            {
                paramters_Hoto.Remove(key);
            }
            QueryByParamters(paramters_Hoto, OrginHomoMinerals);
        }

        private void Txt_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            string value = textBox.Text.Trim();
            string key = textBox.Tag.ToString();
            if (!paramters_Hete.ContainsKey(key))
            {
                paramters_Hete.Add(key, value);
            }
            else
            {
                paramters_Hete[key] = value;
            }
            QueryByParamters(paramters_Hete, OrginHeteMinerals);
        }

        private void Txt_homo_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            string value = textBox.Text.Trim();
            string key = textBox.Tag.ToString();
            if (!paramters_Hoto.ContainsKey(key))
            {
                paramters_Hoto.Add(key, value);
            }
            else
            {
                paramters_Hoto[key] = value;
            }
            QueryByParamters(paramters_Hoto, OrginHomoMinerals);
        }

        /// <summary>
        /// 向数据库添加一条记录
        /// </summary>
        /// <param name="dt">要添加的矿物所属表</param>
        /// <param name="chineseName">矿物名称 用来检测是否重名</param>
        /// <param name="mineral">要添加的矿物信息</param>
        /// <param name="type">决定更新DataGrid的参数</param>
        private void AddMineralToDB(DataTable dt, string chineseName, IMineral mineral, int type)
        {
            DataRow[] dataRow = dt.Select("ChineseName='" + chineseName + "'");
            if (dataRow.Length > 0)
            {
                MessageBox.Show("已存在  ‘" + chineseName + "’  矿物");
                modifyNum++;
            }
            else
            {
                //开始增加矿物 并更新当前内存中的信息
                AccessDB.Add(mineral);
                //DataGrid.ItemsSource正在使用时无法操作
                InitDataTable();
                UpdateCollections();
                if (type == 1)
                {
                    InitDataGridByCollection(OrginHomoMinerals);
                }
                else if (type == 2)
                {
                    InitDataGridByCollection(OrginHeteMinerals);
                }
                else
                {
                    InitDataGridByCollection(OrginMinerals);
                }
                modifyNum = 0;
            }
            IsAdd = false;
        }


        private void Btn_UpdateMineral_Click(object sender, RoutedEventArgs e)
        {
            if (curMineral==null)
            {
                return;
            }
            string firstName = curMineral.mineralType == 1
                ? (curMineral as HomogeneousMineralInfo).ChineseName
                : (curMineral as HeterogeneousMineralInfo).ChineseName;
            int id = curMineral.mineralType == 1
                ? (curMineral as HomogeneousMineralInfo).ID
                : (curMineral as HeterogeneousMineralInfo).ID;
            FillEntityByTextBox(Viewflag);
            if (Viewflag == 1)//表示是均质矿物
            { 
                string str = (curMineral as HomogeneousMineralInfo).ChineseName;
                if (!String.IsNullOrEmpty(str))
                {
                    if (IsAdd)
                    {
                        AddMineralToDB(OrginHomoMineralDt, str, curMineral,1);
                    }
                    else
                    {
                        if (modifyNum == 0)
                        {
                            (curMineral as HomogeneousMineralInfo).ID = id;
                            DataRow[] myr = OrginHomoMineralDt.Select("ChineseName='" + firstName + "'");
                            if (myr.Length > 0)
                            {
                                AccessDB.Update(curMineral);
                                InitDataTable();
                                UpdateCollections();
                                InitDataGridByCollection(OrginHomoMinerals);
                            }
                        }
                        else
                        {
                            AddMineralToDB(OrginHomoMineralDt, str, curMineral, 1);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("矿物名字不能为空哦");
                }
            }
            else if (Viewflag == 2)//表示是非均质矿物
            {
                string str = (curMineral as HeterogeneousMineralInfo).ChineseName;
                if (!String.IsNullOrEmpty(str))
                {
                    if (IsAdd)
                    {
                        AddMineralToDB(OrginHeteMineralDt,str,curMineral,2);
                    }
                    else
                    {
                        if (modifyNum == 0)
                        {
                            (curMineral as HeterogeneousMineralInfo).ID = id;
                            DataRow[] myr = OrginHeteMineralDt.Select("ChineseName='" + firstName + "'");
                            if (myr.Length > 0)
                            {
                                AccessDB.Update(curMineral);
                                InitDataTable();
                                UpdateCollections();
                                InitDataGridByCollection(OrginHeteMinerals);
                            }
                        }
                        else
                        {
                            AddMineralToDB(OrginHeteMineralDt, str, curMineral, 2);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("矿物名字不能为空哦");
                }
            }
        }
        private void Btn_AddMineral_Click(object sender, RoutedEventArgs e)
        {
            this.DataGrid.SelectedItem = null;
            ClealComboBoxProperty();
            ClealTextProperty();
            IsAdd = true;
            if (Viewflag == 1)
            {
                curMineral = new HomogeneousMineralInfo();
            }
            else if (Viewflag == 2)
            {
                curMineral = new HeterogeneousMineralInfo();
            }
            else
            {
                MessageBox.Show("请先确定添加的种类！");
            }
        }

        private void Btn_DelectMineral_Click(object sender, RoutedEventArgs e)
        {
            if (curMineral == null)
            {
                return;
            }
            if (Viewflag == 1) //表示是均质矿物
            {
                string name = (curMineral as HomogeneousMineralInfo).ChineseName;
                if (!String.IsNullOrEmpty(name))
                {
                    if (MessageBox.Show("确认删除  '" + name + "'  ?", "此删除不可恢复", MessageBoxButton.YesNo) ==
                        MessageBoxResult.Yes)
                    {
                        AccessDB.DeleteByEntity(curMineral);
                        InitDataTable();
                        UpdateCollections();
                        InitDataGridByCollection(OrginHomoMinerals);
                        FillTextProperty();
                    }
                }
            }
            else if (Viewflag == 2) //表示是非均质矿物
            {
                string name = (curMineral as HeterogeneousMineralInfo).ChineseName;
                if (!String.IsNullOrEmpty(name))
                {
                    if (MessageBox.Show("确认删除  '" + name + "'  ?", "此删除不可恢复", MessageBoxButton.YesNo) ==
                        MessageBoxResult.Yes)
                    {
                        AccessDB.DeleteByEntity(curMineral);
                        InitDataTable();
                        UpdateCollections();
                        InitDataGridByCollection(OrginHeteMinerals);
                        FillTextProperty();
                    }
                }
            }
            else
            {
                MessageBox.Show("请先确定删除的矿物！");
            }
        }

        private void Btn_ReSet_Click(object sender, RoutedEventArgs e)
        {
            ClealComboBoxProperty();
            if (Viewflag==1)
            {
                InitDataGridByCollection(OrginHomoMinerals);
                FillTextProperty();
            }
            else if (Viewflag == 2)
            {
                InitDataGridByCollection(OrginHeteMinerals);
                FillTextProperty();
            }
        }
       /// <summary>
       /// 判断字符串是否是由英文和数字组成
       /// </summary>
       /// <param name="str"></param>
       /// <returns></returns>
        public bool IsNatural_Number(string str)
        {
            System.Text.RegularExpressions.Regex reg1 = new System.Text.RegularExpressions.Regex(@"^[A-Za-z0-9]+$");
            return reg1.IsMatch(str);
        }

        private void Btn_Media_Click(object sender, RoutedEventArgs e)
        {
            MediaWindow mediawindow = new MediaWindow();
            mediawindow.Show();
        }

      

        private void FindByNameButton_Click(object sender, RoutedEventArgs e)
        {
            Dictionary<string, string> paramters = new Dictionary<string, string>();
            string name = ControlHelper.FindVisualChildItem<TextBox>(this.Txt_QueryByName, "Txt_QueryByName").Text.Trim();
            if (!String.IsNullOrEmpty(name))
            {
                if (IsNatural_Number(name))
                {
                    paramters.Add("EnglishName", name);
                    paramters.Add("ChemicalFormula", name);
                }
                else
                {
                    paramters.Add("ChineseName", name);
                }
                QueryByParamters(paramters, OrginMinerals);
                paramters.Remove("ChineseName");
                paramters.Remove("EnglishName");
                paramters.Remove("ChemicalFormula");
            }
            else
            {
                InitDataGridByCollection(OrginMinerals);
            }
        }

        private void DataGrid_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = e.Row.GetIndex() + 1;
        }
        /// <summary>
        /// 拷贝模板
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CopyTemplate_Click(object sender, RoutedEventArgs e)
        {
            string templateType = ((MenuItem)sender).Tag as string;
            string sourcePath = System.AppDomain.CurrentDomain.BaseDirectory + @"Data\Template\" + templateType;
            if (File.Exists(sourcePath))
            {
                string tatgetPath = @"C:\Users\Administrator\Desktop\" + templateType;
                FileInfo sourcefile = new FileInfo(sourcePath);
                if (!File.Exists(tatgetPath))
                {
                    sourcefile.CopyTo(tatgetPath);
                    MessageBox.Show("导出‘ "+templateType+" ’模板成功，在桌面上已经存在！");
                }
                else
                {
                    MessageBox.Show("‘ "+templateType+" ’在桌面上已经存在！");
                }
            }
            else
            {
                MessageBox.Show("不存在' "+templateType+" '模板");
            }
        }
    }
}
