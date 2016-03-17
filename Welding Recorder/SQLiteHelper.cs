using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SQLite;

namespace Welding_Recorder
{
    public class SQLiteHelper
    {
        public static SQLiteParameter CreateParameter(string paramName, object paramValue, DbType paramType, ParameterDirection paramDirection = ParameterDirection.Input)
        {
            SQLiteParameter param = new SQLiteParameter();
            param.ParameterName = paramName;
            param.DbType = paramType;
            param.Direction = paramDirection;
            param.Value = paramValue;
            return param;
        }

        public static SQLiteParameter CreateStringParameter(string paramName, object paramValue)
        {
            return CreateParameter(paramName, paramValue, DbType.String, ParameterDirection.Input);
        }
    }
}
