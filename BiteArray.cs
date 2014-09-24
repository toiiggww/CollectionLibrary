using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CollectionLibrary
{
    public abstract class BiteArray
    {
        public static short GetInt16(byte[] bytes, int offset)
        {
            if (offset + 1 <= bytes.Length)
            {
                return GetInt16(new byte[] { bytes[offset], bytes[offset + 1] });
            }
            else
            {
                throw new ArgumentException("Array");
            }
        }
        public static short GetInt16(byte[] bytes)
        {
            if (bytes.Length == 2)
            {
                return (short)(
                    (bytes[1] >> 4) * 0x1000 +
                    (bytes[1] & 0xf) * 0x100 +
                    (bytes[0] >> 4) * 0x10 +
                    (bytes[0] & 0xf) // * (Math.Pow(0xf, 0)))
                    );
            }
            else
            {
                throw new ArgumentException("Array");
            }
        }
        public static string GetString(byte[] bytes, int offset)
        {
            return GetString(bytes, offset, Encoding.ASCII);
        }
        public static string GetString(byte[] bytes, int offset, Encoding e)
        {
            if (offset + 4 > bytes.Length)
            {
                throw new IndexOutOfRangeException(offset.ToString());
            }
            int s = GetInt32(new byte[] { bytes[offset], bytes[offset + 1], bytes[offset + 2], bytes[offset + 3] });
            if (s == 0)
            {
                return "";
            }
            else if (s > 0)
            {
                if (bytes.Length - 4 - offset < s)
                {
                    s = bytes.Length - 4 - offset;
                }
                return e.GetString(bytes, offset + 4, s);
            }
            else
            {
                throw new ArgumentOutOfRangeException("array");
            }
        }
        public static int GetInt32(byte[] bytes, int offset)
        {
            if (offset + 3 <= bytes.Length)
            {
                return GetInt32(new byte[] { bytes[offset], bytes[offset + 1], bytes[offset + 2], bytes[offset + 3] });
            }
            else
            {
                throw new ArgumentOutOfRangeException("array");
            }
        }
        public static int GetInt32(byte[] bytes)
        {
            if (bytes.Length == 4)
            {
                return (
                    (bytes[3] >> 4) * 0x10000000 +
                    (bytes[3] & 0xf) * 0x1000000 +
                    (bytes[2] >> 4) * 0x100000 +
                    (bytes[2] & 0xf) * 0x10000 +
                    (bytes[1] >> 4) * 0x1000 +
                    (bytes[1] & 0xf) * 0x100 +
                    (bytes[0] >> 4) * 0x10 +
                    (bytes[0] & 0xf)
                    );
            }
            else
            {
                throw new ArgumentException("Array");
            }
        }
        public static long GetInt64(byte[] bytes)
        {
            if (bytes.Length == 8)
            {
                return (
                    (bytes[7] >> 4) * 0x1000000000000000 +
                    (bytes[7] & 0xf) * 0x100000000000000 +
                    (bytes[6] >> 4) * 0x10000000000000 +
                    (bytes[6] & 0xf) * 0x1000000000000 +
                    (bytes[5] >> 4) * 0x100000000000 +
                    (bytes[5] & 0xf) * 0x10000000000 +
                    (bytes[4] >> 4) * 0x1000000000 +
                    (bytes[4] & 0xf) * 0x100000000 +
                    (bytes[3] >> 4) * 0x10000000 +
                    (bytes[3] & 0xf) * 0x1000000 +
                    (bytes[2] >> 4) * 0x100000 +
                    (bytes[2] & 0xf) * 0x10000 +
                    (bytes[1] >> 4) * 0x1000 +
                    (bytes[1] & 0xf) * 0x100 +
                    (bytes[0] >> 4) * 0x10 +
                    (bytes[0] & 0xf) * 0x1
                    );
            }
            else
            {
                throw new ArgumentException("Array");
            }
        }
        public static long GetInt64(byte[] bytes, int offset)
        {
            if (offset + 8 <= bytes.Length)
            {
                return GetInt64(
                    new byte[]
                {
                bytes[offset+0],
                bytes[offset+1],
                bytes[offset+2],
                bytes[offset+3],
                bytes[offset+4],
                bytes[offset+5],
                bytes[offset+6],
                bytes[offset+7]
                }
                );
            }
            else
            {
                throw new ArgumentOutOfRangeException("index");
            }
        }
        public static double GetDouble(byte[] bytes)
        {
            return BitConverter.ToDouble(bytes, 0);
        }
        public static double GetDouble(byte[] bytes, int offset)
        {
            if (offset + 7 <= bytes.Length)
            {
                return GetDouble(new byte[]{
                bytes[offset+0],
                bytes[offset+1],
                bytes[offset+2],
                bytes[offset+3],
                bytes[offset+4],
                bytes[offset+5],
                bytes[offset+6],
                bytes[offset+7]
            });
            }
            else
            {
                throw new ArgumentOutOfRangeException(offset.ToString());
            }
        }
        public static char GetChar(byte[] bytes)
        {
            return GetChar(bytes, 0);
        }
        public static char GetChar(byte[] bytes, int offset)
        {
            if (offset >= bytes.Length || offset < 0)
            {
                throw new ArgumentOutOfRangeException("Index");
            }
            return (char)(bytes[offset]);
        }
        public static string GetCharString(byte[] bytes, int length)
        {
            return GetCharString(bytes, 0, length, Encoding.ASCII);
        }
        public static string GetCharString(byte[] bytes, int offset, int length)
        {
            return GetCharString(bytes, offset, length, Encoding.ASCII);
        }
        public static string GetCharString(byte[] bytes, int offset, int length, Encoding encode)
        {
            if (offset > bytes.Length)
            {
                throw new IndexOutOfRangeException(string.Format("Offset {0} Out of array length {1}", offset, bytes.Length));
            }
            if (offset+length>bytes.Length)
            {
                length = bytes.Length - offset;
            }
            return encode.GetString(bytes, offset, length);
        }
        public static float GetFloat(byte[] bytes)
        {
            return GetFloat(bytes, 0);
        }
        public static float GetFloat(byte[] bytes, int offset)
        {
            return 0;
        }
        //public static int GetInt32(byte[] p, bool lf)
        //{
        //    if (p.Length != 4)
        //    {
        //        throw new ArgumentException("Array");
        //    }
        //    //double i0, i1, i2, i3, i4, i5, i6, i7;
        //    //i0 = (p[0] & 0xf);
        //    //i1 = (p[0] >> 4) * 0x10;
        //    //i2 = (p[1] & 0xf) * 0x100;
        //    //i3 = (p[1] >> 4) * 0x1000;
        //    //i4 = (p[2] & 0xf) * 0x10000;
        //    //i5 = (p[2] >> 4) * 0x100000;
        //    //i6 = (p[3] & 0xf) * 0x1000000;
        //    //i7 = (p[3] >> 4) * 0x10000000;
        //    //i0 = (i0 + i1 + i2 + i3 + i4 + i5 + i6 + i7);
        //    //return (int)i0;
        //    return (int)(lf ?
        //        (
        //        (p[3] >> 4) * 0x10000000 +
        //        (p[3] & 0xf) * 0x1000000 +
        //        (p[2] >> 4) * 0x100000 +
        //        (p[2] & 0xf) * 0x10000 +
        //        (p[1] >> 4) * 0x1000 +
        //        (p[1] & 0xf) * 0x100 +
        //        (p[0] >> 4) * 0x10 +
        //        (p[0] & 0xf) // * (Math.Pow(0xf, 0)))
        //        )
        //        :
        //        (
        //        (p[0] >> 4) * 0x10000000 +
        //        (p[0] & 0xf) * 0x1000000 +
        //        (p[1] >> 4) * 0x100000 +
        //        (p[1] & 0xf) * 0x10000 +
        //        (p[2] >> 4) * 0x1000 +
        //        (p[2] & 0xf) * 0x100 +
        //        (p[3] >> 4) * 0x10 +
        //        (p[3] & 0xf) // * (Math.Pow(0xf, 0)))
        //        ));
        //}
        public static char GetHexValue(int i)
        {
            if (i < 10)
            {
                return (char)(i + 48);
            }
            return (char)(i - 10 + 65);
        }
        //public static string ToString(byte[] p)
        //{
        //    if (p == null)
        //    {
        //        throw new ArgumentNullException("Array");
        //    }
        //    int l = p.Length;
        //    char[] c = new char[l * 2];
        //    int num2 = 0;
        //    for (int i = 0; i < l * 2; i += 2)
        //    {
        //        byte b = p[num2++];
        //        c[i] = GetHexValue((int)(b / 16));
        //        c[i + 1] = GetHexValue((int)(b % 16));
        //    }
        //    return new string(c);
        //}
        public static string ArrayToHexString(byte[] bytes)
        {
            //return AToString(b, b.Length);
            //string r = "";
            //for (int i = 0; i < b.Length; i++)
            //{
            //    r = string.Format("{0}{1:X2}", r, b[i]);
            //}
            //return r;
            return ArrayToHexString(bytes, bytes.Length, "");
        }
        public static string ArrayToHexString(byte[] bytes, int L)
        {
            return ArrayToHexString(bytes, L, "");
        }
        public static string ArrayToHexString(byte[] bytes, int L, string split)
        {
            string r = "";
            for (int i = 0; i < L; i++)
            {
                r = string.Format("{0}{1}{2:X2}", r, split, bytes[i]);
            }
            return r;
        }
        public static byte[] GetBytes(short i)
        {
            byte[] r = new byte[2];
            r[0] = (byte)(0xff & i);
            r[1] = (byte)(i >> 8);
            return r;
        }
        public static byte[] GetBytes(int i)
        {
            byte[] r = new byte[4];
            r[0] = (byte)(0xff & i);
            r[1] = (byte)(i >> 8);
            r[2] = (byte)(0xffff & (i >> 16));
            r[3] = (byte)(i >> 16);
            return r;
        }
        public static byte[] GetBytes(long i)
        {
            byte[] r = new byte[4];
            r[0] = (byte)(0xff & i);
            r[1] = (byte)(i >> 8);
            r[2] = (byte)(0xffff & (i >> 16));
            r[3] = (byte)(i >> 16);
            r[4] = (byte)(0xffffff & (i >> 24));
            r[5] = (byte)(i >> 24);
            r[6] = (byte)(0xffffffff & (i >> 32));
            r[7] = (byte)(i >> 32);
            return r;
        }
        public static byte[] GetBytes(string s)
        {
            return Concate(BitConverter.GetBytes(s.Length), Encoding.ASCII.GetBytes(s));
        }
        private static byte[] GetArray(object l) { return Encoding.ASCII.GetBytes(string.Format("{0:2X}", l)); }
        public static byte[] Concate(byte[] bytes1, byte[] bytes2)
        {
            if (bytes1 == null || bytes2 == null)
            {
                throw new ArgumentNullException("Source");
            }
            if (bytes1.Length == 0)
            {
                return bytes2;
            }
            if (bytes2.Length == 0)
            {
                return bytes1;
            }
            byte[] r = new byte[bytes1.Length + bytes2.Length];
            Buffer.BlockCopy(bytes1, 0, r, 0, bytes1.Length);
            Buffer.BlockCopy(bytes2, 0, r, bytes1.Length, bytes2.Length);
            return r;
        }
        public static byte[] ToByteArray(string s)
        {
            //if (s.Trim().StartsWith("0x") && !(Regex.Match(s, "[^x0-9a-f]").Success))
            //{
            //    long l = Convert.ToInt64(s.Trim());
            //    if (l>short.MinValue && l<short.MaxValue)
            //    {
            //        return GetBytes((short)(l));
            //    }
            //    else if (l>int.MinValue && l<int.MaxValue)
            //    {
            //        return GetBytes((int)(l));
            //    }
            //    else if(l>long.MinValue&&l<long.MaxValue)
            //    {
            //        return GetBytes(l);
            //    }
            //    else
            //    {
            //        return GetArray(s);
            //    }
            //}
            ////else if (!(Regex.Match(s,"[^0-9]").Success))
            ////{

            ////}
            //else
            //{
            return GetArray(s);
            //}
        }
        public static byte[] FromString(string str)
        {
            return FromString(str, "");
        }
        public static byte[] FromString(string str, string split)
        {
            string[] s = str.Split(new string[] { split }, StringSplitOptions.RemoveEmptyEntries);
            byte[] r = new byte[s.Length];
            for (int i = 0; i < s.Length; i++)
            {
                r[i] = Convert.ToByte(s[i]);
            }
            return r;
        }
        public static string FormatArrayMatrix(byte[] bytes)
        {
            if (bytes == null)
            {
                return "";
            }
            string r = "", b = "", s = "";
            r = string.Format("{0}{1}Index \\ Offset  ", r, Environment.NewLine);
            int i = 0, j = 0;
            for (; i < 16; i++)
            {
                r = string.Format("{0} _{1:X}", r, i);
            }
            r = r + " [_____string_____]" + Environment.NewLine;
            i = bytes.Length / 16;
            if (i > 0)
            {
                for (int k = 0; k < i; k++)
                {
                    for (j = k * 16; j < (((k + 1) * 16 > bytes.Length) ? bytes.Length - 1 : ((k + 1) * 16)); j++)
                    {
                        b = string.Format("{0} {1:X2}", b, bytes[j]);
                        s = string.Format("{0}{1}", s, (bytes[j] == 0 ? '.' : ((bytes[j] == 0x0a || bytes[j] == 0x0d || bytes[j] == 0x08 || bytes[j] == 0x09 || bytes[j] == 0x7f) ? '_' : (char)bytes[j])));
                    }
                    r = string.Format("{0}{1:X11}_ |  {2}  {3:50}{4}", r, k, b, s, Environment.NewLine);
                    b = "";
                    s = "";
                    if (i > 0 && k == i - 1)
                    {
                        for (j = (k + 1) * 16; j < bytes.Length; j++)
                        {
                            b = string.Format("{0} {1:X2}", b, bytes[j]);
                            s = string.Format("{0}{1}", s, (bytes[j] == 0 ? '.' : ((bytes[j] == 0x0a || bytes[j] == 0x0d || bytes[j] == 0x08 || bytes[j] == 0x09 || bytes[j] == 0x7f) ? '_' : (char)bytes[j])));
                        }
                        for (j = 0; j < (i + 1) * 16 - bytes.Length; j++)
                        {
                            b = string.Format("{0}   ", b);
                        }
                        k++;
                        r = string.Format("{0}{1:X11}_ |  {2}  {3:50}{4}", r, k, b, s, Environment.NewLine);
                    }
                }
            }
            else
            {
                for (int l = 0; l < bytes.Length; l++)
                {
                    b = string.Format("{0} {1:X2}", b, bytes[l]);
                    s = string.Format("{0}{1}", s, (bytes[l] == 0 ? '.' : ((bytes[l] == 0x0a || bytes[l] == 0x0d || bytes[l] == 0x08 || bytes[l] == 0x09 || bytes[l] == 0x7f) ? '_' : (char)bytes[l])));
                }
                for (int l = 0; l < 16 - bytes.Length; l++)
                {
                    b = string.Format("{0}   ", b);
                }
                r = string.Format("{0}{1:X11}_ |  {2}  {3:50}{4}", r, 0, b, s, Environment.NewLine);
            }
            return r;
        }
        //public static byte[] GetBytes(string str)
        //{
        //    byte[] r = new byte[4 + str.Length];
        //    Buffer.BlockCopy(GetBytes(str.Length), 0, r, 0, 4);
        //    Buffer.BlockCopy(Encoding.ASCII.GetBytes(string.Format("{0:2X}", str)), 0, r, 4, str.Length);
        //    return r;
        //}
        public static int GetInt32BE(byte[] bytes, int offset)
        {
            if (offset >= 0 && offset + 4 <= bytes.Length)
            {
                byte[] b = new byte[4];
                System.Buffer.BlockCopy(bytes, offset, b, 0, 4);
                return GetInt32BE(b);
            }
            else
            {
                throw new ArgumentOutOfRangeException("Offset", offset, "OutOf " + bytes.Length.ToString());
            }
        }
        public static int GetInt32BE(byte[] bytes)
        {
            Array.Reverse(bytes);
            return GetInt32(bytes);
        }
    }
}
