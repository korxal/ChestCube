using System;
using System.Collections.Generic;
using System.Text;

namespace ChestCube
{
    public class EntityTypeManager
    {
        static NLog.Logger l = NLog.LogManager.GetCurrentClassLogger();
        private MsSql.MSSQLDriver d;
        private RepoSettings rs;


        public EntityTypeManager(MsSql.MSSQLDriver db, RepoSettings settings)
        {
            d = db;
            rs = settings;
        }


        public bool IsTypeKnown(Type T)
        {
            var table = d.FetchStringValue(rs.IndexTableFullName, "TableName", $"TypeName='{T.Name}' and AssemblyFullName='{T.Assembly.FullName}'");
            if (string.IsNullOrEmpty(table)) return false;

            return d.IsTableExists(table);

        }



        public void CreateItemStore(Type T)
        {
            EntityType et = new EntityType(T);

            StringBuilder sb = new StringBuilder($"CREATE TABLE {rs.Schema}.{et.TableName}(");

            sb.Append("ChestGuid bigint identity(1,1),");

            foreach (var j in et.EntityFields)
            {
                sb.Append(j.Key);
                sb.Append(" ");
                sb.Append(j.Value);
                sb.Append(",");
            }

            sb.Length--;
            sb.Append(")");

            try
            {
                d.ExecQuery(sb.ToString());
            }
            catch (Exception e)
            {
                l.Error($"Failed to create data table:{e.Message}  Query:'{sb.ToString()}' ");
            }

            d.InsertRow(rs.IndexTableFullName, new Dictionary<string, object>() {
               {"TypeName",T.Name},
               {"TypeFullName",T.FullName},
               {"AssemblyFullName",T.Assembly.FullName},
               {"TableName",$"{T.Namespace}.{T.Name}"},
               {"NameSpace",T.Namespace}
            });


        }

    }
}
