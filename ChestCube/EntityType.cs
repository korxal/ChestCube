using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace ChestCube
{
    class EntityType
    {
        static NLog.Logger l = NLog.LogManager.GetCurrentClassLogger();
        private MsSql.MSSQLDriver d;

        public string TypeFullName { get; }
        public string AsmFullName { get; }
        public string Name { get; }

    public string TableName { get; }


        public EntityType(object o):this(o.GetType()) { }

        public EntityType(Type T)
        {
            TypeFullName = T.FullName;
            AsmFullName = T.Assembly.FullName;
            Name = T.Name;
            TableName = "["+T.Namespace+"."+T.Name+"]";

            foreach (PropertyInfo j in T.GetProperties())
            {
                var t = Utils.MapType(j);
                if (t != null)
                {
                    EntityFields[j.Name] = Utils.MapType(j);
                    Properties.Add(j.Name);
                    continue;
                }
                else
                {


                }
            }

            foreach (FieldInfo j in T.GetFields(BindingFlags.Public| BindingFlags.Instance))
            {
                var t = Utils.MapType(j);
                if (t != null)
                {
                    EntityFields[j.Name] = Utils.MapType(j);
                    Fields.Add(j.Name);
                    continue;
                }
                else
                {
                    //  j.FieldType

                }
            }


        }

        public List<string> Properties = new List<string>();
        public List<string> Fields = new List<string>();
        public List<string> Lists = new List<string>();
        public List<string> Dictionarys = new List<string>();
        public Dictionary<string, string> EntityFields = new Dictionary<string, string>();

    }
}
