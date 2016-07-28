using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Welding_Recorder
{
    public class AutoWeldHistory : History
    {
        public History Template { get; set; }

        public AutoWeldHistory(Dictionary<string, object> meta) : base(meta)
        {
            long hid = (long)meta["history_id"];
            Template = History.Find(hid);
        }

        public new AutoWeldHistory Save()
        {
            var db = new DataProcess();
            if (Id == null)
            {
                Id = db.saveAutoWeldHistory(this);
            }
            else
            {
                db.updateAutoWeldHistory(this);
            }

            return this;
        }

        public override void Delete()
        {
            var db = new DataProcess();
            db.deleteAutoWeldHistory(this);
        }
        
        public override string ToString()
        {
            var signals = Template.Signals;
            var message = "自动控制记录详细信息\r\n\r\n";
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

            for (int n = 0; n < signals.Count; n++)
            {
                var signal = signals[n];
                message += string.Format("{0}. {1}, 开始于 {2:N3} 秒后\r\n", n + 1, signal.ToString(), signal.Delta / 1000.0);
            }

            return message;
        }

        public static new AutoWeldHistory Find(long history_id)
        {
            var db = new DataProcess();
            var history = db.autoWeldHistoryOfId(history_id);
            return history;
        }
    }
}
