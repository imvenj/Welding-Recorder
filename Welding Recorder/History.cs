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
                for (int i = 0; i < Signals.Count; i++)
                {
                    var sig = Signals[i];
                    var delta = 0;
                    if (i != 0) //first
                    {
                        var interval = Signals[i].Timestamp - Signals[i - 1].Timestamp;
                        delta = (int)interval.TotalMilliseconds; // Ignore time less tham 1ms.
                    }
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
            message += "焊接流程：\r\n";
            message += "\r\n";

            for (int i = 0; i < signals.Count; i++)
            {
                var signal = signals[i];
                message += string.Format("{0}. {1}, 开始于 {2:N3} 秒后\r\n", i + 1, signal.ToString(), signal.Delta / 1000.0);
            }
            return message;
        }
    }
}
