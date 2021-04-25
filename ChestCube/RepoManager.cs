using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace ChestCube
{
    class RepoManager
    {
        static NLog.Logger l = NLog.LogManager.GetCurrentClassLogger();
        private MsSql.MSSQLDriver d;
        private RepoSettings rs;

        public RepoManager(MsSql.MSSQLDriver db, RepoSettings settings)
        {
            d = db;
            rs = settings;
        }


        public void InitRepo(string Schema)
        {

            //Verify that our schema exists
            try
            {
                if (d.FetchIntValue("sys.schemas", "count(*)", $"name= '{rs.Schema}'") < 1)
                    d.ExecQuery("CREATE SCHEMA " + rs.Schema);
            }
            catch (Exception e)
            {
                l.Fatal($"Unable to create schema '{rs.Schema}':{e.Message} \r\n{e.StackTrace}");
                System.Environment.Exit(-1);
            }


            //Create Index table
            try
            {
                if (!d.IsTableExists(rs.IndexTableName))
                {
                    d.ExecQuery($"CREATE TABLE {rs.IndexTableFullName} (" +
                        $"[id] [bigint] IDENTITY(1,1) NOT NULL," +
                        $"[NameSpace] varchar(255) NOT NULL," +
                        $"[TypeName] varchar(255) NOT  NULL," +
                        $"[TypeFullName] varchar(255) NOT  NULL," +
                        $"[AssemblyFullName] varchar(255) NOT  NULL," +
                        $"[TableName] varchar(255) NOT NULL" +
                        $")");
                }
            }
            catch (Exception e)
            {
                l.Fatal($"Unable to create index table'{rs.IndexTableFullName}':{e.Message} \r\n{e.StackTrace}");
                System.Environment.Exit(-1);
            }


        }




    }
}
