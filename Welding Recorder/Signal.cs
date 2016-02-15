using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SerialReader
{
    // Signal reading from chip
    class Signal
    {
        public enum SignalType
        {
            ArcStart = 0,
            ArcEnd,
            SolderStart,
            SolderEnd,
            Acceleration,
            Deceleration,
            RevolveStart,
            RevolveEnd,
            Unknown = Int32.MaxValue
        }

        private DateTime timestamp;
        private byte[] rawBytes;

        public SignalType Type
        {
            get
            {
                byte typeByte = rawBytes[3];
                SignalType t = SignalType.Unknown;
                switch (typeByte)
                {
                    case 0x08:
                        t = SignalType.ArcStart;
                        break;
                    case 0x10:
                        t = SignalType.ArcEnd;
                        break;
                    case 0x04:
                        t = (rawBytes[4] == 0x00) ? SignalType.ArcStart : t = SignalType.Acceleration;
                        break;
                    case 0x02:
                        t = (rawBytes[4] == 0x00) ? SignalType.ArcEnd : t = SignalType.Deceleration;
                        break;
                    case 0x40:
                        t = SignalType.RevolveStart;
                        break;
                    case 0x20:
                        t = SignalType.RevolveEnd;
                        break;
                    default:
                        t = SignalType.Unknown;
                        break;
                }
                return t;
            }
        }

        public int Step
        {
            get
            {
                if (Type == SignalType.Acceleration)
                {
                    return rawBytes[4];
                }
                else
                {
                    return -1;
                }
            }
        }

        public DateTime Timestamp
        {
            get
            {
                return timestamp;
            }
        }

        public Signal(byte[] rawBytes, DateTime timestamp)
        {
            this.rawBytes = rawBytes;
            this.timestamp = timestamp;
        }

        public Signal(byte[] rawBytes) : this(rawBytes, DateTime.Now) { }

        public bool isValid()
        {
            if ((rawBytes[1] ^ rawBytes[2] ^ rawBytes[3] ^ rawBytes[4]) == rawBytes[5])
            {
                return true;
            }
            return false;
        }

        public override string ToString()
        {
            return this.Type.ToString() + " " + ((this.Step != int.MaxValue) ? -1 : this.Step) + " " + (this.isValid() ? "Valid signal" : "Invalid signal");
        }
    }
}
