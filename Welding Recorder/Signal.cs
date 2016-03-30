﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

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
        RevolveStart,
        RevolveEnd,
        Unknown = Int32.MaxValue
    }

    // Signal reading from chip
    public class Signal
    {
        private long? id = null;
        public long? Id { get; set; }
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
                        t = (rawBytes[4] == 0x00) ? SignalType.SolderStart : SignalType.Acceleration;
                        break;
                    case 0x02:
                        t = (rawBytes[4] == 0x00) ? SignalType.SolderEnd : SignalType.Deceleration;
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
                if (Type == SignalType.Acceleration || Type == SignalType.Deceleration)
                {
                    return rawBytes[4];
                }
                else
                {
                    return int.MinValue;
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
        private int delta = 0;
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
                case SignalType.Acceleration:
                    bytes.Add(0x04);
                    break;
                case SignalType.SolderEnd:
                case SignalType.Deceleration:
                    bytes.Add(0x02);
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
            byte sixthByte = (byte)(bytes[1] ^ bytes[2] ^ bytes[3] ^ bytes[4]);
            bytes.Add(sixthByte);

            rawBytes = bytes.ToArray();
            timestamp = ts;
        }

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
