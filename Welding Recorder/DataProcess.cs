using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Diagnostics;

namespace Welding_Recorder
{
    class DataProcess
    {
        public static string DataDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), Process.GetCurrentProcess().ProcessName);
        public static string DBPATH = Path.Combine(DataDirectory, "data.db");

        private string DataSource
        {
            get
            {
                if (!Directory.Exists(DataDirectory))
                {
                    Directory.CreateDirectory(DataDirectory);
                }
                return "Data Source=" + DBPATH;
            }
        }

        private void InitializeTablesAndData()
        {
            using (var conn = new SQLiteConnection(DataSource))
            {
                conn.Open();
                using (SQLiteCommand command = new SQLiteCommand(conn))
                {
                    command.CommandText = "CREATE TABLE Histories(id integer NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE, name string, gangtao_type string, welding_item string, welding_current real, ar_flow string, room_temperature string, operator string, created_at timestamp)";
                    command.ExecuteNonQuery();
                    command.CommandText = "CREATE TABLE GangTao(id integer NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE, type string UNIQUE)";
                    command.ExecuteNonQuery();
                    command.CommandText = "CREATE TABLE WeldingItem(id integer NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE, type string UNIQUE)";
                    command.ExecuteNonQuery();
                    command.CommandText = "CREATE TABLE Operator(id integer NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE, name string UNIQUE)";
                    command.ExecuteNonQuery();
                    command.CommandText = "CREATE TABLE Signal(id integer NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE, type integer, at timestamp, delta integer, history_id integer)";
                    command.ExecuteNonQuery();
                    command.CommandText = "INSERT INTO GangTao ('type') values ('270'), ('242'), ('215'), ('190'), ('170'), ('150'), ('130'), ('110'), ('90'), ('75'), ('60'), ('46')";
                    command.ExecuteNonQuery();
                    command.CommandText = "INSERT INTO WeldingItem ('type') values ('瓦片'), ('活塞'), ('后盖')";
                    command.ExecuteNonQuery();
                }
            }
        }

        public DataProcess()
        {
            if (!File.Exists(DBPATH))
            {
                InitializeTablesAndData();
            }

            Console.WriteLine(DBPATH);
        }

        public List<string> GangTaoList()
        {
            var gangtaoList = new List<string>();
            using (var conn = new SQLiteConnection(DataSource))
            {
                conn.Open();
                using (SQLiteCommand command = new SQLiteCommand(conn))
                {
                    var sql = "SELECT * FROM GangTao";
                    command.CommandText = sql;
                    var reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        var type = (string)reader.GetValue(1);
                        gangtaoList.Add(type);
                    }
                }
            }
            
            return gangtaoList;
        }

        public List<string> WeldingItemList()
        {
            var weldingItemList = new List<string>();
            using (var conn = new SQLiteConnection(DataSource))
            {
                conn.Open();
                using (SQLiteCommand command = new SQLiteCommand(conn))
                {
                    var sql = "SELECT * FROM WeldingItem";
                    command.CommandText = sql;
                    var reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        var type = (string)reader.GetValue(1);
                        weldingItemList.Add(type);
                    }
                }
            }

            return weldingItemList;
        }

        public List<string> OperatorList()
        {
            var operatorList = new List<string>();
            using (var conn = new SQLiteConnection(DataSource))
            {
                conn.Open();
                using (SQLiteCommand command = new SQLiteCommand(conn))
                {
                    var sql = "SELECT * FROM Operator";
                    command.CommandText = sql;
                    var reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        var type = (string)reader.GetValue(1);
                        operatorList.Add(type);
                    }
                }
            }

            return operatorList;
        }
        
        public void addGangTao(string type)
        {
            using (var conn = new SQLiteConnection(DataSource))
            {
                conn.Open();
                using (SQLiteCommand command = new SQLiteCommand(conn))
                {
                    command.CommandText = "INSERT INTO GangTao ('type') values (@type)";
                    var typeParam = SQLiteHelper.CreateStringParameter("@type", type);
                    command.Parameters.Add(typeParam);

                    command.ExecuteNonQuery();
                }
            }
        }

        public void addWeldingItem(string type)
        {
            using (var conn = new SQLiteConnection(DataSource))
            {
                conn.Open();
                using (SQLiteCommand command = new SQLiteCommand(conn))
                {
                    command.CommandText = "INSERT INTO WeldingItem ('type') values (@type)";
                    var typeParam = SQLiteHelper.CreateStringParameter("@type", type);
                    command.Parameters.Add(typeParam);

                    command.ExecuteNonQuery();
                }
            }
        }

        public void addOperator(string name)
        {
            using (var conn = new SQLiteConnection(DataSource))
            {
                conn.Open();
                using (SQLiteCommand command = new SQLiteCommand(conn))
                {
                    command.CommandText = "INSERT INTO Operator ('name') values (@name)";
                    var nameParam = SQLiteHelper.CreateStringParameter("@name", name);
                    command.Parameters.Add(nameParam);

                    command.ExecuteNonQuery();
                }
            }
        }

        public void saveSignals(List<Signal> signals, Dictionary<string, object> meta)
        {
            using (var conn = new SQLiteConnection(DataSource))
            {
                conn.Open();
                using (SQLiteCommand command = new SQLiteCommand(conn))
                {
                    // history
                    command.CommandText = "INSERT INTO Histories ('name', 'gangtao_type', 'welding_item', 'welding_current', 'ar_flow', 'room_temperature', 'operator', 'created_at') values (@name, @gangtao_type, @welding_item, @welding_current, @ar_flow, @room_temperature, @operator, @created_at)";
                    var nameParam = SQLiteHelper.CreateStringParameter("@name", (string)meta["name"]);
                    var gangtaoTypeParam = SQLiteHelper.CreateStringParameter("@gangtao_type", (string)meta["gangtao_type"]);
                    var WeldingItemParam = SQLiteHelper.CreateStringParameter("@welding_item", (string)meta["welding_item"]);
                    var WeldingCurrentParam = SQLiteHelper.CreateStringParameter("@welding_current", (string)meta["welding_current"]);
                    var ARFlowParam = SQLiteHelper.CreateStringParameter("@ar_flow", (string)meta["ar_flow"]);
                    var RoomTemperatureParam = SQLiteHelper.CreateStringParameter("@room_temperature", (string)meta["room_temperature"]);
                    var OperatorParam = SQLiteHelper.CreateStringParameter("@operator", (string)meta["operator"]);
                    var CreatedAtParam = SQLiteHelper.CreateParameter("@created_at", (DateTime)meta["created_at"], DbType.DateTime);
                    command.Parameters.Add(nameParam);
                    command.Parameters.Add(gangtaoTypeParam);
                    command.Parameters.Add(WeldingItemParam);
                    command.Parameters.Add(WeldingCurrentParam);
                    command.Parameters.Add(ARFlowParam);
                    command.Parameters.Add(RoomTemperatureParam);
                    command.Parameters.Add(OperatorParam);
                    command.Parameters.Add(CreatedAtParam);

                    command.ExecuteNonQuery();

                    // history id
                    long historyId = -1;

                    var sql = "SELECT * FROM Histories ORDER BY `created_at` DESC LIMIT 1";
                    command.CommandText = sql;
                    var reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        historyId = reader.GetInt64(0);
                    }

                    reader.Close();

                    // signals
                    for (int i = 0; i < signals.Count; i++)
                    {
                        var sig = signals[i];
                        var delta = 0;
                        if (i != 0) //first
                        {
                            var interval = signals[i].Timestamp - signals[i - 1].Timestamp;
                            delta = (int)interval.TotalMilliseconds; // Ignore time less tham 1ms.
                        }
                        command.CommandText = "INSERT INTO Signal ('type', 'at', 'delta', 'history_id') values (@type, @at, @delta, @history_id)";
                        var typeParam = SQLiteHelper.CreateParameter("@type", (int)sig.Type, DbType.Int32);
                        var atParam = SQLiteHelper.CreateParameter("@at", sig.Timestamp, DbType.DateTime);
                        var deltaParam = SQLiteHelper.CreateParameter("@delta", delta, DbType.Int32);
                        var historyIdParam = SQLiteHelper.CreateParameter("@history_id", historyId, DbType.Int64);
                        command.Parameters.Add(typeParam);
                        command.Parameters.Add(atParam);
                        command.Parameters.Add(deltaParam);
                        command.Parameters.Add(historyIdParam);

                        command.ExecuteNonQuery();
                    }
                }
            }
        }
    }
}
