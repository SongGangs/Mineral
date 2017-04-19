using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mineral.Helper
{
    class AngleHelper
    {
        /// <summary>
        /// 将度分秒转换为小数
        /// </summary>
        /// <param name="angle"></param>
        /// <returns></returns>
        public static string ConvertAngleToString(string angle)
        {
            string result=String.Empty;
            int counts = 0;
            if (angle.Contains("″"))
                counts++;
            if (angle.Contains("′"))
                counts++;
            if (angle.Contains("°"))
                counts++;
            string[] value = new string[counts]; ;
            switch (counts)
            {
                case   1:
                    value[0] = angle.Split('°')[0];
                    result = value[0];
                    break;
                case 2:
                    value[0] = angle.Split('°')[0];
                    value[1] = angle.Split('°')[1].Split('′')[0];
                    result = (float.Parse(value[0])+(float.Parse(value[1])/60)).ToString();
                    break;
                case 3:
                    value[0] = angle.Split('°')[0];
                    value[1] = angle.Split('°')[1].Split('′')[0];
                    value[2] = angle.Split('°')[1].Split('′')[1].Split('″')[0];
                    result = (float.Parse(value[0]) + (float.Parse(value[1]) / 60) + (float.Parse(value[2]) / 3600)).ToString();
                    break;
            }
          return  result;
        }

        public static string ConvertStringToAngle(float value)
        {
            string result = String.Empty;
            return result;
        }
    }
}
