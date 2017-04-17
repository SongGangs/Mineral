using System;
using System.Collections.Generic;
using System.Reflection;
namespace Mineral.Helper
{
    class MapHelper
    {
        /// <summary>  
        ///   
        /// 将对象属性转换为key-value对  
        /// </summary>  
        /// <param name="o"></param>  
        /// <returns></returns>  
        public static Dictionary<String, Object> ToMap(Object o)
        {
            Dictionary<String, Object> map = new Dictionary<string, object>();

            Type t = o.GetType();

            PropertyInfo[] pi = t.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (PropertyInfo p in pi)
            {
                MethodInfo mi = p.GetGetMethod();

                if (mi != null && mi.IsPublic)
                {
                    map.Add(p.Name, mi.Invoke(o, new Object[] { }));
                }
            }

            return map;

        }  
    }
}
