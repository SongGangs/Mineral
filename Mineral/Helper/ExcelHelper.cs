using System;
using System.Data;
using System.Data.OleDb;
using Mineral.Common;

namespace Mineral.Helper
{
    class ExcelHelper
    {
        /// <summary>
        /// 从EXCEL中获取数据
        /// </summary>
        /// <param name="filepath"></param>
        /// <returns></returns>
        private static DataTable GetDataByExcel(string filepath)
        {
            try
            {
                string strConn = String.Empty;
                OleDbConnectionStringBuilder connectStringBuilder = new OleDbConnectionStringBuilder();
                connectStringBuilder.DataSource = filepath;
                if (System.IO.Path.GetExtension(filepath).ToLower() == ".xls")
                {
                    //如果是07以下（.xls）的版本的Excel文件就使用这条连接字符串
                    strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filepath +
                              ";Extended Properties=Excel 8.0;";
                }
                else if (System.IO.Path.GetExtension(filepath).ToLower() == ".xlsx")
                {
                    //如果是07以上(.xlsx)的版本的Excel文件就使用这条连接字符串
                    strConn = "Provider=Microsoft.Ace.OleDb.12.0;" + "data source=" + filepath +
                              ";Extended Properties='Excel 12.0; HDR=NO; IMEX=1'";
                }
                OleDbConnection conn = new OleDbConnection(strConn);
                conn.Open();
                DataTable sheetNamesDt = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                string strSheetTableName = sheetNamesDt.Rows[0]["TABLE_NAME"].ToString();
                    
                //当前选中的工作表前几行数据，获取数据列
                OleDbDataAdapter oada = new OleDbDataAdapter("Select * from ["+strSheetTableName+"]", strConn);
                DataTable dt = new DataTable();
                dt.TableName = strSheetTableName.Replace("$",null);
                oada.Fill(dt);
                conn.Close();
                return UpdateDataTableByFiled(dt);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// 将DataTable的数据导入到数据库
        /// </summary>
        /// <param name="filepath"></param>
        /// <param name="dt"></param>
        public static string ExportDataToAccess(string filepath, DataSet mineralDataSet)
        {
            int successed = 0;
            int failed = 0;
            DataTable dt = GetDataByExcel(filepath);
            if (dt.Rows.Count > 0)
            {
                if (CheckIsRepeatInExcel(dt))
                {
                    return ("Excel中有重复的矿物，请检查后再来");
                }
                if (dt.TableName.Contains("非均质矿物"))
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DataRow[] dataRow =
                            mineralDataSet.Tables[dt.TableName].Select("ChineseName='" + dt.Rows[i]["矿物中文名称"].ToString() +
                                                                       "'");
                        if (dataRow.Length > 0)
                        {
                            failed++;
                        }
                        else
                        {
                            HeterogeneousMineralInfo heterogeneousMineral = null;
                            string Ar = dt.Rows[i]["非均质视旋转角Ar"].ToString();
                            if (!String.IsNullOrEmpty(Ar))
                            {
                                if (Ar.Contains("="))
                                    Ar = Ar.Split('=')[1];
                                Ar = AngleHelper.ConvertAngleToString(Ar);
                            }
                            heterogeneousMineral = new HeterogeneousMineralInfo(0,
                                dt.Rows[i]["矿物中文名称"].ToString(), dt.Rows[i]["矿物英文名称"].ToString(),
                                dt.Rows[i]["化学式"].ToString(), dt.Rows[i]["矿物的晶系"].ToString(),
                                dt.Rows[i]["均非性"].ToString(), dt.Rows[i]["反射率"].ToString(), dt.Rows[i]["硬度"].ToString(),
                                dt.Rows[i]["反射色"].ToString(), dt.Rows[i]["双反射及反射多色性"].ToString(),
                                float.Parse(Ar), dt.Rows[i]["非均质视旋转色散DAr"].ToString(),
                                dt.Rows[i]["旋向Rs"].ToString(), dt.Rows[i]["相符Ps"].ToString(),
                                dt.Rows[i]["反射视旋转色散DRr"].ToString(), dt.Rows[i]["反射视旋转色散DAR"].ToString(),
                                dt.Rows[i]["内反射"].ToString(), dt.Rows[i]["矿物成因产状形态特征及伴生矿物"].ToString(),
                                dt.Rows[i]["主要鉴定特征"].ToString());
                            AccessDB.Add(heterogeneousMineral);
                            successed++;
                        }

                    }
                }
                else
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DataRow[] dataRow =
                            mineralDataSet.Tables[dt.TableName].Select("ChineseName='" + dt.Rows[i]["矿物中文名称"].ToString() +
                                                                       "'");
                        if (dataRow.Length > 0)
                        {
                            failed++;
                        }
                        else
                        {
                            HomogeneousMineralInfo homogeneousMineral = null;
                            string Rr = dt.Rows[i]["反射视旋转角Rr"].ToString();
                            if (!String.IsNullOrEmpty(Rr))
                            {
                                if (Rr.Contains("="))
                                    Rr = Rr.Split('=')[1];
                                Rr = AngleHelper.ConvertAngleToString(Rr);
                            }
                            homogeneousMineral = new HomogeneousMineralInfo(0,
                                dt.Rows[i]["矿物中文名称"].ToString(), dt.Rows[i]["矿物英文名称"].ToString(),
                                dt.Rows[i]["化学式"].ToString(), dt.Rows[i]["矿物的晶系"].ToString(),
                                dt.Rows[i]["均非性"].ToString(), dt.Rows[i]["反射率"].ToString(), dt.Rows[i]["硬度"].ToString(),
                                dt.Rows[i]["反射色"].ToString(), float.Parse(Rr),
                                dt.Rows[i]["反射视旋转色散DRr"].ToString(), dt.Rows[i]["内反射"].ToString(),
                                dt.Rows[i]["矿物成因产状形态特征及伴生矿物"].ToString(), dt.Rows[i]["主要鉴定特征"].ToString());

                            AccessDB.Add(homogeneousMineral);
                            successed++;
                        }
                    }
                }
                return ("数据导入到" + dt.TableName + "表成功！成功导入" + successed + "条，有" + failed + "条在数据库中已存在");
            }
            else
            {
                return ("请检查你的Excel中是否存在数据");
            }
        }

        #region 更改前

        /*
        public static string ExportDataToAccess(string filepath, DataSet mineralDataSet)
        {
            int successed = 0;
            int failed = 0;
            DataTable dt = GetDataByExcel(filepath);
            if (dt.Rows.Count > 0)
            {
                if (CheckIsRepeatInExcel(dt))
                {
                    return ("Excel中有重复的矿物，请检查后再来");
                }
                if (dt.TableName.Contains("非均质矿物"))
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DataRow[] dataRow = mineralDataSet.Tables[dt.TableName].Select("ChineseName='" + dt.Rows[i]["矿物中文名称"].ToString() + "'");
                        if (dataRow.Length > 0)
                        {
                            failed++;
                        }
                        else
                        {
                            HeterogeneousMineralInfo heterogeneousMineral = null;
                            if (!String.IsNullOrEmpty(dt.Rows[i]["非均质视旋转角Ar"].ToString()))
                            {
                                heterogeneousMineral = new HeterogeneousMineralInfo(0,
                                                        dt.Rows[i]["矿物中文名称"].ToString(), dt.Rows[i]["矿物英文名称"].ToString(),
                                                        dt.Rows[i]["化学式"].ToString(), dt.Rows[i]["矿物的晶系"].ToString(),
                                                        dt.Rows[i]["均非性"].ToString(), dt.Rows[i]["反射率"].ToString(), dt.Rows[i]["硬度"].ToString(),
                                                        dt.Rows[i]["反射色"].ToString(), dt.Rows[i]["双反射及反射多色性"].ToString(),
                                                        float.Parse(Ar), dt.Rows[i]["非均质视旋转色散DAr"].ToString(),
                                                        dt.Rows[i]["旋向Rs"].ToString(), dt.Rows[i]["相符Ps"].ToString(),
                                                        dt.Rows[i]["反射视旋转色散DRr"].ToString(), dt.Rows[i]["反射视旋转色散DAR"].ToString(),
                                                        dt.Rows[i]["内反射"].ToString(), dt.Rows[i]["矿物成因产状形态特征及伴生矿物"].ToString(),
                                                        dt.Rows[i]["主要鉴定特征"].ToString());
                            }
                            else
                            {
                                heterogeneousMineral = new HeterogeneousMineralInfo(0,
                                                        dt.Rows[i]["矿物中文名称"].ToString(), dt.Rows[i]["矿物英文名称"].ToString(),
                                                        dt.Rows[i]["化学式"].ToString(), dt.Rows[i]["矿物的晶系"].ToString(),
                                                        dt.Rows[i]["均非性"].ToString(), dt.Rows[i]["反射率"].ToString(), dt.Rows[i]["硬度"].ToString(),
                                                        dt.Rows[i]["反射色"].ToString(), dt.Rows[i]["双反射及反射多色性"].ToString(),
                                                        dt.Rows[i]["非均质视旋转色散DAr"].ToString(),dt.Rows[i]["旋向Rs"].ToString(), 
                                                        dt.Rows[i]["相符Ps"].ToString(),dt.Rows[i]["反射视旋转色散DRr"].ToString(), 
                                                        dt.Rows[i]["反射视旋转色散DAR"].ToString(),dt.Rows[i]["内反射"].ToString(), 
                                                        dt.Rows[i]["矿物成因产状形态特征及伴生矿物"].ToString(),dt.Rows[i]["主要鉴定特征"].ToString());
                            }
                            AccessDB.Add(heterogeneousMineral);
                            successed++; 
                        }

                    }
                }
                else
                {
                     for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DataRow[] dataRow = mineralDataSet.Tables[dt.TableName].Select("ChineseName='" + dt.Rows[i]["矿物中文名称"].ToString() + "'");
                        if (dataRow.Length > 0)
                        {
                            failed++;
                        }
                        else
                        {
                            HomogeneousMineralInfo homogeneousMineral = null;
                            if (!String.IsNullOrEmpty(dt.Rows[i]["反射视旋转角Rr"].ToString()))
                            {
                                homogeneousMineral = new HomogeneousMineralInfo(0,
                                                        dt.Rows[i]["矿物中文名称"].ToString(), dt.Rows[i]["矿物英文名称"].ToString(),
                                                        dt.Rows[i]["化学式"].ToString(), dt.Rows[i]["矿物的晶系"].ToString(),
                                                        dt.Rows[i]["均非性"].ToString(), dt.Rows[i]["反射率"].ToString(), dt.Rows[i]["硬度"].ToString(),
                                                        dt.Rows[i]["反射色"].ToString(), float.Parse(dt.Rows[i]["反射视旋转角Rr"].ToString()),
                                                        dt.Rows[i]["反射视旋转色散DRr"].ToString(), dt.Rows[i]["内反射"].ToString(),
                                                        dt.Rows[i]["矿物成因产状形态特征及伴生矿物"].ToString(), dt.Rows[i]["主要鉴定特征"].ToString());
                            }
                            else
                            {
                                homogeneousMineral = new HomogeneousMineralInfo(0,
                                                        dt.Rows[i]["矿物中文名称"].ToString(), dt.Rows[i]["矿物英文名称"].ToString(),
                                                        dt.Rows[i]["化学式"].ToString(), dt.Rows[i]["矿物的晶系"].ToString(),
                                                        dt.Rows[i]["均非性"].ToString(), dt.Rows[i]["反射率"].ToString(), 
                                                        dt.Rows[i]["硬度"].ToString(),dt.Rows[i]["反射色"].ToString(),
                                                        dt.Rows[i]["反射视旋转色散DRr"].ToString(),dt.Rows[i]["内反射"].ToString(),
                                                        dt.Rows[i]["矿物成因产状形态特征及伴生矿物"].ToString(), dt.Rows[i]["主要鉴定特征"].ToString());
                            }
                            AccessDB.Add(homogeneousMineral);
                            successed++; 
                        }
                    }
                }
                return ("数据导入到" + dt.TableName + "表成功！成功导入" + successed + "条，有" + failed + "条在数据库中已存在");
            }
            else
            {
                return ("请检查你的Excel中是否存在数据");
            }
        }
        */
        #endregion
        /// <summary>
        /// 整理EXCEL导入的datatable
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        private static DataTable UpdateDataTableByFiled(DataTable dt)
        {
            DataTable newdt = new DataTable();
            newdt.TableName = dt.TableName;
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                newdt.Columns.Add(dt.Rows[0][i].ToString(), typeof(string));
                for (int j = 0; j < dt.Rows.Count - 1; j++)
                {
                    if (j >= newdt.Rows.Count)
                    {
                        newdt.Rows.Add(newdt.NewRow());
                    }
                    newdt.Rows[j][dt.Rows[0][i].ToString()] = dt.Rows[j + 1][i].ToString();
                }
            }
            return newdt;
        }

        /// <summary>
        /// 判断Excel中是否有重复的矿物
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        private static bool CheckIsRepeatInExcel(DataTable dt)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                //若矿物名称为空也直接返回错误，让用户去检查
                if (string.IsNullOrEmpty(dt.Rows[i]["矿物中文名称"].ToString()))
                {
                    return true;
                }
                //这里算法不简洁
                for (int j = i + 1; j < dt.Rows.Count; j++)
                {
                    if (dt.Rows[i]["矿物中文名称"].ToString() == dt.Rows[j]["矿物中文名称"].ToString())
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
