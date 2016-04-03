using System;
using System.Collections.Generic;

namespace Welding_Recorder
{
    public enum SignalType
    {
        ArcStart = 0,
        ArcEnd,
        SolderStart,
        SolderEnd,
        Acceleration,
        Deceleration,
        RotateStart, //正转
        RotateEnd,
        RevolveStart, //反转
        RevolveEnd,
        Unknown = -1 // -1 is much simpler than int.MaxValue
    }

    // Signal reading from chip
    public class Signal
    {
        public static int LENGTH = 7;
        public long? Id { get; set; }
        private DateTime timestamp;
        private byte[] rawBytes;
        public byte[] RawBytes
        {
            get
            {
                return rawBytes;
            }
        }

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
                    case 0x40:
                        t = SignalType.SolderStart;
                        break;
                    case 0x20:
                        t = SignalType.SolderEnd;
                        break;
                    case 0x04:
                        t = SignalType.Acceleration;
                        break;
                    case 0x02:
                        t = SignalType.Deceleration;
                        break;
                    case 0x90:
                        t = SignalType.RotateStart;
                        break;
                    case 0x60:
                        t = SignalType.RotateEnd;
                        break;
                    case 0x80:
                        t = SignalType.RevolveStart;
                        break;
                    case 0x70:
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
                if (Type == SignalType.Acceleration || Type == SignalType.Deceleration)
                {
                    return rawBytes[4];
                }
                else
                {
                    return 0;
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

        public History History { get; set; }
        public int Delta { get; set; }

        public Signal(byte[] rawBytes, DateTime timestamp)
        {
            this.rawBytes = rawBytes;
            this.timestamp = timestamp;
        }

        public Signal(byte[] rawBytes) : this(rawBytes, DateTime.Now) { }

        public Signal(int typeRaw, int step, DateTime ts)
        {
            List<byte> bytes = new List<byte>();
            bytes.Add(0xFF);bytes.Add(0x01);bytes.Add(0x00);
            SignalType t = (SignalType)typeRaw;
            switch (t)
            {
                case SignalType.ArcStart:
                    bytes.Add(0x08);
                    break;
                case SignalType.ArcEnd:
                    bytes.Add(0x10);
                    break;
                case SignalType.SolderStart:
                    bytes.Add(0x40);
                    break;
                case SignalType.SolderEnd:
                    bytes.Add(0x20);
                    break;
                case SignalType.Acceleration:
                    bytes.Add(0x04);
                    break;
                case SignalType.Deceleration:
                    bytes.Add(0x02);
                    break;
                case SignalType.RotateStart:
                    bytes.Add(0x90);
                    break;
                case SignalType.RotateEnd:
                    bytes.Add(0x60);
                    break;
                case SignalType.RevolveStart:
                    bytes.Add(0x40);
                    break;
                case SignalType.RevolveEnd:
                    bytes.Add(0x20);
                    break;
                default:
                    bytes.Add(0x00); // ??
                    break;
            }
            if (t == SignalType.Acceleration || t == SignalType.Deceleration)
            {
                bytes.Add((byte)step);
            }
            else
            {
                bytes.Add(0x00);
            }
            bytes.Add(0x00); // Reserved byte
            byte seventhByte = (byte)(bytes[1] ^ bytes[2] ^ bytes[3] ^ bytes[4] ^ bytes[5]);
            bytes.Add(seventhByte);

            rawBytes = bytes.ToArray();
            timestamp = ts;
        }

        public bool isValid()
        {
            if ((rawBytes[1] ^ rawBytes[2] ^ rawBytes[3] ^ rawBytes[4] ^ rawBytes[5]) == rawBytes[6])
            {
                return true;
            }
            return false;
        }
        
        public override string ToString()
        {
            var name = "未知信号";
            switch (Type)
            {
                case SignalType.ArcStart:
                    name = "起弧";
                    break;
                case SignalType.ArcEnd:
                    name = "起弧停止";
                    break;
                case SignalType.SolderStart:
                    name = "焊接开始";
                    break;
                case SignalType.SolderEnd:
                    name = "焊接停止";
                    break;
                case SignalType.Acceleration:
                    name = "加速至" + Step + "档";
                    break;
                case SignalType.Deceleration:
                    name = "减速至" + Step + "档";
                    break;
                case SignalType.RotateStart:
                    name = "正转";
                    break;
                case SignalType.RotateEnd:
                    name = "正传停止";
                    break;
                case SignalType.RevolveStart:
                    name = "反转";
                    break;
                case SignalType.RevolveEnd:
                    name = "反转停止";
                    break;
                case SignalType.Unknown:
                default:
                    break;
            }
            return name;
        }

        public Signal Save()
        {
            var db = new DataProcess();
            if (History == null)
            {
                throw new SignalException("Signal should be associated to history.");
            }
            else
            {
                if (this.Id == null)
                {
                    Id = db.saveSignal(this);
                }
                else
                {
                    db.updateSignal(this);
                }
                return this;
            }
        }
    }
    
    // Place holder exception class
    public class SignalException : Exception
    {
        public SignalException() : base() { }
        public SignalException(string message) : base(message) { }
        public SignalException(string message, Exception e) : base(message, e) { }
    }

}
