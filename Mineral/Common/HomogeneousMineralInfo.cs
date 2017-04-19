

namespace Mineral.Common
{
    /// <summary>
    /// 均质矿物
    /// </summary>
    class HomogeneousMineralInfo:IMineral
    {
        public int mineralType { get { return 1; }}
        public int ID { get; set; }
        public string ChineseName { get; set; }//矿物中文名称
        public string EnglishName { get; set; }//矿物英文名称
        public string ChemicalFormula { get; set; }//化学式
        public string Syngony { get; set; }//矿物的晶系
        public string NonUniformity { get; set; }//均非性
        public string Reflectivity { get; set; }//反射率
        public string Hardness { get; set; }//硬度
        public string ReflectionColor { get; set; }//反射色
        public string Rr { get; set; } //反射视旋转角Rr
        public string DRr { get; set; }//反射视旋转色散DRr
        public string InternalReflection { get; set; }//内反射
        public string Origin { get; set; }//矿物成因产状形态特征及伴生矿物
        public string IMK { get; set; }//主要鉴定特征

        public HomogeneousMineralInfo()
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
        /// <param name="Rr">反射视旋转角Rr</param>
        /// <param name="DRr">反射视旋转色散DRr</param>
        /// <param name="InternalReflection">内反射</param>
        /// <param name="Origin">矿物成因产状形态特征及伴生矿物</param>
        /// <param name="IMK">主要鉴定特征</param>
        public HomogeneousMineralInfo(int ID,string ChineseName, string EnglishName, string ChemicalFormula,
            string Syngony, string NonUniformity, string Reflectivity, string Hardness, string ReflectionColor,
            string DRr, string InternalReflection, string Origin, string IMK)
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
            this.DRr = DRr;
            this.InternalReflection = InternalReflection;
            this.Origin = Origin;
            this.IMK = IMK;
        }
        
        public HomogeneousMineralInfo(int ID, string ChineseName, string EnglishName, string ChemicalFormula,
            string Syngony, string NonUniformity, string Reflectivity, string Hardness, string ReflectionColor, string Rr,
            string DRr, string InternalReflection, string Origin, string IMK)
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
            this.Rr = Rr;
            this.DRr = DRr;
            this.InternalReflection = InternalReflection;
            this.Origin = Origin;
            this.IMK = IMK;
        }
    }
}
