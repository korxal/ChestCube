using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace ChestCube
{
   public static class Utils
    {



        public static string MapType(FieldInfo f) => MapType(f.FieldType.Name);

        public static string MapType(PropertyInfo f) => MapType(f.PropertyType.Name);

        public static string MapType(string p)
        {
            //Check if type belongs to assembly
            switch (p)
            {
                case "string":
                case "String": return "varchar(max)";
                case "Single": return "real";
                case "Int32": return "integer";
                case "Int16": return "integer";
                case "Int64": return "bigint";
                case "DateTime": return "DateTime";
                case "Boolean": return "BIT";
                case "Guid": return "uniqueidentifier";
                case "Decimal": return "decimal(19, 5)";
                default:
                    return null;
            }
        }


    }
}
