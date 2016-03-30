using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Welding_Recorder
{
    public class History
    {
        private long? id = null;
        public long? Id { get; set; }
        public string Name { get; set; }
        private string gangtaoType;
        public string GangtaoType { get; set; }
        private string weldingItem;
        public string WeldingItem { get; set; }
        private string weldingCurrent;
        public string WeldingCurrent { get; set; }
        private string arFlow;
        public string ArFlow { get; set; }
        private string roomTemperature;
        public string RoomTemperature { get; set; }
        private string operatorName;
        public string OperatorName { get; set; }
        private DateTime createdAt;
        public DateTime CreatedAt { get; set; }
        public List<Signal> signals;

        public History(Dictionary<string, object> meta)
        {
            gangtaoType = (string)meta["gangtao_type"];
            weldingItem = (string)meta["welding_item"];
            weldingCurrent = (string)meta["welding_current"];
            arFlow = (string)meta["ar_flow"];
            roomTemperature = (string)meta["room_temperature"];
            operatorName = (string)meta["operator"];
            createdAt = (DateTime)meta["created_at"];
        }

        public History Save()
        {
            var db = new DataProcess();
            if (Id == null)
            {
                Id = db.saveHistory(this);
                for (int i = 0; i < signals.Count; i++)
                {
                    var sig = signals[i];
                    var delta = 0;
                    if (i != 0) //first
                    {
                        var interval = signals[i].Timestamp - signals[i - 1].Timestamp;
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
