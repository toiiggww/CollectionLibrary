using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TEArts.Etc.CollectionLibrary
{
    public struct ByteFinger
    {
        private static ByteFinger mbrEmpty = ByteFinger.New();

        public byte[] Header
        {
            get;
            private set;
        }

        public byte[] PerfixIndex
        {
            get;
            private set;
        }

        public byte[] PerfixPercent
        {
            get;
            private set;
        }

        public byte[] PostfixIndex
        {
            get;
            private set;
        }

        public byte[] PostPercent
        {
            get;
            private set;
        }

        public byte[] Tailer
        {
            get;
            private set;
        }

        public FailPostion Postion
        {
            get;
            set;
        }

        public static ByteFinger Empty
        {
            get
            {
                return ByteFinger.mbrEmpty;
            }
        }

        public static ByteFinger New()
        {
            return new ByteFinger
            {
                Header = new byte[8],
                PerfixIndex = new byte[4],
                PerfixPercent = new byte[4],
                PostfixIndex = new byte[4],
                PostPercent = new byte[4],
                Tailer = new byte[8]
            };
        }

        public byte[] Concate()
        {
            byte[] result = new byte[32];
            Buffer.BlockCopy(this.Header, 0, result, 0, 8);
            Buffer.BlockCopy(this.PerfixIndex, 0, result, 8, 4);
            Buffer.BlockCopy(this.PerfixPercent, 0, result, 12, 4);
            Buffer.BlockCopy(this.PostfixIndex, 0, result, 16, 4);
            Buffer.BlockCopy(this.PostPercent, 0, result, 20, 4);
            Buffer.BlockCopy(this.Header, 0, result, 24, 8);
            return result;
        }

        public override int GetHashCode()
        {
            return this.Header.GetHashCode() ^ this.PerfixIndex.GetHashCode() ^ this.PerfixPercent.GetHashCode() ^ this.PostfixIndex.GetHashCode() ^ this.PostPercent.GetHashCode() ^ this.Tailer.GetHashCode();
        }

        public override string ToString()
        {
            return this.Postion.ToString() + "\t" + BiteArray.ArrayToHexString(this.Concate());
        }
    }
    public enum FailPostion
    {
        Successed,
        Header,
        PerfixIndex,
        PerfixPercent,
        PostfixIndex,
        PostPercent,
        Tailer
    }
}
