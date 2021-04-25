using System;
using System.Collections.Generic;
using System.Text;

namespace ChestCube
{
   public  class RepoSettings
    {

        public RepoSettings(string schema)
        {
            Schema = schema;
            IndexTableFullName = "["+Schema + "].[" + IndexTableName+"]";
        }
        public readonly string IndexTableFullName;

        public string GetTableName(Type T) => "[" + Schema + "].[" + T.Namespace + "." + T.Name + "]";

        public string Schema;
        public  string IndexTableName = "ChestCubeIndex";
    }
}
