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
        public List<List<Signal>> SignalGroups //grouping
        {
            get
            {
                var signalCount = Signals.Count;
                List<List<Signal>> signalGroups = new List<List<Signal>>();
                List<Signal> currentGroup = new List<Signal>();
                for (int i = 0; i < signalCount; i++)
                {
                    var sig = Signals[i];
                    
                    currentGroup.Add(sig);
                    
                    if (sig.Type == SignalType.SolderEnd)
                    {
                        signalGroups.Add(currentGroup);
                        currentGroup = new List<Signal>();
                    }
                }
                return signalGroups;
            }
        }

        public History(Dictionary<string, object> meta)
        {
            Name = (string)meta["name"];
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

                foreach (var group in SignalGroups)
                {
                    for (int i = 0; i < group.Count; i++)
                    {
                        var sig = group[i];
                        sig.Delta = 0;

                        if (i != 0)
                        {
                            var interval = group[i].Timestamp - group[0].Timestamp;
                            var delta = (int)interval.TotalMilliseconds; // Ignore time less tham 1ms.
                            sig.Delta = delta;
                        }
                        
                        sig.History = this;
                        sig.Save();
                    }
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

        public override string ToString()
        {
            var signals = Signals;
            var message = "焊接记录详细信息\r\n\r\n";
            message += string.Format("本记录包含{0}条焊接指令。\r\n\r\n", signals.Count);

            var formatString = "{0} {1} \r\n";
            message += string.Format(formatString, "  记录名：", Name);
            message += string.Format(formatString, "  操作人：", OperatorName);
            var timeString = CreatedAt.ToLongDateString() + CreatedAt.ToLongTimeString();
            message += string.Format(formatString, "焊接时间：", timeString);
            message += string.Format(formatString, "焊接项目：", WeldingItem);
            message += string.Format(formatString, "缸套规格：", GangtaoType);
            message += string.Format(formatString, "焊接电流：", WeldingCurrent + "A");
            message += string.Format(formatString, "室内温度：", RoomTemperature + "℃");
            message += string.Format(formatString, "氩气流量：", ArFlow + "L/min");
            message += "\r\n";
            message += string.Format("焊接流程：（{0}个子过程）\r\n", SignalGroups.Count);
            message += "\r\n";

            for (int n = 0; n < SignalGroups.Count; n++)
            {
                message += string.Format("子过程{0}:\r\n", n + 1);
                var group = SignalGroups[n];
                for (int i = 0; i < group.Count; i++)
                {
                    var signal = group[i];
                    message += string.Format("{0}. {1}, 开始于 {2:N3} 秒后\r\n", i + 1, signal.ToString(), signal.Delta / 1000.0);
                }
            }
            
            return message;
        }
    }
}
