using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Welding_Recorder
{
    public class History
    {
        public long? Id { get; set; }
        public string Name { get; set; }
        public string TaskName { get; set; }
        public string GangtaoType { get; set; }
        public string WeldingItem { get; set; }
        public string WeldingCurrent { get; set; }
        public string ArFlow { get; set; }
        public string RoomTemperature { get; set; }
        public string OperatorName { get; set; }
        public DateTime CreatedAt { get; set; }
        private List<Signal> signals;
        public List<Signal> Signals {
            get
            {
                if (signals == null)
                {
                    var db = new DataProcess();
                    signals = db.SignalListOfHistory(this);
                }

                return signals;
            }
            set
            {
                signals = value;
            }
        }

        public History(Dictionary<string, object> meta)
        {
            Name = (string)meta["name"];
            TaskName = (string)meta["task_name"];
            GangtaoType = (string)meta["gangtao_type"];
            WeldingItem = (string)meta["welding_item"];
            WeldingCurrent = (string)meta["welding_current"];
            ArFlow = (string)meta["ar_flow"];
            RoomTemperature = (string)meta["room_temperature"];
            OperatorName = (string)meta["operator"];
            CreatedAt = (DateTime)meta["created_at"];
        }

        public History Save()
        {
            var db = new DataProcess();
            if (Id == null)
            {
                Id = db.saveHistory(this);

                foreach (var sig in Signals)
                {
                    sig.Delta = 0;

                    var interval = sig.Timestamp - Signals[0].Timestamp;
                    var delta = (int)interval.TotalMilliseconds; // Ignore time less tham 1ms.
                    sig.Delta = delta;
                    sig.History = this;
                    sig.Save();
                }
            }
            else
            {
                db.updateHistory(this);
            }

            return this;
        }

        public void ReloadSignals()
        {
            signals = null;
        }

        public virtual void Delete()
        {
            var db = new DataProcess();
            db.deleteHistory(this);
        }

        public override string ToString()
        {
            var signals = Signals;
            var message = "焊接记录详细信息\r\n\r\n";
            message += string.Format("本记录包含{0}条焊接指令。\r\n\r\n", signals.Count);

            var formatString = "{0} {1} \r\n";
            message += string.Format(formatString, "  记录名：", Name);
            message += string.Format(formatString, "任务书号：", TaskName);
            message += string.Format(formatString, "  操作人：", OperatorName);
            var timeString = CreatedAt.ToLongDateString() + CreatedAt.ToLongTimeString();
            message += string.Format(formatString, "焊接时间：", timeString);
            message += string.Format(formatString, "焊接项目：", WeldingItem);
            message += string.Format(formatString, "缸套规格：", GangtaoType);
            message += string.Format(formatString, "焊接电流：", WeldingCurrent + "A");
            message += string.Format(formatString, "室内温度：", RoomTemperature + "℃");
            message += string.Format(formatString, "氩气流量：", ArFlow + "L/min");
            message += "\r\n";

            for (int n = 0; n < Signals.Count; n++)
            {
                var signal = Signals[n];
                message += string.Format("{0}. {1}, 开始于 {2:N3} 秒后\r\n", n + 1, signal.ToString(), signal.Delta / 1000.0);
            }
            
            return message;
        }

        public string ShortDescription()
        {
            var timeString = CreatedAt.ToLongDateString() + CreatedAt.ToLongTimeString();
            var name = (Name == "") ? "(未命名)" : Name;
            var item = (WeldingItem == "") ? "未知项目" : WeldingItem;
            var type = (GangtaoType == "") ? "?" : GangtaoType;
            var task = (TaskName == "") ? "无" : TaskName;
            return string.Format("{0}: {1}({2}), 任务书号：{3}, 记录于：{4}", name, item, type, task, timeString);
        }

        public static History Find(long history_id)
        {
            var db = new DataProcess();
            var history = db.historyOfId(history_id);
            return history;
        }
        
        public static History LatestHistory()
        {
            var db = new DataProcess();
            var histories = db.HistoryList();
            var sortedHistories = from h in histories where true orderby h.CreatedAt descending select h;
            if (sortedHistories.Count() == 0)
            {
                return null;
            }
            else
            {
                return sortedHistories.First();
            }
        }
    }
}
