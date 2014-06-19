using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Reflection;

namespace BDJX.BSCP.Common
{
    /// <summary>
    /// 工厂类--利用反射实现
    /// </summary>
    public class BdjxFactory
    {
        /// <summary>
        /// 创建实例--调用该方法的项目中添加了对程序集的引用
        /// </summary>
        /// <typeparam name="T">实例类型</typeparam>
        /// <param name="assemblyName">程序集名称</param>
        /// <param name="nameSpace">命名空间</param>
        /// <param name="className">类名</param>
        /// <returns>指定类型的对象实例</returns>
        public static T CreateInstance<T>(string assemblyName, string nameSpace, string className)
        {
            try
            {
                string fullName = nameSpace + "." + className;//命名空间.类型名
                //此为第一种写法
                object ect = Assembly.Load(assemblyName).CreateInstance(fullName);//加载程序集，创建程序集里面的 命名空间.类型名 实例
                return (T)ect;//类型转换并返回
                //下面是第二种写法
                //string path = fullName + "," + assemblyName;//命名空间.类型名,程序集
                //Type o = Type.GetType(path);//加载类型
                //object obj = Activator.CreateInstance(o, true);//根据类型创建实例
                //return (T)obj;//类型转换并返回
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                //发生异常，返回类型的默认值
                return default(T);
            }
        }

        /// <summary>
        /// 创建实例--调用该方法的项目中没有添加对程序集的引用，直接通过程序集的路径来加载程序集
        /// </summary>
        /// <typeparam name="T">实例类型</typeparam>
        /// <param name="assemblyPath">程序集的路径,将要加载的程序集放到bin/debug目录下，与可执行文件同一个目录</param>
        /// <param name="className">类名,包括命名空间,如BDJX.BSCP.BLL.Test</param>
        /// <returns>指定类型的对象实例</returns>
        public static T CreateInstance<T>(string assemblyPath, string className)
        {
            try
            {
                //注意，用LoadFrom性能比Load差，因为，LoatFrom最终也会调用Load方法，这里只是为了不添加项目引用的情况下使用
                object ect = Assembly.LoadFrom(assemblyPath).CreateInstance(className);//加载程序集，创建程序集里面的 命名空间.类型名 实例
                return (T)ect;//类型转换并返回
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                //发生异常，返回类型的默认值
                return default(T);
            }
        }
    }
}
