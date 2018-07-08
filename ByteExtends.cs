using System;
using System.Text;

namespace MQTT.Common
{
    public static class ByteExtends
    {
        public static readonly byte[] Empty = new byte[] { };
        public static byte[] Append(this byte[] array, params byte[] append)
        {
            if (append == null || append.Length == 0)
            {
                return array;
            }
            else
            {
                Array.Resize<byte>(ref array, array.Length + append.Length);
                //byte[] value = new byte[];
                //Buffer.BlockCopy(array, 0, value, 0, array.Length);
                Buffer.BlockCopy(append, 0, array, array.Length, append.Length);
                //return value;
                return array;
            }
        }

        public static byte[] Insert(this byte[] buffer, int index, params byte[] value)
        {
            if (value == null || value.Length == 0)
            {
                return buffer;
            }
            else
            {
                if (buffer.Length < index + value.Length)
                {
                    buffer = new byte[buffer.Length + value.Length];
                    Buffer.BlockCopy(buffer, 0, value, 0, buffer.Length);
                }
                //Buffer.BlockCopy(buffer, 0, value, 0, buffer.Length);
                Buffer.BlockCopy(value, 0, buffer, index, value.Length);
                return value;
            }
        }
        public static byte[] GetBytes(this bool value) { return BitConverter.GetBytes(value); }
        public static byte[] GetBytes(this char value) { return BitConverter.GetBytes(value); }
        public static byte[] GetBytes(this double value) { return BitConverter.GetBytes(value); }
        public static byte[] GetBytes(this float value) { return BitConverter.GetBytes(value); }
        public static byte[] GetBytes(this int value) { return BitConverter.GetBytes(value); }
        public static byte[] GetBytes(this long value) { return BitConverter.GetBytes(value); }
        public static byte[] GetBytes(this short value) { return BitConverter.GetBytes(value); }
        public static byte[] GetBytes(this uint value) { return BitConverter.GetBytes(value); }
        public static byte[] GetBytes(this ulong value) { return BitConverter.GetBytes(value); }
        public static byte[] GetBytes(this ushort value) { return BitConverter.GetBytes(value); }
        public static byte[] GetASCIIBytes(this string value) { return Encoding.ASCII.GetBytes(value); }
        public static byte[] GetUTF8Bytes(this string value) { return Encoding.UTF8.GetBytes(value); }

        public static string GetASCIIString(this byte[] buffer, int index, int count) { return buffer.GetString(Encoding.ASCII, index, count); }
        public static string GetUTF8String(this byte[] buffer, int index, int count) { return buffer.GetString(Encoding.UTF8, index, count); }
        public static string GetString(this byte[] buffer, Encoding encoding, int index, int count) { return encoding.GetString(buffer, index, count); }

    }
}
