using System;
using System.Collections.Generic;
using System.Reflection;

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
            typeof(void)
        };
        public static bool IsBaseType(this Type t) { return BaseTypes.Contains(t); }
        public static bool IsBaseType(this object o) { return o.GetType().IsBaseType(); }
        public static string GenericDeclare(this Type t)
        {
            if (t.IsGenericType)
            {
                string decl = string.Empty;
                foreach (Type g in t.GetGenericParameterConstraints())
                {
                    decl += (decl == string.Empty ? string.Empty : ", ") + (g.IsGenericType ? g.GenericDeclar() : g.Name);
                }
                return t.Name + "<" + decl + ">";
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
            string ret = value;
            for (int i = 0; i < repeat; i++)
            {
                ret += spliter + value;
            }
            return ret;
        }
        public static string Repeat(this char value, int repeat, string spliter = "") { return value.ToString().Repeat(repeat, spliter); }
    }
}
