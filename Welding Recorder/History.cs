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
        public List<Signal> Signals { get; set; }

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
        

    }
}
