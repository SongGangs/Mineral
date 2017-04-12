using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mineral.Helper;

namespace Mineral.Common
{
    class AccessDB
    {
        public AccessDB()
        {
        }
        
        /// <summary>
        /// 通过对象增加数据库记录
        /// </summary>
        /// <param name="mineral"></param>
        public static void Add( IMineral mineral )
        {
            if (mineral.mineralType == 1)//均质
            {
                HomogeneousMineralInfo homogeneousMineral = (HomogeneousMineralInfo) mineral;
                string sql =
                    String.Format(
                        "INSERT INTO HomogeneousMineral(ChineseName,EnglishName,ChemicalFormula,Syngony,NonUniformity,Reflectivity,Hardness,ReflectionColor,Rr,DRr,InternalReflection,Origin,IMK) VALUES('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}')",
                        homogeneousMineral.ChineseName, homogeneousMineral.EnglishName,
                        homogeneousMineral.ChemicalFormula, homogeneousMineral.Syngony,
                        homogeneousMineral.NonUniformity, homogeneousMineral.Reflectivity,
                        homogeneousMineral.Hardness, homogeneousMineral.ReflectionColor, homogeneousMineral.Rr,
                        homogeneousMineral.DRr, homogeneousMineral.InternalReflection,
                        homogeneousMineral.Origin, homogeneousMineral.IMK);
                SqlHelper.ExecuteNonQuery(sql);
            }
            else if (mineral.mineralType == 2)//非均质
            {
                HeterogeneousMineralInfo heterogeneousMineral = (HeterogeneousMineralInfo) mineral;
                string sql =
                    String.Format(
                        "INSERT INTO HeterogeneousMineral(ChineseName,EnglishName,ChemicalFormula,Syngony,NonUniformity,Reflectivity,Hardness,ReflectionColor,Bireflection,Ar,DAr,Rs,Ps,DRr,ReflectionDAR,InternalReflection,Origin,IMK) VALUES('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}')",
                        heterogeneousMineral.ChineseName, heterogeneousMineral.EnglishName,
                        heterogeneousMineral.ChemicalFormula, heterogeneousMineral.Syngony,
                        heterogeneousMineral.NonUniformity, heterogeneousMineral.Reflectivity,
                        heterogeneousMineral.Hardness, heterogeneousMineral.ReflectionColor,
                        heterogeneousMineral.Bireflection, heterogeneousMineral.Ar, heterogeneousMineral.DAr,
                        heterogeneousMineral.Rs, heterogeneousMineral.Ps, heterogeneousMineral.DRr,
                        heterogeneousMineral.ReflectionDAR, heterogeneousMineral.InternalReflection,
                        heterogeneousMineral.Origin, heterogeneousMineral.IMK);
                SqlHelper.ExecuteNonQuery(sql);
            }

        }
        
        /// <summary>
        /// 通过实体删除
        /// </summary>
        /// <param name="mineral"></param>
        public static void DeleteByEntity(IMineral mineral)
        {

            string sql =
                String.Format(
                    "DELETE FROM " + (mineral.mineralType == 1 ? "HomogeneousMineral" : "HeterogeneousMineral") +
                    " WHERE mineral.Name='{0}'",
                    (mineral.mineralType == 1
                        ? ((HomogeneousMineralInfo) mineral).ChineseName
                        : ((HeterogeneousMineralInfo) mineral).ChineseName));
            SqlHelper.ExecuteNonQuery(sql);
            
             
        }
        
      
        /// <summary>
        /// 更新数据库
        /// </summary>
        /// <param name="mineral"></param>
        public static void Update(IMineral mineral)
        {
            if (mineral.mineralType == 1)
            {
                HomogeneousMineralInfo homogeneousMineral = (HomogeneousMineralInfo) mineral;
                string sql =
                    String.Format(
                          "UPDATE HomogeneousMineral SET ChineseName='{0}',EnglishName='{1}',ChemicalFormula='{2}',Syngony='{3}',NonUniformity='{4}',Reflectivity='{5}',Hardness='{6}',ReflectionColor='{7}',Rr='{8}',DRr='{9}',InternalReflection='{10}',Origin='{11}',IMK='{12}'  WHERE ID={13}",
                        homogeneousMineral.ChineseName, homogeneousMineral.EnglishName,
                        homogeneousMineral.ChemicalFormula, homogeneousMineral.Syngony,
                        homogeneousMineral.NonUniformity, homogeneousMineral.Reflectivity,
                        homogeneousMineral.Hardness, homogeneousMineral.ReflectionColor, homogeneousMineral.Rr,
                        homogeneousMineral.DRr, homogeneousMineral.InternalReflection,
                        homogeneousMineral.Origin, homogeneousMineral.IMK,homogeneousMineral.ID);
                SqlHelper.ExecuteNonQuery(sql);
            }
            else if (mineral.mineralType == 2)
            {
                HeterogeneousMineralInfo heterogeneousMineral = (HeterogeneousMineralInfo) mineral;
                string sql =
                    String.Format(
                     "UPDATE HeterogeneousMineral SET ChineseName='{0}',EnglishName='{1}',ChemicalFormula='{2}',Syngony='{3}',NonUniformity='{4}',Reflectivity='{5}',Hardness='{6}',ReflectionColor='{7}',Bireflection='{8}',Ar='{9}',DAr='{10}',Rs='{11}',Ps='{12}',DRr='{13}',ReflectionDAR='{14}',InternalReflection='{15}',Origin='{16}',IMK='{17}'  WHERE ID={18}",
                        heterogeneousMineral.ChineseName, heterogeneousMineral.EnglishName,
                        heterogeneousMineral.ChemicalFormula, heterogeneousMineral.Syngony,
                        heterogeneousMineral.NonUniformity, heterogeneousMineral.Reflectivity,
                        heterogeneousMineral.Hardness, heterogeneousMineral.ReflectionColor,
                        heterogeneousMineral.Bireflection, heterogeneousMineral.Ar, heterogeneousMineral.DAr,
                        heterogeneousMineral.Rs, heterogeneousMineral.Ps, heterogeneousMineral.DRr,
                        heterogeneousMineral.ReflectionDAR, heterogeneousMineral.InternalReflection,
                        heterogeneousMineral.Origin, heterogeneousMineral.IMK,heterogeneousMineral.ID);
                SqlHelper.ExecuteNonQuery(sql);
            }
        }

        /// <summary>
        /// 根据输入的矿物种类查询
        /// </summary>
        /// <param name="mineral">代表矿物种类  1表示均质矿物，2表示非均质矿物</param>
        /// <returns></returns>
        public static DataTable Query(IMineral mineral)
        {
            if (mineral.mineralType == 1)
            {
                string sql = String.Format("SELECT * FROM HomogeneousMineral");
                return SqlHelper.ExecuteDataTable(sql);
            }
            else if (mineral.mineralType == 2)
            {
                string sql = String.Format("SELECT * FROM HeterogeneousMineral");
                return SqlHelper.ExecuteDataTable(sql);
            }
            return new DataTable();
        }
    }
}
