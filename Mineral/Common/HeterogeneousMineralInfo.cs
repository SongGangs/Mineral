using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mineral.Common
{
    /// <summary>
    /// 非均质矿物
    /// </summary>
    class HeterogeneousMineralInfo:IMineral
    {
        public int mineralType { get { return 2; } }//代表非均质矿物
        public int ID { get; set; }
        public string ChineseName { get; set; }//矿物中文名称
        public string EnglishName { get; set; }//矿物英文名称
        public string ChemicalFormula { get; set; }//化学式
        public string Syngony { get; set; }//矿物的晶系
        public string NonUniformity { get; set; }//均非性
        public string Reflectivity { get; set; }//反射率
        public string Hardness { get; set; }//硬度
        public string ReflectionColor { get; set; }//反射色
        public string Bireflection { get; set; }//双反射及反射多色性
        public float Ar { get; set; }//非均质视旋转角Ar
        public string DAr { get; set; }//非均质视旋转色散DAr
        public string Rs { get; set; }//旋向Rs
        public string Ps { get; set; }//相符Ps
        public string DRr { get; set; }//反射视旋转色散DRr
        public string ReflectionDAR { get; set; }//反射视旋转色散DAR
        public string InternalReflection { get; set; }//内反射
        public string Origin { get; set; }//矿物成因产状形态特征及伴生矿物
        public string IMK { get; set; }//主要鉴定特征

        public HeterogeneousMineralInfo()
        {
            
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="ChineseName">矿物中文名称</param>
        /// <param name="EnglishName">矿物英文名称</param>
        /// <param name="ChemicalFormula">化学式</param>
        /// <param name="Syngony">矿物的晶系</param>
        /// <param name="NonUniformity">均非性</param>
        /// <param name="Reflectivity">反射率</param>
        /// <param name="Hardness">硬度</param>
        /// <param name="ReflectionColor">反射色</param>
        /// <param name="Bireflection">双反射及反射多色性</param>
        /// <param name="Ar">非均质视旋转角Ar</param>
        /// <param name="DAr">非均质视旋转色散DAr</param>
        /// <param name="Rs">旋向Rs</param>
        /// <param name="Ps">相符Ps</param>
        /// <param name="DRr">反射视旋转色散DRr</param>
        /// <param name="ReflectionDAR">反射视旋转色散DAR</param>
        /// <param name="InternalReflection">内反射</param>
        /// <param name="Origin">矿物成因产状形态特征及伴生矿物</param>
        /// <param name="IMK">主要鉴定特征</param>
        public HeterogeneousMineralInfo(int ID, string ChineseName, string EnglishName, string ChemicalFormula,
            string Syngony, string NonUniformity, string Reflectivity, string Hardness, string ReflectionColor,
            string Bireflection, float Ar, string DAr, string Rs, string Ps, string DRr, string ReflectionDAR,
            string InternalReflection, string Origin, string IMK)
        {
            this.ID = ID;
            this.ChineseName = ChineseName;
            this.EnglishName = EnglishName;
            this.ChemicalFormula = ChemicalFormula;
            this.Syngony = Syngony;
            this.NonUniformity = NonUniformity;
            this.Reflectivity = Reflectivity;
            this.Hardness = Hardness;
            this.ReflectionColor = ReflectionColor;
            this.Bireflection = Bireflection;
            this.Ar = Ar;
            this.DAr = DAr;
            this.Rs = Rs;
            this.Ps = Ps;
            this.DRr = DRr;
            this.ReflectionDAR = ReflectionDAR;
            this.InternalReflection = InternalReflection;
            this.Origin = Origin;
            this.IMK = IMK;
        }
        public HeterogeneousMineralInfo(int ID, string ChineseName, string EnglishName, string ChemicalFormula,
                string Syngony, string NonUniformity, string Reflectivity, string Hardness, string ReflectionColor,
                string Bireflection, string DAr, string Rs, string Ps, string DRr, string ReflectionDAR,
                string InternalReflection, string Origin, string IMK)
        {
            this.ID = ID;
            this.ChineseName = ChineseName;
            this.EnglishName = EnglishName;
            this.ChemicalFormula = ChemicalFormula;
            this.Syngony = Syngony;
            this.NonUniformity = NonUniformity;
            this.Reflectivity = Reflectivity;
            this.Hardness = Hardness;
            this.ReflectionColor = ReflectionColor;
            this.Bireflection = Bireflection;
            this.DAr = DAr;
            this.Rs = Rs;
            this.Ps = Ps;
            this.DRr = DRr;
            this.ReflectionDAR = ReflectionDAR;
            this.InternalReflection = InternalReflection;
            this.Origin = Origin;
            this.IMK = IMK;
        }
    }
}
