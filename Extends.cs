using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace TEArts.Etc.CollectionLibrary
{
    public static class TypeExtends
    {
        public static readonly List<Type> BaseTypes = new List<Type>
        {
            typeof(bool), typeof(byte), typeof(char),
            typeof(DateTime), typeof(decimal), typeof(double),
            typeof(float), typeof(Guid), typeof(int),
            typeof(IntPtr), typeof(long), typeof(object),
            typeof(sbyte), typeof(short), typeof(string),
            typeof(TimeSpan), typeof(Type), typeof(uint),
            typeof(ushort), typeof(ulong), typeof(UIntPtr),
            typeof(void), typeof(FieldInfo), typeof(PropertyInfo),
            typeof(MethodInfo), typeof(Exception)
        };
        public static bool IsBaseType(this Type t) { return BaseTypes.Contains(t); }
        public static bool IsBaseType(this object o) { return o.GetType().IsBaseType(); }
        public static string GenericDeclare(this Type t)
        {
            if (t.IsGenericType)
            {
                string decl = string.Empty;
                foreach (Type g in t.GetGenericArguments())
                {
                    decl += (decl == string.Empty ? string.Empty : ", ") + (g.IsGenericType ? g.GenericDeclare() : g.Name);
                }
                return t.Name + " < " + decl + " > ";
            }
            else
            {
                return t.Name;
            }
        }
    }
    public static class StringExtends
    {
        public static string Repeat(this string value, int repeat, string spliter = "")
        {
            if (string.IsNullOrEmpty(value))
            {
                return value;
            }
            if (repeat <= 0)
            {
                return value;
            }
            StringBuilder ret = new StringBuilder(value);
            for (int i = 0; i < repeat; i++)
            {
                ret.AppendFormat("{0}{1}", spliter, value);
            }
            return ret.ToString();
        }
        public static string Repeat(this char value, int repeat, string spliter = "") { return value.ToString().Repeat(repeat, spliter); }
    }

    public static class ArrayExtends
    {
        public static string Concate(this Array ary, string spliter = ", ")
        {
            string ret = string.Empty;
            StringBuilder sb = new StringBuilder();
            foreach (var i in ary)
            {
                sb.AppendFormat("{0}{1}", i, spliter);
            }
            ret = sb.ToString().TrimEnd(spliter.ToCharArray());
            return ret;
        }

    }

    public static class ByteArrayExtends
    {
        public static bool GetBit(this byte[] array, int index, int offset)
        {
            if (index >= array.Length || index < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }
            if (offset > 8 || offset < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(offset));
            }
            return (array[index] & (1 << offset)) >> offset == 1;
        }
    }
}
