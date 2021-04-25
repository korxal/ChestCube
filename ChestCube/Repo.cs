using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ChestCube
{



    public class Repo
    {
        static NLog.Logger l = NLog.LogManager.GetCurrentClassLogger();
        private readonly RepoManager rm;
        private readonly EntityTypeManager etm;
        private readonly RepoSettings rs;


        private MsSql.MSSQLDriver d;

        public Repo(string ConnectionString, string Schema = "ChestCube")
        {
            //Connect to database
            try
            {
                d = new MsSql.MSSQLDriver(ConnectionString);

            }
            catch (Exception e)
            {
                l.Fatal($"Unable to connect to DB:{e.Message} \r\n{e.StackTrace}");
                System.Environment.Exit(-1);
            }

            rs = new RepoSettings(Schema);
            rm = new RepoManager(d, rs);
            etm = new EntityTypeManager(d, rs);

            rm.InitRepo(Schema);

        }


        //FIXME - return Int64
        public ulong StoreObject(object o)
        {
            Type T = o.GetType();

            //Check if type is known
            //If not known create store place
            if (!etm.IsTypeKnown(T))
                etm.CreateItemStore(T);

            //TODO if known Compare definition
            //TODO Take type from cache
            EntityType et = new EntityType(o);

            Dictionary<string, object> kv = new Dictionary<string, object>();

            //Save Fields
            foreach (var f in et.Fields)
                kv[f] = T.GetField(f).GetValue(o);

            //Save Properties
            foreach (var f in et.Properties)
                kv[f] = T.GetProperty(f).GetValue(o);

            // Store Object
            return (ulong)d.InsertRow($"{rs.Schema}.{et.TableName}", kv);

        }


        public T GetObject<T>(ulong id)
        {

            //TODO check cache
            string tableName = d.FetchStringValue(
                rs.IndexTableFullName,
                "TableName",
                $" TypeFullName='{typeof(T).FullName}' and AssemblyFullName='{typeof(T).Assembly.FullName}'");
            //TODO - deal with nested objects
            //TODO - deal with nested lists
            //TODO - deal with nested Dicts

            object rez = d.Connection.Query<T>($"select * from [{rs.Schema}].[{tableName}] where ChestGuid={id}").FirstOrDefault();

            return (T)rez;
        }



    }

}