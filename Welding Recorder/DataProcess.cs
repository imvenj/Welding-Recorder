﻿using System;
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
                    command.CommandText = "CREATE TABLE Histories(id integer NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE, name string, task_name string, gangtao_type string, welding_item string, welding_current string, ar_flow string, room_temperature string, operator string, created_at timestamp)";
                    command.ExecuteNonQuery();
                    command.CommandText = "CREATE TABLE AutoWeldHistories(id integer NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE, name string, task_name string, gangtao_type string, welding_item string, welding_current string, ar_flow string, room_temperature string, operator string, history_id integer, created_at timestamp, interupted boolean)";
                    command.ExecuteNonQuery();
                    command.CommandText = "CREATE TABLE GangTao(id integer NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE, type string UNIQUE)";
                    command.ExecuteNonQuery();
                    command.CommandText = "CREATE TABLE WeldingItem(id integer NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE, type string UNIQUE)";
                    command.ExecuteNonQuery();
                    command.CommandText = "CREATE TABLE Operator(id integer NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE, name string UNIQUE)";
                    command.ExecuteNonQuery();
                    command.CommandText = "CREATE TABLE Signal(id integer NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE, type integer, step integer, at timestamp, delta integer, history_id integer, auto_weld boolean)";
                    command.ExecuteNonQuery();
                    command.CommandText = "CREATE TABLE Template(id integer NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE, type string, item string, history_id integer)";
                    command.ExecuteNonQuery();
                    command.CommandText = "INSERT INTO GangTao ('type') values ('270'), ('242'), ('215'), ('190'), ('170'), ('150'), ('130'), ('110'), ('90'), ('75'), ('60'), ('46'), ('30')";
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

        public List<Template> TemplateList()
        {
            var templateList = new List<Template>();
            using (var conn = new SQLiteConnection(DataSource))
            {
                conn.Open();
                using (SQLiteCommand command = new SQLiteCommand(conn))
                {
                    var sql = "SELECT * FROM Template ORDER BY `id` DESC";
                    command.CommandText = sql;
                    var reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        var dict = new Dictionary<string, object>();
                        dict["type"] = reader.GetValue(1);
                        dict["item"] = reader.GetValue(2);
                        var history_id = reader.GetInt64(3);

                        var history = historyOfId(history_id);
                        var template = new Template(dict);
                        template.Id = reader.GetInt64(0);
                        template.History = history;

                        templateList.Add(template);
                    }
                }
            }

            return templateList;
        }

        public long saveTemplate(Template tpl)
        {
            using (var conn = new SQLiteConnection(DataSource))
            {
                conn.Open();
                using (SQLiteCommand command = new SQLiteCommand(conn))
                {
                    command.CommandText = "INSERT INTO Template ('type', 'item', 'history_id') values (@type, @item, @history_id)";
                    var typeParam = SQLiteHelper.CreateParameter("@type", tpl.Type, DbType.String);
                    var itemParam = SQLiteHelper.CreateParameter("@item", tpl.Item, DbType.String);
                    var historyIdParam = SQLiteHelper.CreateParameter("@history_id", tpl.History.Id, DbType.Int64);
                    command.Parameters.Add(typeParam);
                    command.Parameters.Add(itemParam);
                    command.Parameters.Add(historyIdParam);
                    command.ExecuteNonQuery();

                    // signal id
                    long tplId = -1;
                    var sql = "SELECT * FROM Template ORDER BY `id` DESC LIMIT 1"; // Not thread safe.
                    command.CommandText = sql;
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        tplId = reader.GetInt64(0);
                    }
                    reader.Close();
                    return tplId;
                }
            }
        }

        public void updateTemplate(Template tpl)
        {
            using (var conn = new SQLiteConnection(DataSource))
            {
                conn.Open();
                using (SQLiteCommand command = new SQLiteCommand(conn))
                {
                    command.CommandText = "UPDATE Template SET `type` = @type, `item` = @item, `history_id` = @history_id WHERE `id` = @tid";
                    var typeParam = SQLiteHelper.CreateParameter("@type", tpl.Type, DbType.String);
                    var itemParam = SQLiteHelper.CreateParameter("@item", tpl.Item, DbType.String);
                    var historyIdParam = SQLiteHelper.CreateParameter("@history_id", tpl.History.Id, DbType.Int64);
                    var tIdParam = SQLiteHelper.CreateParameter("@tid", tpl.Id, DbType.Int64);
                    command.Parameters.Add(typeParam);
                    command.Parameters.Add(itemParam);
                    command.Parameters.Add(historyIdParam);
                    command.Parameters.Add(tIdParam);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void deleteTemplate(Template tpl)
        {
            using (var conn = new SQLiteConnection(DataSource))
            {
                conn.Open();
                using (SQLiteCommand command = new SQLiteCommand(conn))
                {
                    command.CommandText = "DELETE FROM Template WHERE `id` = @tid";
                    var tIdParam = SQLiteHelper.CreateStringParameter("@tid", tpl.Id);
                    command.Parameters.Add(tIdParam);
                    command.ExecuteNonQuery();
                }
            }
        }

        public List<History> HistoryList()
        {
            var historyList = new List<History>();
            using (var conn = new SQLiteConnection(DataSource))
            {
                conn.Open();
                using (SQLiteCommand command = new SQLiteCommand(conn))
                {
                    var sql = "SELECT * FROM Histories ORDER BY `created_at` DESC";
                    command.CommandText = sql;
                    var reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        var dict = new Dictionary<string, object>();
                        dict["name"] = reader.GetValue(1);
                        dict["task_name"] = reader.GetValue(2);
                        dict["gangtao_type"] = reader.GetValue(3);
                        dict["welding_item"] =  reader.GetValue(4);
                        dict["welding_current"] = reader.GetValue(5);
                        dict["ar_flow"] = reader.GetValue(6);
                        dict["room_temperature"] = reader.GetValue(7);
                        dict["operator"] = reader.GetValue(8);
                        dict["created_at"] = reader.GetValue(9);

                        var history = new History(dict);
                        history.Id = reader.GetInt64(0);

                        historyList.Add(history);
                    }
                }
            }

            return historyList;
        }

        public List<AutoWeldHistory> AutoWeldHistoryList()
        {
            var historyList = new List<AutoWeldHistory>();
            using (var conn = new SQLiteConnection(DataSource))
            {
                conn.Open();
                using (SQLiteCommand command = new SQLiteCommand(conn))
                {
                    var sql = "SELECT * FROM AutoWeldHistories ORDER BY `created_at` DESC";
                    command.CommandText = sql;
                    var reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        var dict = new Dictionary<string, object>();
                        dict["name"] = reader.GetValue(1);
                        dict["task_name"] = reader.GetValue(2);
                        dict["gangtao_type"] = reader.GetValue(3);
                        dict["welding_item"] = reader.GetValue(4);
                        dict["welding_current"] = reader.GetValue(5);
                        dict["ar_flow"] = reader.GetValue(6);
                        dict["room_temperature"] = reader.GetValue(7);
                        dict["operator"] = reader.GetValue(8);
                        dict["history_id"] = reader.GetValue(9);
                        dict["created_at"] = reader.GetValue(10);
                        dict["interupted"] = reader.GetValue(11);

                        var autoWeldHistory = new AutoWeldHistory(dict);
                        autoWeldHistory.Id = reader.GetInt64(0);

                        historyList.Add(autoWeldHistory);
                    }
                }
            }

            return historyList;
        }

        public List<Signal> SignalListOfHistory(History history, bool auto_weld = false)
        {
            var signals = new List<Signal>();
            using (var conn = new SQLiteConnection(DataSource))
            {
                conn.Open();
                using (SQLiteCommand command = new SQLiteCommand(conn))
                {
                    var autoweld_value = auto_weld ? 1 : 0;
                    var sql = "SELECT * FROM Signal WHERE `history_id` = @hid and `auto_weld` = @autoweld ORDER BY `at` ASC ";
                    command.CommandText = sql;
                    var hIdParam = SQLiteHelper.CreateStringParameter("@hid", history.Id);
                    var autoweldParam = SQLiteHelper.CreateStringParameter("@autoweld", autoweld_value);
                    command.Parameters.Add(hIdParam);
                    command.Parameters.Add(autoweldParam);
                    var reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        var id = reader.GetInt64(0);
                        var type = reader.GetInt32(1);
                        var step = reader.GetInt32(2);
                        var at = (DateTime)reader.GetValue(3);
                        var delta = reader.GetInt32(4);

                        var signal = new Signal(type, step, at);
                        signal.Id = id;
                        signal.Delta = delta;
                        signal.History = history;
                        signals.Add(signal);
                    }

                    return signals;
                }
            }
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

        public long saveSignal(Signal sig)
        {
            using (var conn = new SQLiteConnection(DataSource))
            {
                conn.Open();
                using (SQLiteCommand command = new SQLiteCommand(conn))
                {
                    var auto_weld = sig.AutoWeld == true ? true : false;
                    command.CommandText = "INSERT INTO Signal ('type', 'at', 'step', 'delta', 'history_id', 'auto_weld') values (@type, @at, @step, @delta, @history_id, @auto_weld)";
                    var typeParam = SQLiteHelper.CreateParameter("@type", (int)sig.Type, DbType.Int32);
                    var atParam = SQLiteHelper.CreateParameter("@at", sig.Timestamp, DbType.DateTime);
                    var stepParam = SQLiteHelper.CreateParameter("@step", sig.Step, DbType.Int32);
                    var deltaParam = SQLiteHelper.CreateParameter("@delta", sig.Delta, DbType.Int32);
                    var historyIdParam = SQLiteHelper.CreateParameter("@history_id", sig.History.Id, DbType.Int64);
                    var autoWeldParam = SQLiteHelper.CreateParameter("@auto_weld", auto_weld, DbType.Boolean);
                    command.Parameters.Add(typeParam);
                    command.Parameters.Add(atParam);
                    command.Parameters.Add(deltaParam);
                    command.Parameters.Add(stepParam);
                    command.Parameters.Add(historyIdParam);
                    command.Parameters.Add(autoWeldParam);
                    command.ExecuteNonQuery();

                    // signal id
                    long sigId = -1;
                    var sql = "SELECT * FROM Signal ORDER BY `id` DESC LIMIT 1"; // Not thread safe.
                    command.CommandText = sql;
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        sigId = reader.GetInt64(0);
                    }
                    reader.Close();
                    return sigId;
                }
            }
        }

        public void updateSignal(Signal sig)
        {
            using (var conn = new SQLiteConnection(DataSource))
            {
                conn.Open();
                using (SQLiteCommand command = new SQLiteCommand(conn))
                {
                    command.CommandText = "UPDATE Signal SET `type` = @type `at` = @at, `step` = @step, `delta` = @delta, `history_id` = @history_id WHERE `id` = @sid";
                    var typeParam = SQLiteHelper.CreateParameter("@type", (int)sig.Type, DbType.Int32);
                    var atParam = SQLiteHelper.CreateParameter("@at", sig.Timestamp, DbType.DateTime);
                    var stepParam = SQLiteHelper.CreateParameter("@at", sig.Step, DbType.Int32);
                    var deltaParam = SQLiteHelper.CreateParameter("@delta", sig.Delta, DbType.Int32);
                    var historyIdParam = SQLiteHelper.CreateParameter("@history_id", sig.History.Id, DbType.Int64);
                    var sIdParam = SQLiteHelper.CreateParameter("@sid", sig.Id, DbType.Int64);
                    command.Parameters.Add(typeParam);
                    command.Parameters.Add(atParam);
                    command.Parameters.Add(deltaParam);
                    command.Parameters.Add(stepParam);
                    command.Parameters.Add(historyIdParam);
                    command.Parameters.Add(sIdParam);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void deleteSignal(Signal sig)
        {
            using (var conn = new SQLiteConnection(DataSource))
            {
                conn.Open();
                using (SQLiteCommand command = new SQLiteCommand(conn))
                {
                    command.CommandText = "DELETE FROM Signal WHERE `id` = @sid";
                    var sIdParam = SQLiteHelper.CreateStringParameter("@sid", sig.Id);
                    command.Parameters.Add(sIdParam);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void deleteOperator(string name)
        {
            using (var conn = new SQLiteConnection(DataSource))
            {
                conn.Open();
                using (SQLiteCommand command = new SQLiteCommand(conn))
                {
                    command.CommandText = "DELETE FROM Operator WHERE `name` = @name";
                    var nameParam = SQLiteHelper.CreateStringParameter("@name", name);
                    command.Parameters.Add(nameParam);
                    command.ExecuteNonQuery();
                }
            }
        }

        public long saveHistory(History history)
        {
            using (var conn = new SQLiteConnection(DataSource))
            {
                conn.Open();
                using (SQLiteCommand command = new SQLiteCommand(conn))
                {
                    // history
                    command.CommandText = "INSERT INTO Histories ('name', 'task_name', 'gangtao_type', 'welding_item', 'welding_current', 'ar_flow', 'room_temperature', 'operator', 'created_at') values (@name, @task_name, @gangtao_type, @welding_item, @welding_current, @ar_flow, @room_temperature, @operator, @created_at)";
                    var nameParam = SQLiteHelper.CreateStringParameter("@name", history.Name);
                    var taskNameParam = SQLiteHelper.CreateStringParameter("@task_name", history.TaskName);
                    var gangtaoTypeParam = SQLiteHelper.CreateStringParameter("@gangtao_type", history.GangtaoType);
                    var WeldingItemParam = SQLiteHelper.CreateStringParameter("@welding_item", history.WeldingItem);
                    var WeldingCurrentParam = SQLiteHelper.CreateStringParameter("@welding_current", history.WeldingCurrent);
                    var ARFlowParam = SQLiteHelper.CreateStringParameter("@ar_flow", history.ArFlow);
                    var RoomTemperatureParam = SQLiteHelper.CreateStringParameter("@room_temperature", history.RoomTemperature);
                    var OperatorParam = SQLiteHelper.CreateStringParameter("@operator", history.OperatorName);
                    var CreatedAtParam = SQLiteHelper.CreateParameter("@created_at", history.CreatedAt, DbType.DateTime);
                    command.Parameters.Add(nameParam);
                    command.Parameters.Add(taskNameParam);
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

                    var sql = "SELECT * FROM Histories ORDER BY `created_at` DESC LIMIT 1"; // Not thread safe.
                    command.CommandText = sql;
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        historyId = reader.GetInt64(0);
                    }
                    reader.Close();
                    return historyId;
                }
            }
        }

        public void updateHistory(History history)
        {
            using (var conn = new SQLiteConnection(DataSource))
            {
                conn.Open();
                using (SQLiteCommand command = new SQLiteCommand(conn))
                {
                    // history
                    command.CommandText = "UPDATE Histories SET `name` = @name, `task_name` = @task_name, `gangtao_type` = @gangtao_type, `welding_item` = @welding_item, `welding_current` = @welding_current, `ar_flow` = @ar_flow, `room_temperature` = @room_temperature, `operator` = @operator, `created_at` = @created_at WHERE `id` = @hid";
                    var nameParam = SQLiteHelper.CreateStringParameter("@name", history.Name);
                    var taskNameParam = SQLiteHelper.CreateStringParameter("@task_name", history.TaskName);
                    var gangtaoTypeParam = SQLiteHelper.CreateStringParameter("@gangtao_type", history.GangtaoType);
                    var WeldingItemParam = SQLiteHelper.CreateStringParameter("@welding_item", history.WeldingItem);
                    var WeldingCurrentParam = SQLiteHelper.CreateStringParameter("@welding_current", history.WeldingCurrent);
                    var ARFlowParam = SQLiteHelper.CreateStringParameter("@ar_flow", history.ArFlow);
                    var RoomTemperatureParam = SQLiteHelper.CreateStringParameter("@room_temperature", history.RoomTemperature);
                    var OperatorParam = SQLiteHelper.CreateStringParameter("@operator", history.OperatorName);
                    var CreatedAtParam = SQLiteHelper.CreateParameter("@created_at", history.CreatedAt, DbType.DateTime);
                    var hIdParam = SQLiteHelper.CreateParameter("@hid", history.Id, DbType.Int64);
                    command.Parameters.Add(nameParam);
                    command.Parameters.Add(taskNameParam);
                    command.Parameters.Add(gangtaoTypeParam);
                    command.Parameters.Add(WeldingItemParam);
                    command.Parameters.Add(WeldingCurrentParam);
                    command.Parameters.Add(ARFlowParam);
                    command.Parameters.Add(RoomTemperatureParam);
                    command.Parameters.Add(OperatorParam);
                    command.Parameters.Add(CreatedAtParam);
                    command.Parameters.Add(hIdParam);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void deleteHistory(History history)
        {
            using (var conn = new SQLiteConnection(DataSource))
            {
                conn.Open();
                using (SQLiteCommand command = new SQLiteCommand(conn))
                {
                    // history
                    command.CommandText = "DELETE FROM Histories WHERE `id` = @hid";
                    var hIdParam = SQLiteHelper.CreateStringParameter("@hid", history.Id);
                    command.Parameters.Add(hIdParam);
                    command.ExecuteNonQuery();

                    command.CommandText = "DELETE FROM Signal WHERE `history_id` = @hid";
                    command.Parameters.Add(hIdParam);
                    command.ExecuteNonQuery();
                }
            }
        }

        public History historyOfId(long history_id)
        {
            using (var conn = new SQLiteConnection(DataSource))
            {
                conn.Open();
                using (SQLiteCommand command = new SQLiteCommand(conn))
                {
                    var sql = "SELECT * FROM Histories WHERE `id` = @hid";
                    command.CommandText = sql;
                    var hIdParam = SQLiteHelper.CreateStringParameter("@hid", history_id);
                    command.Parameters.Add(hIdParam);

                    var reader = command.ExecuteReader();
                    History history = null;
                    while (reader.Read())
                    {
                        var dict = new Dictionary<string, object>();
                        dict["name"] = reader.GetValue(1);
                        dict["task_name"] = reader.GetValue(2);
                        dict["gangtao_type"] = reader.GetValue(3);
                        dict["welding_item"] = reader.GetValue(4);
                        dict["welding_current"] = reader.GetValue(5);
                        dict["ar_flow"] = reader.GetValue(6);
                        dict["room_temperature"] = reader.GetValue(7);
                        dict["operator"] = reader.GetValue(8);
                        dict["created_at"] = reader.GetValue(9);

                        history = new History(dict);
                        history.Id = reader.GetInt64(0);
                    }

                    return history;
                }
            }
        }

        public long saveAutoWeldHistory(AutoWeldHistory history)
        {
            using (var conn = new SQLiteConnection(DataSource))
            {
                conn.Open();
                using (SQLiteCommand command = new SQLiteCommand(conn))
                {
                    // history
                    var interupted = history.Interupted == false ? false : true;
                    command.CommandText = "INSERT INTO AutoWeldHistories ('name', 'task_name', 'gangtao_type', 'welding_item', 'welding_current', 'ar_flow', 'room_temperature', 'operator', 'history_id', 'created_at', 'interupted') values (@name, @task_name, @gangtao_type, @welding_item, @welding_current, @ar_flow, @room_temperature, @operator, @history_id, @created_at, @interupted)";
                    var nameParam = SQLiteHelper.CreateStringParameter("@name", history.Name);
                    var taskNameParam = SQLiteHelper.CreateStringParameter("@task_name", history.TaskName);
                    var gangtaoTypeParam = SQLiteHelper.CreateStringParameter("@gangtao_type", history.GangtaoType);
                    var WeldingItemParam = SQLiteHelper.CreateStringParameter("@welding_item", history.WeldingItem);
                    var WeldingCurrentParam = SQLiteHelper.CreateStringParameter("@welding_current", history.WeldingCurrent);
                    var ARFlowParam = SQLiteHelper.CreateStringParameter("@ar_flow", history.ArFlow);
                    var RoomTemperatureParam = SQLiteHelper.CreateStringParameter("@room_temperature", history.RoomTemperature);
                    var OperatorParam = SQLiteHelper.CreateStringParameter("@operator", history.OperatorName);
                    var HistoryIdParam = SQLiteHelper.CreateStringParameter("@history_id", history.Template.Id);
                    var CreatedAtParam = SQLiteHelper.CreateParameter("@created_at", history.CreatedAt, DbType.DateTime);
                    var InteruptedParam = SQLiteHelper.CreateParameter("@interupted", interupted, DbType.Boolean);
                    command.Parameters.Add(nameParam);
                    command.Parameters.Add(taskNameParam);
                    command.Parameters.Add(gangtaoTypeParam);
                    command.Parameters.Add(WeldingItemParam);
                    command.Parameters.Add(WeldingCurrentParam);
                    command.Parameters.Add(ARFlowParam);
                    command.Parameters.Add(RoomTemperatureParam);
                    command.Parameters.Add(OperatorParam);
                    command.Parameters.Add(HistoryIdParam);
                    command.Parameters.Add(CreatedAtParam);
                    command.Parameters.Add(InteruptedParam);
                    command.ExecuteNonQuery();

                    // history id
                    long historyId = -1;

                    var sql = "SELECT * FROM AutoWeldHistories ORDER BY `created_at` DESC LIMIT 1"; // Not thread safe.
                    command.CommandText = sql;
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        historyId = reader.GetInt64(0);
                    }
                    reader.Close();
                    return historyId;
                }
            }
        }

        public void updateAutoWeldHistory(AutoWeldHistory history)
        {
            using (var conn = new SQLiteConnection(DataSource))
            {
                conn.Open();
                using (SQLiteCommand command = new SQLiteCommand(conn))
                {
                    // history
                    command.CommandText = "UPDATE AutoWeldHistories SET `name` = @name, `task_name` = @task_name, `gangtao_type` = @gangtao_type, `welding_item` = @welding_item, `welding_current` = @welding_current, `ar_flow` = @ar_flow, `room_temperature` = @room_temperature, `operator` = @operator, `created_at` = @created_at WHERE `id` = @hid";
                    var nameParam = SQLiteHelper.CreateStringParameter("@name", history.Name);
                    var taskNameParam = SQLiteHelper.CreateStringParameter("@task_name", history.TaskName);
                    var gangtaoTypeParam = SQLiteHelper.CreateStringParameter("@gangtao_type", history.GangtaoType);
                    var WeldingItemParam = SQLiteHelper.CreateStringParameter("@welding_item", history.WeldingItem);
                    var WeldingCurrentParam = SQLiteHelper.CreateStringParameter("@welding_current", history.WeldingCurrent);
                    var ARFlowParam = SQLiteHelper.CreateStringParameter("@ar_flow", history.ArFlow);
                    var RoomTemperatureParam = SQLiteHelper.CreateStringParameter("@room_temperature", history.RoomTemperature);
                    var OperatorParam = SQLiteHelper.CreateStringParameter("@operator", history.OperatorName);
                    var CreatedAtParam = SQLiteHelper.CreateParameter("@created_at", history.CreatedAt, DbType.DateTime);
                    var hIdParam = SQLiteHelper.CreateParameter("@hid", history.Id, DbType.DateTime);
                    command.Parameters.Add(nameParam);
                    command.Parameters.Add(taskNameParam);
                    command.Parameters.Add(gangtaoTypeParam);
                    command.Parameters.Add(WeldingItemParam);
                    command.Parameters.Add(WeldingCurrentParam);
                    command.Parameters.Add(ARFlowParam);
                    command.Parameters.Add(RoomTemperatureParam);
                    command.Parameters.Add(OperatorParam);
                    command.Parameters.Add(CreatedAtParam);
                    command.Parameters.Add(hIdParam);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void deleteAutoWeldHistory(AutoWeldHistory history)
        {
            using (var conn = new SQLiteConnection(DataSource))
            {
                conn.Open();
                using (SQLiteCommand command = new SQLiteCommand(conn))
                {
                    // history
                    command.CommandText = "DELETE FROM AutoWeldHistories WHERE `id` = @hid";
                    var hIdParam = SQLiteHelper.CreateStringParameter("@hid", history.Id);
                    command.Parameters.Add(hIdParam);
                    command.ExecuteNonQuery();
                }
            }
        }

        public AutoWeldHistory autoWeldHistoryOfId(long history_id)
        {
            using (var conn = new SQLiteConnection(DataSource))
            {
                conn.Open();
                using (SQLiteCommand command = new SQLiteCommand(conn))
                {
                    var sql = "SELECT * FROM AutoWeldHistories ORDER BY `created_at` DESC WHERE id=@hid";
                    command.CommandText = sql;
                    var hIdParam = SQLiteHelper.CreateStringParameter("@hid", history_id);
                    command.Parameters.Add(hIdParam);

                    var reader = command.ExecuteReader();
                    AutoWeldHistory history = null;
                    while (reader.Read())
                    {
                        var dict = new Dictionary<string, object>();
                        dict["name"] = reader.GetValue(1);
                        dict["task_name"] = reader.GetValue(2);
                        dict["gangtao_type"] = reader.GetValue(3);
                        dict["welding_item"] = reader.GetValue(4);
                        dict["welding_current"] = reader.GetValue(5);
                        dict["ar_flow"] = reader.GetValue(6);
                        dict["room_temperature"] = reader.GetValue(7);
                        dict["operator"] = reader.GetValue(8);
                        dict["history_id"] = reader.GetValue(9);
                        dict["created_at"] = reader.GetValue(10);
                        dict["interupted"] = reader.GetValue(11);

                        history = new AutoWeldHistory(dict);
                        history.Id = reader.GetInt64(0);
                    }

                    return history;
                }
            }
        }
    }
}
