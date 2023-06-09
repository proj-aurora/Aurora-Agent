using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Serilog;

namespace ACL
{
    internal class ACLUtils
    {
        public static void PrintProperties(ILogger logger,object obj)
        {
            PrintProperties(logger, obj, 0);
        }
        public static void PrintProperties(ILogger logger,object obj, int indent)
        {
            if (obj == null) return;
            string indentString = new string(' ', indent);
            Type objType = obj.GetType();
            PropertyInfo[] properties = objType.GetProperties();
            foreach (PropertyInfo property in properties)
            {
                object propValue = property.GetValue(obj, null);
                if (property.PropertyType.Assembly == objType.Assembly && !property.PropertyType.IsEnum)
                {

                    logger.Debug("{0}{1}:", indentString, property.Name);
                    PrintProperties(logger, propValue, indent + 2);
                }
                else
                {
                    logger.Debug("{0}{1}: {2}", indentString, property.Name, propValue);
                }
            }
        }
    }
}
