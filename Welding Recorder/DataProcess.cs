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

    }
}
